using OpenNETCF.ORM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenNETCF.FormManager
{
    public class QuestionDetail
    {
        public string QuestionText { get; set; }
        public string Units { get; set; }
        public QuestionType QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public bool AcceptNotes { get; set; }
        public string DefaultValue { get; set; }
    }
}
