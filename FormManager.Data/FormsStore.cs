using OpenNETCF.FormManager;
using OpenNETCF.ORM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenNETCF.FormManager
{
    public class FormsStore : IFormStore
    {
        private ISQLBasedStore m_store;

        public FormsStore()
            : this(@"..\..\..\FormManager.sqlite")
        {
        }

        public FormsStore(string dataStorePath)
        {
            m_store = new SQLiteDataStore(dataStorePath);
            m_store.ConnectionBehavior = ConnectionBehavior.Persistent;

            if (!m_store.StoreExists)
            {
                m_store.CreateStore();
            }

            m_store.AddType<SQLForm>();
            m_store.AddType<SQLQuestion>();
            m_store.AddType<SQLField>();
        }

        internal SQLForm CreateForm(string name, string description)
        {
            var form = new SQLForm()
            {
                FormName = name,
                Description = description
            };

            // TODO: check for name duplication

            m_store.Insert(form);

            return form;
        }

        public SQLQuestion CreateQuestion(QuestionType type, string text, string units = null)
        {
            var question = new SQLQuestion()
            {
                QuestionText = text,
                QuestionType = type,
                Units = units
            };

            // TODO: check for duplication

            m_store.Insert(question);

            return question;
        }

        internal void AddQuestionToForm(SQLQuestion question, SQLForm form, bool required, bool acceptNotes, object defaultValue)
        {

            var fq = new StoreFormQuestion()
            {
                FormID = form.FormID,
                QuestionID = question.QuestionID,
                IsRequired = required,
                AcceptNotes = acceptNotes,
                DefaultValue = defaultValue.ToString()
            };

            m_store.Insert(fq);
        }


        public Task<IEnumerable<FormSummary>> GetFormSummariesAsync()
        {
            return Task.Run(() =>
            {
                var forms = m_store.Select<SQLForm>(false);

                return from f in forms
                       select new FormSummary()
                       {
                           FormName = f.FormName,
                           FormID = f.FormID,
                           Description = f.Description
                       };
            });
        }

        public Task<Form> GetFormAsync(int formID)
        {
            return Task.Run(() =>
            {
                var form = m_store.Select<SQLForm>(formID, true);

                if (form == null) return null;

                return ConvertSQLForm(form);
            });
        }

        public Form GetForm(string name)
        {
            var filters = new FilterCondition[]
                {
                    new FilterCondition("FormName", name, FilterCondition.FilterOperator.Equals)
                };
            var forms = m_store.Select<SQLForm>(filters, true);

            var f = forms.FirstOrDefault();

            if (f == null) return null;

            return ConvertSQLForm(f);
        }

        private Form ConvertSQLForm(SQLForm f)
        {
            // since all fields are stored in one table, we have to "arrange" them back by block number
            var result = new Form(f.FormName)
            {
                FormID = f.FormID,
                RequiresSignature = f.RequiresSignature,
                Description = f.Description
            };

            foreach (var question in f.Questions)
            {
                result.Questions.Add(new Question(question.QuestionText, question.QuestionType, question.Units, question.DefaultValue, question.IsRequired, question.AcceptNotes));
            }

            foreach (var field in f.Fields)
            {
                switch (field.BlockNumber)
                {
                    case 1:
                        result.Header.Add(field.Text, field.DefaultValue);
                        break;
                    case 2:
                        result.Footer.Add(field.Text, field.DefaultValue);
                        break;
                }
            }

            return result;
        }

        public void StoreForm(Form form)
        {
            m_store.BeginTransaction();
            try
            {
                var entity = new SQLForm()
                {
                    FormName = form.FormName,
                    Description = form.Description,
                    RequiresSignature = form.RequiresSignature
                };

                foreach (var question in form.Questions)
                {
                    entity.Questions.Add(new SQLQuestion()
                    {
                        QuestionText = question.QuestionText,
                        QuestionType = question.QuestionType,
                        Units = question.Units,
                        DefaultValue = question.DefaultValue,
                        IsRequired = question.IsRequired,
                        AcceptNotes = question.AcceptNotes
                    });
                }

                foreach (var field in form.Header)
                {
                    entity.Fields.Add(new SQLField()
                    {
                        Text = field.Text,
                        DefaultValue = field.DefaultValue,
                        BlockNumber = 1
                    });
                }

                foreach (var field in form.Footer)
                {
                    entity.Fields.Add(new SQLField()
                    {
                        Text = field.Text,
                        DefaultValue = field.DefaultValue,
                        BlockNumber = 2
                    });
                }
                m_store.Insert(entity, true);
                m_store.Commit();
            }
            catch (Exception ex)
            {
                m_store.Rollback();
                if (Debugger.IsAttached) Debugger.Break();
                throw ex;
            }
        }

        public void DeleteForm(Form form)
        {
            if (form.FormID > 0)
            {
                // deleting a form cascades, so do it as a transaction
                m_store.BeginTransaction();
                try
                {
                    m_store.Delete<SQLForm>(form.FormID);
                    m_store.Commit();
                }
                catch
                {
                    m_store.Rollback();
                }
            }
            else
            {
                // TODO: if it has no ID, does it actually exist in the DB?  Probably not (needs testing)
                if (Debugger.IsAttached) Debugger.Break();
            }
        }

        public Task StoreFormAsync(Form form)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFormAsync(Form form)
        {
            throw new NotImplementedException();
        }
    }
}
