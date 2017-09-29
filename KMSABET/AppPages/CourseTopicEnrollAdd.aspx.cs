using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CourseTopicEnrollAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(program);
                new Students().SelectCourse(course, program);
                new Students().GetAcadmicYear(program, course, Acadmicyear);
                new Students().GetSamester(semster, Acadmicyear, course);
                GetAllCourseTopicEnrollment();
            }
        }
        
        public void GetAllCourseTopicEnrollment()
        {
            try
            {
                string query = "select TOPIC_ID as ID, TOPIC_STATEMENT as TS, t2.course_name as 'CN', t3.clo_statement as 'C' from APP_COURSE_TOPIC t1 inner join App_Course t2 on t1.COURSE_ID = t2.course_id inner join I_CLO t3 on t1.CLO_ID = t3.clo_id  where t2.course_name = '" + course.SelectedValue + "'";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(query);
                
                List<APP_CourseTopic> list = new List<APP_CourseTopic>();

                while (sdb.Read())
                {
                    list.Add(new APP_CourseTopic() { TOPIC_ID = sdb["ID"].ToString(), Course_ID = sdb["CN"].ToString(), TOPIC_STATEMENT = sdb["TS"].ToString(), CLO = sdb["C"].ToString() });
                }

                MainGrid.DataSource = list;
                MainGrid.DataBind(); 
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error Occur While Get Course Topic Enrollment", ex);
            }
        }

        protected void program_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(course, program);
            GetAllCourseTopicEnrollment();
        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetAcadmicYear(program, course, Acadmicyear);
            GetAllCourseTopicEnrollment();
        }

        protected void MainGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            List<string> li = new List<string>();

            foreach (GridViewRow item in MainGrid.Rows)
            {
                CheckBox checkBox = (CheckBox)item.FindControl("ch");
                if (checkBox.Checked)
                {
                    li.Add(item.Cells[1].Text.ToString());
                }

            }

            Connections.LIST = li;

            Response.Redirect("~/AppPages/CourseTopicEnrollView.aspx?CID=" + course.SelectedValue + "&y=" + Acadmicyear.SelectedValue + "&s=" + semster.SelectedValue);
        }

        protected void Acadmicyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetSamester(semster, Acadmicyear, course);
            GetAllCourseTopicEnrollment();
        }

        protected void semster_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllCourseTopicEnrollment();
        }
    }
}