using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AskUserWhereAttributes
    {
        public int id { get; set; }
        public String tableName { get; set; }
        public String alias { get; set; }
        public String pkColumnName { get; set; }
        public String displayColumn { get; set; }
        public String headingLabel { get; set; }
        public int questionID { get; set; }
        public String questionStatement { get; set; }
        public String whereAskUserToReplace { get; set; }
        public int selectedItemValue { get; set; }
    }
}