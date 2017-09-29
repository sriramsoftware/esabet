using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class QueAttributeOption
    {
        public int attributeOptionId { get; set; }

        public String optionStatement { get; set; }

        public int attributeId { get; set; }

        public String attributeStatement { get; set; }

        public int score { get; set; }

        public String priorityOption { get; set; }
    }
}