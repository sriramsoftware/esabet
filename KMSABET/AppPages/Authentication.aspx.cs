using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class Authentication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }
        
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(
                "select t2.instructor_name as 'INS', FIRST_TIME_LOGIN_FLAG AS FIRST_LOGIN,"
                + " EMAIL as Email, t1.Username as ID, t1.InstructorID, ac.course_id,"
                + " ac.course_name + ' (' + ac.COURSE_NUMBER + ')' as course_name," 
                + " INSRUCTOR_TYPE as t, EXPERT_SYSTEM_TYPE AS ES_TYPE" 
                + " from Users t1 inner join App_Instructor t2 on t1.InstructorID = t2.instructor_id"
                + " join app_course ac on ac.course_id = t1.COURSE_ID"
                + " where t1.Username = '" + username.Text + "' and Password = '" + pass.Text + "';");
            Boolean recordExists = false;

            while (sdb.Read())
            {
                recordExists = true;
                Session["Instrutor"] = sdb["INS"];
                Session["InstructorID"] = sdb["InstructorID"];
                Session["LoginID"] = sdb["ID"];
                Session["Email"] = sdb["Email"];
                Session["CourseID"] = sdb["course_id"];
                Session["CourseName"] = sdb["course_name"];
                Session["FIRST_LOGIN"] = Boolean.Parse(sdb["FIRST_LOGIN"].ToString());
                
                String esType = sdb["ES_TYPE"].ToString();
                String roleType = sdb["t"].ToString();
                
                if (roleType == "2001") // Admin
                {
                    Session["userTypeId"] = "1";
                }
                else if (roleType == "2002") // Instructor
                {
                    Session["userTypeId"] = "2";
                }
                else if (roleType == "2003") // Super Admin
                {
                    Session["userTypeId"] = "3";
                }


                if (esType == "1901") //QUESTION BANK
                {
                    Session["ESTYPE"] = "1";
                }
                else if (esType == "1902") //IMPROVEMENT PLAN
                {
                    Session["ESTYPE"] = "2";
                }
                else //if (esType == "1903") //SUPER ADMIN
                {
                    Session["ESTYPE"] = "3";
                }

                Response.Redirect("/KMSPages/PageRedirection.aspx");
                
            }

            if (recordExists == false) TextBox1.Text = "Error: Provided username or password is wrong.";
        }

    }
}