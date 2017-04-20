using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenNETCF.FormManager
{
    public class FieldBlock : IEnumerable<Field>
    {
        Form m_form;

        public FieldBlock()
        {
            Fields = new List<Field>();
        }

        internal void SetForm(Form parent)
        {
            m_form = parent;
        }

        internal FieldBlock(Form parent)
            : this()
        {
            m_form = parent;
        }

        public List<Field> Fields { get; set; }

        public Field Add(string text, string defaultValue = "")
        {
            var field = new Field(text, defaultValue);
            Fields.Add(field);
            return field;
        }

        public IEnumerator<Field> GetEnumerator()
        {
            return Fields.GetEnumerator(); ;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
