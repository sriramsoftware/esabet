using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class ImpRuleQuesAnswer
    {
        public int answerId { get; set; }
        public String answerStatemet { get; set; }
        public int questionId { get; set; }
        public int comparisonTypeID { get; set; }
        public int comparisonValue { get; set; }
    }
}