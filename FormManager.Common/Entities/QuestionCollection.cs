using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenNETCF.FormManager
{
    public class QuestionCollection : IEnumerable<Question>
    {
        private readonly object m_syncRoot = new object();

        private Form m_form;
        private List<Question> m_questions;

        internal QuestionCollection()
        {
            m_questions = new List<Question>();
        }

        internal QuestionCollection(Form parent)
            : this()
        {
            m_form = parent;
        }

        internal void SetForm(Form parent)
        {
            m_form = parent;
        }

        public IEnumerator<Question> GetEnumerator()
        {
            return m_questions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return m_questions.Count; }
        }

        public Question Add(string questionText, QuestionType questionType, string units = "", string defaultValue = "", bool required = true, bool acceptNotes = true)
        {
            return this.Add(new Question(questionText, questionType, units, defaultValue, required, acceptNotes));
        }

        public Question Add(Question question)
        {
            if (!m_questions.Contains(question))
            {
                m_questions.Add(question);
            }

            return question;
        }

    }
}
