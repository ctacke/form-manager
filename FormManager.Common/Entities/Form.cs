using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenNETCF.FormManager
{
    public class Form : FormSummary
    {
        private FieldBlock m_header;
        private FieldBlock m_footer;
        private QuestionCollection m_questions;

        public bool RequiresSignature { get; set; }

        public Form()
        {
            Header = new FieldBlock(this);
            Questions = new QuestionCollection(this);
            Footer = new FieldBlock(this);
        }

        public Form(string formName)
            : this()
        {
            FormName = formName;
        }

        [JsonConverter(typeof(FieldBlockConverter))]
        public FieldBlock Header
        {
            get { return m_header; }
            set
            {
                m_header = value;
                m_header.SetForm(this);
            }
        }

        [JsonConverter(typeof(QuestionCollectionConverter))]
        public QuestionCollection Questions
        {
            get { return m_questions; }
            set
            {
                m_questions = value;
                m_questions.SetForm(this);
            }
        }

        [JsonConverter(typeof(FieldBlockConverter))]
        public FieldBlock Footer
        {
            get { return m_footer; }
            set
            {
                m_footer = value;
                m_footer.SetForm(this);
            }
        }
    }
}
