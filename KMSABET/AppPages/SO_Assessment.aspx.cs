using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class SO_Assessment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Program_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Course.Items.Clear();
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader res = db.readOperation(@"declare @a int select @a = program_id from App_Program where program_name = '" + Program.SelectedValue + "'; select (course_name) as Course from App_Course where App_Program_program_id = @a;");

                Course.Items.Add(new ListItem() { Text = "Select Course", Value = "Select Course" });

                while (res.Read())
                {
                    ListItem listItem = new ListItem();

                    listItem.Text = res["Course"].ToString();
                    listItem.Value = res["Course"].ToString();

                    Course.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Exception :", ex);
            }
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new Connections().SQLCON())
                {
                    string strQuery = "declare @a int; select @a = course_id from dbo.App_Course where course_name = '" + Course.SelectedValue + "'; select WHEN_SO_INTRODUCED,HOW_WILL_IT_ASCERTAINED,HOW_WILL_SO_ASSESSED from dbo.APP_SO_ASSESSMENT where COURSE_ID = @a;";
                    
                    SqlCommand cmd = new SqlCommand(strQuery);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        con.Open();
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error ", ex);
            }
        }
    }
}