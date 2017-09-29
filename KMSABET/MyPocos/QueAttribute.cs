using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class QueAttribute
    {
        public int attributeID {get; set;}

        public String attributeStatement { get; set; }

        public int attributeTypeID { get; set; }

        public String AttributeType { get; set; }

        public int attributeWeight { get; set; }

        public List<QueAttributeOption> optionsList { get; set; }

        public Boolean isRelevanceApplicable { get; set; }

    }
}