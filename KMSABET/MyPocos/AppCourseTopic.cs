using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AppCourseTopic
    {
        public int id { get; set; }
        public String topic { get; set; }
        public int lectureHours { get; set; }
        public int labHours { get; set; }
        public AppCourse course { get; set; }
    }
}