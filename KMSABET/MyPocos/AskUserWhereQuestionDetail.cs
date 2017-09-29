using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AskUserWhereQuestionDetail
    {
        public int questionID { get; set; }
        public String questionStatement { get; set; }
        public AskUserWhereAttributes whereAttributes { get; set; }
    }
}