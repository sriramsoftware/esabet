using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyUtilities
{
    public class MyConstants
    {
        public static string DBConnectionString { 
            get {
                String toReturn = "";
                //toReturn = "Server=tcp:isdrm6frsu.database.windows.net; Database=KMSABETDB; Uid=harisadmin@isdrm6frsu; Pwd=Baloch@1; Encrypt=yes;";
                toReturn = "Server=tcp:kmsabetpayasyougo.database.windows.net,1433;Database=KMSABET;Uid=harisadmin;Pwd=Baloch@1;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=36000;";
                //toReturn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KMSABETDB;Integrated Security=True";
                return toReturn;
            }
        }
        public static string SupportEmailAdd
        {
            get
            {
                return "muhammadhariskhan@gmail.com";
            }
        }
    }
}