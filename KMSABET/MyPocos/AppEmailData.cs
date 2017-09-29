using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    public class AppEmailData
    {
        public String toAddress { get; set; }
        public String subject { get; set; }
        public String bodyHtml { get; set; }
        public String ccAddress { get; set; }
    }
}