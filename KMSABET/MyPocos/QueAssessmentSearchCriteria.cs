using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class QueAssessmentSearchCriteria
    {
        public int programId { get; set; }
        public int courseId { get; set; }
        public int cloId { get; set; }
        public int soId { get; set; }
        public int courseTopicId { get; set; }
        public int selectedAttributeCount { get; set; }
    }
}