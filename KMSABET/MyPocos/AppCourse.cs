using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AppCourse
    {
        public int courseId { get; set; }
        public String courseName { get; set; }
        public AppProgram program { get; set; }
    }
}