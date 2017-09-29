using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class Programs : System.Web.UI.Page
    {
        int IDs = 0;
        bool Update = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
                bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
                IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);                

                if (Delete)
                {
                    new Connections().DeleteDate("delete from App_Program where program_id = " + IDs + "");
                    Response.Redirect("~/AppPages/ProgramView.aspx");
                }
                else if (Update)
                {   if(!IsPostBack)
                    SelectionData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SelectionData()
        {
            try
            {
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select Program_name as PN from App_Program where program_id = " + IDs + "");

                while (sdb.Read())
                {
                    Program.Text = sdb["PN"].ToString();   
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.Text.Length != 0)
                {
                    string q = "";
                    if (!Update)
                    {
                        q = "insert into App_Program (program_name) values ('" + Program.Text + "');";
                    }
                    else
                    {
                        q = "update App_Program set program_name = '" + Program.Text + "' where program_id = " + IDs + " ";
                    }

                    int a = new Connections().InsertData(q);

                    if (a == 1)
                    {
                        Response.Redirect("~/AppPages/ProgramView.aspx");
                    }
                    else
                    {
                        MyUtilities.LogUtils.myLog.Error("Error While Inseting Data.");
                    }
                }
                else
                {
                    Program.Focus();                    
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppPages/ProgramView.aspx");
        }
    }
}