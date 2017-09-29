using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class Score_Input : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                try
                {
                    new Connections().GetProgram(Program);
                    new Students().SelectCourse(Course, Program);
                    new Students().GetAcadmicYear(Program, Course, Year);
                    new Students().GetSamester(Semester, Year, Course);
                    new Connections().GetAssessmentandRawScore(assessmenttype, text, Semester, Year, Course);
                    new Connections().GetAssessemntsValues(Semester,text2, Course, Year, assessemnt, assessmenttype);
                    new Connections().GetStudent(Student, Session["LoginID"].ToString());
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void Savebtn_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "insert into APP_SCORE_INPUT ( STUDENT_ID,APP_SCORE_DESIGN_ID,MARKS_OBTAINED) values ("+Student.SelectedValue+",(select APP_SCORE_DESIGN_ID from APP_SCORE_DESIGN where SCORE_DISTRIBUTION_ID = (select SCORE_DISTRIBUTION_ID from APP_SCORE_DISTRIBUTION where COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semester, Course, Year) + ") and ASSESSMENT_NAME = '" + assessemnt.SelectedValue + "')," + Marks.Text + ")";

                int res = new Connections().InsertData(Query);
                
                if (res == 1)
                {
                    Response.Redirect("~/AppPages/ScoreInputView.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Error While Inseting Score Input");
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Inseting Score Input", ex);
            }
        }

        protected void Program_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().SelectCourse(Course, Program);
                new Students().GetAcadmicYear(Program, Course, Year);
                new Students().GetSamester(Semester, Year, Course);
                new Connections().GetAssessmentandRawScore(assessmenttype, text, Semester, Year, Course);
                new Connections().GetAssessemntsValues(Semester,text2, Course, Year, assessemnt, assessmenttype);
                new Connections().GetStudent(Student, Session["LoginID"].ToString());
            }
            catch (Exception ex)
            {

            }
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().GetAcadmicYear(Program, Course, Year);
                new Students().GetSamester(Semester, Year, Course);
                new Connections().GetAssessmentandRawScore(assessmenttype, new TextBox(), Semester, Year, Course);
                new Connections().GetAssessemntsValues(Semester,text2, Course, Year, assessemnt, assessmenttype);
                new Connections().GetStudent(Student, Session["LoginID"].ToString());
            }
            catch (Exception ex)
            {

            }
        }

        protected void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().GetSamester(Semester, Year, Course);
                new Connections().GetAssessmentandRawScore(assessmenttype, new TextBox(), Semester, Year, Course);
                new Connections().GetAssessemntsValues(Semester,text2, Course, Year, assessemnt, assessmenttype);
                new Connections().GetStudent(Student, Session["LoginID"].ToString());
            }
            catch (Exception ex)
            {

            }
        }

        protected void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Connections().GetAssessmentandRawScore(assessmenttype, new TextBox(), Semester, Year, Course);
                new Connections().GetAssessemntsValues(Semester,text2, Course, Year, assessemnt, assessmenttype);
                new Connections().GetStudent(Student, Session["LoginID"].ToString(), Semester, Course, Year);
            }
            catch (Exception ex)
            {

            }
        }

        protected void assessmenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Connections().GetAssessmentandRawScore(assessmenttype, new TextBox(), Semester, Year, Course);
                new Connections().GetAssessemntsValues(Semester,text2, Course, Year, assessemnt, assessmenttype);
                new Connections().GetStudent(Student, Session["LoginID"].ToString(), Semester, Course, Year);
            }
            catch (Exception ex)
            {

            }
        }

        protected void assessemnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}