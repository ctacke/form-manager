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
    [Entity(NameInStore = "FormQuestions")]
    public class StoreFormQuestion
    {
        [Field]
        public int FormID { get; set; }
        [Field]
        public int QuestionID { get; set; }
        [Field]
        public bool IsRequired { get; set; }
        [Field]
        public bool AcceptNotes { get; set; }
        [Field]
        public string DefaultValue { get; set; }


//        public virtual Form Form { get; set; }
//        public virtual Question Questions { get; set; }
    }
}
