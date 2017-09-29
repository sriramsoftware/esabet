using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    [Serializable]
    public class FavListQueScore
    {
        public int favListScoreID { get; set; }
        public int favListID { get; set; }
        public int quesID { get; set; }
        public int attrTypeID { get; set; }
        public String attributeStatement { get; set; }
        public int attributeID { get; set; }
        public String userSelectionStatement { get; set; }
        public String questionAttributeOptStatement { get; set; }
        public int userSelectedID { get; set; }
        public int quesAttributeID { get; set; }
        public int scoreValue { get; set; }
        public int weightValue { get; set; }
        public int sumScoreValue { get; set; }
    }
}