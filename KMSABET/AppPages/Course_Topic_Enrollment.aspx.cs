using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class Course_Topic_Enrollment : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(program);
                new Students().SelectCourse(course, program);
                new Students().GetAcadmicYear(program, course, Acadmicyear);
                new Students().GetSamester(semester, Acadmicyear, course);

                GetAllCourseTopicEnrollment();
                
            }


        }

        protected void program_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(course, program);
            new Students().GetAcadmicYear(program, course, Acadmicyear);
            new Students().GetSamester(semester, Acadmicyear, course);

            GetAllCourseTopicEnrollment();

        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {            
            new Students().GetAcadmicYear(program, course, Acadmicyear);
            new Students().GetSamester(semester, Acadmicyear, course);

            GetAllCourseTopicEnrollment();

        }

        protected void Acadmicyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetSamester(semester, Acadmicyear, course);
            GetAllCourseTopicEnrollment();
        }

        protected void semester_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllCourseTopicEnrollment();
        }
        
        public void GetAllCourseTopicEnrollment()
        {
            try
            {
                string query = "select t1.COURSE_TOPIC_ENR_ID as ID,t2.course_name as CN,t3.TOPIC_STATEMENT as St,t1.TOPIC_SEQ_NUM as SQ from APP_COURSE_TOPIC_ENROL t1 inner join App_Course t2 on t1.COURSE_ENR_ID = t2.course_id inner join APP_COURSE_TOPIC t3 on t1.COURSE_TOPIC_ID = t3.TOPIC_ID inner join APP_COURSE_ENROLMENT t4 on t4.COURSE_ENROL_ID = t1.COURSE_ENR_ID where t4.ACDEMIC_YEAR = " + Acadmicyear.SelectedValue + " and t4.SEMESTER = (select CODE_ID from App_CODE where CODE_VALUE = '" + semester.SelectedValue + "')";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(query);

                List<App_Course_Topic_Enroll> list = new List<App_Course_Topic_Enroll>();

                while (sdb.Read())
                {
                    list.Add(new App_Course_Topic_Enroll() { CourseTopicEnrID = sdb["ID"].ToString(), CourseEnrolID = sdb["CN"].ToString(), CourseTopicID = sdb["St"].ToString(), TopicSeqNo = sdb["SQ"].ToString() });
                }

                MainGrid.DataSource = list;
                MainGrid.DataBind();                
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error Occur While Get Course Topic Enrollment", ex);
            }
        }

        protected void MainGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
        }

    }
}