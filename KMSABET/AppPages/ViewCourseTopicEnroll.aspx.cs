using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class ViewCourseTopicEnroll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                List<App_Course_Topic_Enroll> list = new List<App_Course_Topic_Enroll>();

                try
                {
                    SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select t1.ACDEMIC_YEAR as 'Y', t3.CODE_VALUE as 'SM', t2.course_name as 'CN', t4.program_name as 'PN' from APP_COURSE_ENROLMENT t1 inner join App_Course t2 on t1.APP_COURSE_ID = t2.course_id inner join App_CODE t3 on t1.SEMESTER = t3.CODE_ID inner join App_Program t4 on t2.App_Program_program_id = t4.program_id where COURSE_ENROL_ID = '" + Request.QueryString["EID"].ToString() + "'");

                    while (sdb.Read())
                    {
                        Program.Text = sdb["PN"].ToString();
                        Course.Text = sdb["CN"].ToString();
                        Year.Text = sdb["Y"].ToString();
                        Semester.Text = sdb["SM"].ToString();
                    }


                    sdb = new MyUtilities.DBUtils().readOperation("select t1.COURSE_TOPIC_ENR_ID as ID, t2.TOPIC_STATEMENT as ST,TOPIC_SEQ_NUM as SEQ from APP_COURSE_TOPIC_ENROL t1 inner join APP_COURSE_TOPIC t2 on t1.COURSE_TOPIC_ID = t2.TOPIC_ID where COURSE_ENR_ID = " + Request.QueryString["EID"].ToString() + "");

                    while (sdb.Read())
                    {
                        list.Add(new App_Course_Topic_Enroll() { CourseTopicEnrID = sdb["ID"].ToString(), CourseTopicID = sdb["ST"].ToString(), TopicSeqNo = sdb["seq"].ToString() });
                    }

                    MainGrid.DataSource = list;
                    MainGrid.DataBind();

                }
                catch (Exception ex)
                {
                    MyUtilities.LogUtils.myLog.Error("Error Occure While Run Page Load Of Course Topic Enroll View.", ex);

                }


            }
        }
    }
}