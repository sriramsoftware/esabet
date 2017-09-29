using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AppCLO
    {
        public int cloId { get; set; }
        public String cloStatement { get; set; }
        public int courseId { get; set; }
        public String courseName { get; set; }
        public int programId { get; set; }
        public String programName { get; set; }
        public List<AppSO> soList { get; set; }
    }
}