
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SetValueToDropDown(string query, DropDownList dlist, string dlistName)
        {
            try
            {
                SqlConnection con = new Connections().SQLCON();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                con.Open();

                SqlDataReader ddlValues;
                ddlValues = cmd.ExecuteReader();

                dlist.DataSource = ddlValues;
                dlist.DataValueField = dlistName;
                dlist.DataTextField = dlistName;
                dlist.DataBind();

                con.Close();
                con.Dispose();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

        }

 
        protected void grid_Load(object sender, EventArgs e)
        {
            try
            {
                string a = @"select COURSE_ENROL_ID as ID,t2.course_name as 'Course Name',ACDEMIC_YEAR as 'Acadmic Year',t3.instructor_name as 'Instructor Name',t4.CODE_VALUE as Semester
                from APP_COURSE_ENROLMENT t1 inner join App_Course t2 
                on t1.APP_COURSE_ID = t2.course_id 
                inner join App_Instructor t3 
                on t1.INSTRUCTOR_ID = t3.instructor_id 
                inner join App_CODE t4 
                on t1.SEMESTER = t4.CODE_ID where t1.UNIVERSITY_ID = t3.UNI_ID";

                MyUtilities.DBUtils db = new MyUtilities.DBUtils();

                SqlDataReader sdb =  db.readOperation(a);
                List<Course_Information> list = new List<Course_Information>();
                
                while (sdb.Read())
                {
                    Course_Information info = new Course_Information() { ACDEMIC_YEAR = sdb["Acadmic Year"].ToString(), APP_COURSE_ID = sdb["Course Name"].ToString(), COURSE_ENROL_ID = sdb["ID"].ToString(), INSTRUCTOR_ID = sdb["Instructor Name"].ToString(), SEMESTER = sdb["Semester"].ToString() };
                    list.Add(info);
                }

                grid.DataSource = list;
                grid.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Addnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppPages/CourseViewData.aspx");
        }
    }
}