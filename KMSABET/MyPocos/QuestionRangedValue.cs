using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace KMSABET.MyPocos
{
    [Serializable]
    public class QuestionRangedValue
    {
        public String attributeID { get; set; }
        public String fromValue { get; set; }
        public String toValue { get; set; }
    }
}