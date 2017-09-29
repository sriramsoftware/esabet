using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class ViewUniversity : System.Web.UI.Page
    {
        bool Update;
        int IDs;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
                bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
                IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

                if (Delete)
                {
                    new Connections().DeleteDate("delete from APP_UNIVERSITY where UNI_ID = " + IDs + "");
                    Response.Redirect("~/AppPages/University.aspx");
                }
                else if (Update)
                {
                    if (!IsPostBack)
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
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select UNI_ID as ID, UNI_NAME as Name, UNI_ADDRESS as 'A', UNI_VERIFIED as V from APP_UNIVERSITY where UNI_ID = " + IDs + ";");
                
                while (sdb.Read())
                {
                    uniname.Text = sdb["Name"].ToString();
                    Address.Text = sdb["A"].ToString();
                    isv.Checked = Convert.ToBoolean(sdb["V"].ToString());
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {   
                if (uniname.Text.Length != 0 && Address.Text.Length != 0)
                {
                    string q = "";
                    if (!Update)
                    {
                        q = "insert into APP_UNIVERSITY (UNI_NAME,UNI_ADDRESS,UNI_VERIFIED) values ('" + uniname.Text + "','" + Address.Text + "'," + (Convert.ToInt32(isv.Checked)).ToString() + ");";
                    }
                    else
                    {
                        q = "update APP_UNIVERSITY set UNI_NAME = '" + uniname.Text + "', UNI_ADDRESS = '" + Address.Text + "', UNI_VERIFIED = '" + (Convert.ToInt32(isv.Checked)).ToString() + "' where UNI_ID = " + IDs + ";";
                    }

                    int a = new Connections().InsertData(q);

                    if (a == 1 )
                    {
                        Response.Redirect("~/AppPages/University.aspx");
                    }
                    else
                    {
                        MyUtilities.LogUtils.myLog.Error("Error While Inserting or Updating. " + q);
                    }
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }
    }
}