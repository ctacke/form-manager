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
    [Entity(KeyScheme = KeyScheme.Identity, NameInStore = "Questions")]
    public class SQLQuestion
    {
        [Field(IsPrimaryKey = true)]
        public int QuestionID { get; set; }
        [Field]
        public int FormID { get; set; }
        [Field]
        public string QuestionText { get; set; }
        [Field]
        public string Units { get; set; }
        [Field]
        public QuestionType QuestionType { get; set; }
        [Field]
        public string DefaultValue { get; set; }
        [Field]
        public bool IsRequired { get; set; }
        [Field]
        public bool AcceptNotes { get; set; }
        [Field]
        public int DisplayIndex { get; set; }

        //        public virtual ICollection<FormQuestion> FormQuestion { get; set; }
    }
}
