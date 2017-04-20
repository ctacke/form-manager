using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenNETCF.FormManager
{
    public class Field
    {
        public string Text { get; set; }
        public string DefaultValue { get; set; }

        public Field()
        {
        }

        public Field(string text, string defaultValue = "")
        {
            Text = text;
            DefaultValue = defaultValue;
        }
    }
}
