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
    [Entity(KeyScheme=KeyScheme.Identity, NameInStore ="Forms")]
    internal class SQLForm
    {
        public SQLForm()
        {
            Fields = new List<SQLField>();
            Questions = new List<SQLQuestion>();
        }

        [Field(IsPrimaryKey=true)]
        public int FormID { get; set; }
        [Field]
        public string FormName { get; set; }
        [Field]
        public string Description { get; set; }
        [Field]
        public bool RequiresSignature { get; set; }
        [Reference(typeof(SQLField), "FormID", CascadeDelete = true)]
        public List<SQLField> Fields { get; set; }
        [Reference(typeof(SQLQuestion), "FormID", CascadeDelete = true)]
        public List<SQLQuestion> Questions { get; set; }
    }

    [Entity(KeyScheme = KeyScheme.Identity, NameInStore = "Fields")]
    internal class SQLField
    {
        [Field(IsPrimaryKey = true)]
        public int FieldID { get; set; }
        [Field]
        public int FormID { get; set; }
        [Field]
        public string Text { get; set; }
        [Field]
        public string DefaultValue { get; set; }
        [Field]
        public int BlockNumber { get; set; }
        [Field]
        public int DisplayIndex { get; set; }

    }
}
