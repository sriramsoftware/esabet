using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class ProgramView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("Select Program_id as ID, Program_Name as PN from App_Program");
                List<Program> list = new List<Program>();
                while (sdb.Read())
                {
                    list.Add(new Program() { ID = sdb["ID"].ToString(), Program_Name = sdb["PN"].ToString() });
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