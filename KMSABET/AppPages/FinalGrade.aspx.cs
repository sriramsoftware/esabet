using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;

namespace KMSABET.AppPages
{
    public partial class FinalGrade : System.Web.UI.Page
    {
        List<string> querys = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                Clearall();
                new Connections().GetProgram(programs);
                new Students().SelectCourse(course, programs);
                new Students().GetAcadmicYear(programs, course, Years);
                new Students().GetSamester(Semester, Years, course);
                GetUniBYCourse();

            }
            
        }

        public int ConvInt(TextBox t1)
        {
            try
            {
                return Int32.Parse((t1.Text));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return 0;

            }

        }

        string[] Assessments = new string[]
        {   
            "Quizzes",
            "Homework Assignments",
            "Team Project",
            "Attendance",
            "Presentation",
            "Life Long Learning Assignment",
            "Contemporay Issues Knowlege Test",
            "Lab Reports",
            "Lab Examination",
            "Mid Term Examination",
            "Exam 1",
            "Exam 2",
            "Final Examination",
            "Other Assessments"
        };



        protected void OnSaveBtn(object sender, EventArgs e)
        {
            try
            {
                Connections con = new Connections();

                Label1.Text = (ConvInt(TextBox1) + ConvInt(TextBox2) + ConvInt(TextBox3) + ConvInt(TextBox4) + ConvInt(TextBox5) + ConvInt(TextBox6) + ConvInt(TextBox7) + ConvInt(TextBox8) + ConvInt(TextBox9) + ConvInt(TextBox10) + ConvInt(TextBox11) + ConvInt(TextBox12) + ConvInt(TextBox13) + ConvInt(TextBox14)).ToString();

                GetReady(TextBox1, 0);
                GetReady(TextBox2, 1);
                GetReady(TextBox3, 2);
                GetReady(TextBox4, 3);
                GetReady(TextBox5, 4);
                GetReady(TextBox6, 5);
                GetReady(TextBox7, 6);
                GetReady(TextBox8, 7);
                GetReady(TextBox9, 8);
                GetReady(TextBox10, 9);
                GetReady(TextBox11, 10);
                GetReady(TextBox12, 11);
                GetReady(TextBox13, 12);
                GetReady(TextBox14, 13);


                for (int i = 0; i < querys.Count; i++)
                {
                    con.InsertData(querys[i]);    
                }

                Response.Redirect("~/AppPages/FinalGradeView.aspx");


            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Inserting The Score : ", ex);
            }


        }

        public void SUMGRADE()
        {
            Label1.Text = (ConvInt(TextBox1) + ConvInt(TextBox2) + ConvInt(TextBox3) + ConvInt(TextBox4) + ConvInt(TextBox5) + ConvInt(TextBox6) + ConvInt(TextBox7) + ConvInt(TextBox8) + ConvInt(TextBox9) + ConvInt(TextBox10) + ConvInt(TextBox11) + ConvInt(TextBox12) + ConvInt(TextBox13) + ConvInt(TextBox14)).ToString();
        }

        public void Clearall()
        {
            TextRest(TextBox1);
            TextRest(TextBox2);
            TextRest(TextBox3);
            TextRest(TextBox4);
            TextRest(TextBox5);
            TextRest(TextBox6);
            TextRest(TextBox7);
            TextRest(TextBox8);
            TextRest(TextBox9);
            TextRest(TextBox10);
            TextRest(TextBox11);
            TextRest(TextBox12);
            TextRest(TextBox13);
            TextRest(TextBox14);
            Label1.Text = "0";
        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppPages/UniversityPortalMenu.aspx");
        }

        public void TextRest(TextBox t1)
        {
            t1.Text = "0";
        }

        protected void add_Click(object sender, EventArgs e)
        {
            SUMGRADE();
        }

        protected void programs_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(course, programs);
            new Students().GetAcadmicYear(programs, course, Years);
            new Students().GetSamester(Semester, Years, course);
            GetUniBYCourse();
        }



        public void GetReady(TextBox t1, int Assement)
        {
            if (!(t1.Text == null || t1.Text == "0" || 0 == Int32.Parse(t1.Text)))
            {
                int AesstID = GetAssesmentID(Assessments[Assement]);
                string CourID = new Students().GetCoureEnrollID(Semester, course, Years);

                querys.Add("insert into APP_SCORE_DISTRIBUTION (COURSE_ENR_ID,ASSESSMENT_ID,SCORE_VALUE) Values (" + CourID + "," + AesstID + "," + t1.Text + ");");
             
            }
        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().GetAcadmicYear(programs, course, Years);
                new Students().GetSamester(Semester, Years, course);
                GetUniBYCourse();
                
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void Years_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {   
                new Students().GetSamester(Semester, Years, course);
                GetUniBYCourse();
                
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public int GetAssesmentID(string AssessmentName)
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader sdb = db.readOperation("Select CODE_ID from App_CODE where CODE_VALUE = '" + AssessmentName + "'");

                while (sdb.Read())
                {
                    return Convert.ToInt32(sdb[0]);
                }

                return 0;
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Erro", ex);
                return 0;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            SUMGRADE();
        }

        protected void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUniBYCourse();
        }

        public void GetUniBYCourse()
        {
            try
            {
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select t2.UNI_NAME as N from APP_COURSE_ENROLMENT t1 inner join APP_UNIVERSITY t2 on t1.UNIVERSITY_ID = t2.UNI_ID where t1.COURSE_ENROL_ID = " + new Students().GetCoureEnrollID(Semester, course, Years) + "");
                University.Items.Clear();
                while (sdb.Read())
                {
                    University.Items.Add(sdb["N"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}