using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    [Serializable]
    public class QueQuestionScore
    {
        public int questionScoreID { get; set; }
        public int attributeID { get; set; }
        public int assessmentID { get; set; }
        public String attributeStatement { get; set; }
        public int attributOptionId { get; set; }
        public String userSelectionStatement { get; set; }
        public int questionID { get; set; }
        public String questionAttributeOptStatement { get; set; }
        public String questionStatement { get; set; }
        public int scoreValue { get; set; }
        public int weightValue { get; set; }
        public int sumScoreValue { get; set; }
        public int questionAttributeOptId { get; set; }
        public int attributeTypeId { get; set; }
        
    }
}