using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class FinalGradeView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(programs);
                new Students().SelectCourse(course, programs);
                new Students().GetAcadmicYear(programs, course, Years);
                new Students().GetSamester(Semester, Years, course);
                getData();
            }

        }

        protected void programs_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(course, programs);
            new Students().GetAcadmicYear(programs, course, Years);
            new Students().GetSamester(Semester, Years, course);
            getData();
        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {   
            new Students().GetAcadmicYear(programs, course, Years);
            new Students().GetSamester(Semester, Years, course);
            getData();
        }

        protected void Years_SelectedIndexChanged(object sender, EventArgs e)
        {   
            new Students().GetSamester(Semester, Years, course);
            getData();
        }

        protected void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {
            getData();
        }

        public void getData()
        {
            try
            {
                string CEID = new Students().GetCoureEnrollID(Semester, course, Years);
                string Query = "select t.SCORE_DISTRIBUTION_ID as ID, t2.CODE_VALUE as AID,t.SCORE_VALUE as SV,t.COURSE_ENR_ID as CID from APP_SCORE_DISTRIBUTION t inner join App_CODE t2 on t.ASSESSMENT_ID = t2.CODE_ID inner join APP_COURSE_ENROLMENT t3 on t.COURSE_ENR_ID = t3.COURSE_ENROL_ID inner join App_Instructor t4 on t3.UNIVERSITY_ID = t4.UNI_ID  where COURSE_ENR_ID = " + CEID + " and t4.instructor_id = " + Session["LoginID"].ToString() + "";                
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(Query);
                List<Score_Distribution> List = new List<Score_Distribution>();

                while (sdb.Read())
                {
                    List.Add(new Score_Distribution() { Assessment_ID = sdb["AID"].ToString(), ID = sdb["ID"].ToString(), Score_Value = sdb["SV"].ToString() });    
                }


                MainGrid.DataSource = List;
                MainGrid.DataBind();

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

    }
}