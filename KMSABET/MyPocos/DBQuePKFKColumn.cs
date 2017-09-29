using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyPocos
{
    [Serializable]
    public class DBQuePKFKColumn
    {
        public String pkTableName { get; set; }
        public String pkColumnName { get; set; }
        public String fkTableName { get; set; }
        public String fkColumnName { get; set; }
    }
}