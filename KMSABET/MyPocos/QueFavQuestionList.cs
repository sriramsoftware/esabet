using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class QueFavQuestionList
    {
        public int favQuestionId { get; set;}
        public String favQuestionName { get; set; }
        public int instructorId { get; set; }
        public String instructorName { get; set; }
        public int courseId { get; set; }
        public String courseName { get; set; }
        public int programId { get; set; }
        public String programName { get; set; }
        public String cloName { get; set; }
        public String soName { get; set; }
        public String courseTopicName { get; set; }
    }
}