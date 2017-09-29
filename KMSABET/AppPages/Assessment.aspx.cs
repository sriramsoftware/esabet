using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class Assessment : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Connections.LandFirstTimeVal == 0)
                {
                    Program.Items.Insert(0, new ListItem() { Text = "Select Program", Value = "Select Program" });
                    Connections.LandFirstTimeVal = 1;
                }
                else
                {
                    Connections.LandFirstTimeVal = 1;
                }
                

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error Onloading Assessment Page ", ex);
            }
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
                MyUtilities.LogUtils.myLog.Error("Selecting Program And Courses : ", ex);
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                string qu = "declare @a int; declare @b int; declare @c int; Select @a = course_id from App_Course where course_name = '" + Course.SelectedValue + "'; Select @b = SO_ASSESSMENT_ID from dbo.APP_SO_ASSESSMENT where COURSE_ID = @a; select top 1 @c = clo_id from I_CLO where App_Course_course_id = @a; insert into dbo.APP_SCORE_DESIGN (COURSE_ID,ASSESSMENT_ID,CLO_ID, RAW_SCORE) values (@a,@b,@c," + Int32.Parse(Marks.Text) + ");";
                int res = new Connections().InsertData(qu);
                
                if (res == 1)
                {
                    Response.Write("Successfully Complete.");
                }
                else
                { 
                    Response.Write("Error Occur."); 
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Inserting The Assessment Marks : ", ex);
            }
        }
    }
}