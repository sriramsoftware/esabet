using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class ImpRuleQuestion
    {
        public int ruleQuesId { get; set; }
        public String ruleQuestionStatemet { get; set; }
        public int ruleQuesTypeID { get; set; }
        public String ruleQuesType { get; set; }
        public AppCLO cloData { get; set; }
        public String dBQueryStatement { get; set; }
        public int dbQueryCalculatedValue { get; set; }
    }
}