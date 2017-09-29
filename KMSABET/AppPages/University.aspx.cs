using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class University : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select UNI_ID as ID, UNI_NAME as Name, UNI_ADDRESS as 'A', UNI_VERIFIED as V from APP_UNIVERSITY;");
                List<Universities> list = new List<Universities>();
                while (sdb.Read())
                {
                    list.Add(new Universities() { ID = sdb["ID"].ToString(), UA = sdb["A"].ToString(), UN = sdb["Name"].ToString(), V = sdb["V"].ToString()  });
                }

                MainGrid.DataSource = list;
                MainGrid.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}