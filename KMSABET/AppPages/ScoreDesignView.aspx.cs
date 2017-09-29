using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class ScoreDesignView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(programs);
                new Students().SelectCourse(course, programs);
                new Students().GetAcadmicYear(programs, course, Years);
                new Students().GetSamester(Semester, Years, course);
                new Connections().GetAssessmentandRawScore(Assessments, new TextBox(), Semester, Years, course);
                GetData();
            }
            
        }

        private void GetData()
        {
            try
            {
                List<ScoreDesignDataView> List = new List<ScoreDesignDataView>();
                string Query = "select t1.APP_SCORE_DESIGN_ID as ID, t3.CODE_VALUE as 'AT', t1.ASSESSMENT_NAME as AN, t4.clo_statement as 'CLOS', t1.RAW_SCORE as 'RS',t1.SCORE_VALUE as 'SV', t2.SCORE_VALUE as 'TS'  from APP_SCORE_DESIGN t1 inner join APP_SCORE_DISTRIBUTION t2 on t1.SCORE_DISTRIBUTION_ID = t2.SCORE_DISTRIBUTION_ID inner join App_CODE t3 on t2.ASSESSMENT_ID = t3.CODE_ID inner join I_CLO t4 on t1.CLO_ID = t4.clo_id where t3.CODE_VALUE = '" + Assessments.SelectedValue + "' and t2.COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semester, course, Years) + "";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(Query);
                
                while (sdb.Read())
                {
                    List.Add(new ScoreDesignDataView() { Assessment = sdb["AN"].ToString(), Assessment_type = sdb["AT"].ToString(), CLO_Statement = sdb["CLOS"].ToString(), ID = sdb["ID"].ToString(), Raw_Score = sdb["RS"].ToString(), Score_Value = sdb["SV"].ToString(), Total_Score = sdb["TS"].ToString() });
                }

                MainGrid.DataSource = List;
                MainGrid.DataBind();
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void programs_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(course, programs);
            new Students().GetAcadmicYear(programs, course, Years);
            new Students().GetSamester(Semester, Years, course);                        
            new Connections().GetAssessmentandRawScore(Assessments, new TextBox(), Semester, Years, course);
            GetData();
        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {   
            new Students().GetAcadmicYear(programs, course, Years);
            new Students().GetSamester(Semester, Years, course);                        
            new Connections().GetAssessmentandRawScore(Assessments, new TextBox(), Semester, Years, course);
            GetData();
        }

        protected void Years_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetSamester(Semester, Years, course);                        
            new Connections().GetAssessmentandRawScore(Assessments, new TextBox(), Semester, Years, course);
            GetData();
        }

        protected void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {   
            new Connections().GetAssessmentandRawScore(Assessments, new TextBox(), Semester, Years, course);
            GetData();
        }

        protected void MainGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void r_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}