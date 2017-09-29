using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AppEmailConfiguration
    {
        public String smtpHost { get; set; }
        public String fromAddress { get; set; }
        public String fromPassword { get; set; }
    }
}