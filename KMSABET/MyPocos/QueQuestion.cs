using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    [Serializable]
    public class QueQuestion
    {
        public int questionId { get; set;}
        public String questionStatement { get; set; }
        public int instructorId { get; set; }
        public String instructorName { get; set; }
        public int courseId { get; set; }
        public String courseName { get; set; }
        public int programId { get; set; }
        public String programName { get; set; }
        public int questionType { get; set; }
        public int courseTopicId { get; set; }
        public String courseTopicName { get; set; }
        public int cloId { get; set; }
        public String cloStatement { get; set; }
        public int soId { get; set; }
        public String soStatement { get; set; }
        public int sumScore { get; set; }
        public List<int> attrOptionIds { get; set; }
    }
}