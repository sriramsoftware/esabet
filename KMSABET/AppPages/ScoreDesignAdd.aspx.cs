using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class ScoreDesignAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(programs);
                new Students().SelectCourse(course, programs);
                new Students().GetAcadmicYear(programs, course, Years);
                new Students().GetSamester(Semester, Years, course);
                GetAssessment(assessment, ASC);
                GetCLO(CLO);
                
            }
        }

        protected void programs_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(course, programs);
            new Students().GetAcadmicYear(programs, course, Years);
            new Students().GetSamester(Semester, Years, course);
            GetAssessment(assessment, ASC);
            GetCLO(CLO);
        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetAcadmicYear(programs, course, Years);
            new Students().GetSamester(Semester, Years, course);
            GetAssessment(assessment, ASC);
                GetCLO(CLO);
        }

        protected void Years_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetSamester(Semester, Years, course);
            GetAssessment(assessment, ASC);
            GetCLO(CLO);
        }

        protected void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAssessment(assessment, ASC);
            GetCLO(CLO);
        }

        List<string> li = new List<string>();
        public void GetAssessment(DropDownList Assessment, TextBox t1)
        {
            try
            {
                assessment.Items.Clear();
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select t2.CODE_VALUE as ASS, t1.SCORE_VALUE as SV from APP_SCORE_DISTRIBUTION t1 inner join App_CODE t2 on t1.ASSESSMENT_ID = t2.CODE_ID where COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semester, course, Years) + "");
                
                while (sdb.Read())
                {
                    li.Add(sdb["SV"].ToString());
                    Assessment.Items.Add(sdb["ASS"].ToString());
                }

                if (li.Count != 0)
                {
                    t1.Text = li[assessment.SelectedIndex];
                    Assessment.Enabled = true;
                }
                else
                {
                    Assessment.Enabled = false;
                    t1.Text = "";
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Get Assessment", ex);
            }
        }

        public void GetCLO(DropDownList CLO)
        {
            try
            {
                CLO.Items.Clear();
                string Query = "select clo_statement as ST from I_CLO where App_Course_course_id = (select course_id from App_Course where course_name = '" + course.SelectedValue + "');";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(Query);
                
                while (sdb.Read())
                {
                    CLO.Items.Add(sdb["ST"].ToString());
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void assessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ASC.Text = li[assessment.SelectedIndex];
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (assessment.Items.Count != 0 && ASC.Text != "" && AN.Text != "" && RS.Text != "" && SV.Text != "" && Convert.ToInt32(ASC.Text) >= Convert.ToInt32(SV.Text))
                {
                    string Query = "insert into APP_SCORE_DESIGN (CLO_ID,RAW_SCORE,SCORE_VALUE,SCORE_DISTRIBUTION_ID,ASSESSMENT_Name) values ((select clo_id from I_CLO where clo_statement = '" + CLO.SelectedValue + "')," + RS.Text + "," + SV.Text + ",(select SCORE_DISTRIBUTION_ID from APP_SCORE_DISTRIBUTION where ASSESSMENT_ID = (SELECT CODE_ID FROM APP_CODE WHERE CODE_VALUE = '" + assessment.SelectedValue + "') AND COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semester, course, Years) + "),'" + AN.Text + "');";
                    Response.Write(Query);
                    int a = new Connections().InsertData(Query);
                    if (a == 1)
                    {
                        Response.Redirect("~/AppPages/ScoreDesignView.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

    }
}