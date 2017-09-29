using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CourseTopicView : System.Web.UI.Page
    {
        
        bool Updates = false;
        int IDs = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(Program);
                new Students().SelectCourse(Course, Program);
                new ClOSO().GETCLOs(CLO,Course);
            }

            Updates = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
            bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
            IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            bool View = Request.QueryString["View"] == null ? false : bool.Parse(Request.QueryString["View"]);

            if (Delete)
            {
                DeleteData();
                Response.Redirect("~/AppPages/CourseTopic.aspx");
            }
            else if (Updates)
            {
                if(Page.IsPostBack == false)
                {
                    SelectionData();
                    Update.Visible = true;
                    button.Visible = false;
                }
            }
            else if (View)
            {
                SelectionData();
                Update.Visible = false;
                button.Visible = false;
                TopicStatement.Enabled = false;
                Course.Enabled = false;
                Program.Enabled = false;
                Lab_Hours.Enabled = false;
                Lecture.Enabled = false;
                CLO.Enabled = false;
            }
            else
            {
                Update.Visible = false;
                button.Visible = true;                
            }
        }

        private void SelectionData()
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                string que = "Declare @a varchar(50);Declare @b varchar(50); select @a = t2.program_name, @b = t1.course_name from App_Course t1 inner join App_Program t2 on t1.App_Program_program_id = t2.program_id where t1.course_id = (select COURSE_ID from APP_COURSE_TOPIC where TOPIC_ID = " + IDs + "); select *,@a as PN,@b as CN from APP_COURSE_TOPIC where TOPIC_ID = " + IDs + "";
                SqlDataReader res = db.readOperation(que);
                
                while (res.Read())
                {

                    Program.SelectedValue = res["PN"].ToString();
                    Course.Items.Add(res["CN"].ToString());
                    Course.SelectedIndex = Course.Items.Count - 1;

                    TopicStatement.Text = res["TOPIC_STATEMENT"].ToString();

                    Lab_Hours.Items.Add(res["LAB_HOURS"].ToString());
                    Lab_Hours.SelectedIndex = Lab_Hours.Items.Count - 1;

                    Lecture.Items.Add(res["LECTURE_HOURS"].ToString());
                    Lecture.SelectedIndex = Lecture.Items.Count - 1;
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error:", ex);
            }
        }

        protected void button_Click(object sender, EventArgs e)
        {
            try
            {
                Connections con = new Connections();

                int res = con.InsertData("insert into APP_COURSE_TOPIC (TOPIC_STATEMENT,COURSE_ID,CLO_ID,LECTURE_HOURS,LAB_HOURS) values ('" + TopicStatement.Text + "'," + new Connections().GetCourseID(Course) + "," + CLO.SelectedValue + "," + Lecture.SelectedValue + "," + Lab_Hours.SelectedValue + ");");

                if (res == 1)
                {   
                    Response.Redirect("~/AppPages/CourseTopic.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Error Occure While Inserting Course Topic");
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void DeleteData()
        {
            try
            {
                int a = new Connections().DeleteDate("delete from APP_COURSE_TOPIC where TOPIC_ID = " + IDs + "");

                if (a == 1)
                {                    
                    Response.Redirect("~/AppPages/CourseTopic.aspx");
                }
                else
                {
                    Response.Write("Error");
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }
        protected void Program_SelectedIndexChanegd(object sender, EventArgs e)
        {
            new Students().SelectCourse(Course, Program);
            new ClOSO().GETCLOs(CLO, Course);
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            new ClOSO().GETCLOs(CLO, Course);
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {
                Connections con = new Connections();

                int res = con.InsertData("update APP_COURSE_TOPIC set TOPIC_STATEMENT = '" + TopicStatement.Text + "', LAB_HOURS = " + Lab_Hours.SelectedValue + ", LECTURE_HOURS = " + Lecture.SelectedValue + ", COURSE_ID = " + new Connections().GetCourseID(Course) + " where TOPIC_ID = " + IDs + ";");

                    if (res == 1)
                    {
                        Response.Redirect("~/AppPages/CourseTopic.aspx");
                    }
                    else
                    {
                        MyUtilities.LogUtils.myLog.Error("Error Occure While Update Data");
                    }
                
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void CLO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}