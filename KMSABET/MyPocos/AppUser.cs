using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AppUser
    {
        public String username { get; set; }
        public String pwd { get; set; }
        public String fullName { get; set; }
        public String email { get; set; }
        public String module { get; set; }
        public String userType { get; set; }
        public String course { get; set; }
        public int instructorId { get; set; }
        public int instructorTypeId { get; set; }
        public int courseId { get; set; }
        public int expertSystemTypeId { get; set; }
    }
}