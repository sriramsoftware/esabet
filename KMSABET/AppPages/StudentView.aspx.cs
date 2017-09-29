using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class StudentView : System.Web.UI.Page
    {   
        bool Update = false;
        int IDs = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                GetProgram();
                SelectCourse(AdSCour, AdSPro);
                GetAcadmicYear(AdSPro, AdSCour, AdSAYear);
                GetSamester(AdSSem, AdSAYear, AdSCour);
            }

            Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
            bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
            IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            bool Edit = Request.QueryString["Edit"] == null ? false : bool.Parse(Request.QueryString["Edit"]);

            if (Delete)
            {
                DeleteData();
                Response.Redirect("~/AppPages/Students.aspx");
            }
            else if (Update)
            {
                GetProgram();

                if (Page.IsPostBack == false)
                {
                    SelectionData();                    
                }

                Button1.Visible = false;
                Updates.Visible = true;

            }
            else if (Edit)
            {
                SelectionData();
                AdSPro.Enabled = false;
                AdSAYear.Enabled = false;
                AdSSem.Enabled = false;
                AdSCour.Enabled = false;

                StudentNameTxt.Enabled = false;
                StudentrollTxt.Enabled = false;

                Button1.Visible = false;

            }
            else
            {   
                Button1.Visible = true;
                Updates.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AddStudentData();
        }

        public void UpdateData()
        {
            try
            {
                int res = new Connections().InsertData("update dbo.APP_STUDENT set STUDENT_ID = " + StudentrollTxt.Text + " , STUDENT_NAME = '" + StudentNameTxt.Text + "' , COURSE_ENR_ID = " + new Students().GetCoureEnrollID(AdSSem,AdSCour,AdSAYear) + " where STUDENT_ID_PK = " + IDs + ";");
                string aaa = new Students().GetCoureEnrollID(AdSSem, AdSCour, AdSAYear);
                
                if (res == 1)
                {
                    Response.Redirect("~/AppPages/Students.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Error Occur While Update Student");
                }


            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Updating", ex);
            }
        }

        public void GetProgram()
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader sdb = db.readOperation(@"select program_name as PN from App_Program");

                while (sdb.Read())
                {
                    ListItem li = new ListItem();
                    li.Text = sdb["PN"].ToString();
                    li.Value = sdb["PN"].ToString();

                    AdSPro.Items.Add(li);
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public void AddStudentData()
        {
            try
            {
                string Studentid = StudentrollTxt.Text;
                string StudentName = StudentNameTxt.Text;
                
                string qu = @"Declare @a int; Declare @b int; Declare @c int;
                        select @b = CODE_ID from App_CODE where CODE_VALUE = '" + AdSSem.SelectedValue + "';" +
                        "select @c = course_id from App_Course where course_name = '" + AdSCour.SelectedValue + "';" +
                        "select @a = COURSE_ENROL_ID from APP_COURSE_ENROLMENT where ACDEMIC_YEAR = '" + AdSAYear.SelectedValue + "' and SEMESTER = @b and APP_COURSE_ID = @c;" +
                        "insert into APP_STUDENT (STUDENT_ID,STUDENT_NAME,COURSE_ENR_ID) values ('" + StudentrollTxt.Text + "','" + StudentNameTxt.Text + "',@a);";
                Response.Write(qu);

                int a = new Connections().InsertData(qu);

                if (a == 1)
                {
                    Response.Redirect("~/AppPages/Students.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Error Occure While Add Student");
                }

                
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Add Student", ex);
            }
        }

        protected void AdSPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCourse(AdSCour, AdSPro);            
            GetAcadmicYear(AdSPro, AdSCour, AdSAYear);
            GetSamester(AdSSem, AdSAYear, AdSCour);
        }

        protected void AdSCour_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAcadmicYear(AdSPro, AdSCour, AdSAYear);
            GetSamester(AdSSem, AdSAYear, AdSCour);
        }

        protected void AdSAYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSamester(AdSSem, AdSAYear,AdSCour);
            
        }

        public void SelectCourse(DropDownList DropSet, DropDownList Dropget)
        {
            try
            {
                DropSet.Items.Clear();
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader res = db.readOperation(@"declare @a int select @a = program_id from App_Program where program_name = '" + Dropget.SelectedValue + "'; select (course_name) as Course from App_Course where App_Program_program_id = @a;");

                while (res.Read())
                {
                    ListItem listItem = new ListItem();

                    listItem.Text = res["Course"].ToString();
                    listItem.Value = res["Course"].ToString();

                    DropSet.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Exception :", ex);
            }
        }

        public void GetSamester(DropDownList dropSemester, DropDownList dropAcadmicyear, DropDownList dropCourse)
        {
            try
            {
                if (dropAcadmicyear.SelectedIndex != -1)
                {

                    string qus = @"select t3.CODE_VALUE from APP_COURSE_ENROLMENT t1 inner join App_Course t2 on t1.APP_COURSE_ID = t2.course_id "
                    + "inner join App_CODE t3 on t1.SEMESTER = t3.CODE_ID "
                    + "where t1.ACDEMIC_YEAR = " + dropAcadmicyear.SelectedValue + " and t2.course_name = '" + dropCourse.SelectedValue + "';";

                    dropSemester.Items.Clear();

                    MyUtilities.DBUtils dbs = new MyUtilities.DBUtils();
                    SqlDataReader sdr1 = dbs.readOperation(qus);
                    
                    while (sdr1.Read())
                    {
                        ListItem li = new ListItem();
                        li.Value = sdr1["CODE_VALUE"].ToString();
                        li.Text = sdr1["CODE_VALUE"].ToString();

                        dropSemester.Items.Add(li);
                    }
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public void GetAcadmicYear(DropDownList dropProgram, DropDownList dropCourse, DropDownList dropAcadmicYear)
        {
            try
            {
                if (dropCourse.SelectedIndex != -1)
                {

                    string qu = @"
                    Declare @a int;
                    Declare @b int;
                    Declare @c int;

                    Select @a = program_id from App_Program where App_Program.program_name = '" + dropProgram.SelectedValue + "';"
                    + "select @b = course_id from App_Course where course_name = '" + dropCourse.SelectedValue + "';"
                    + "select distinct ACDEMIC_YEAR from APP_COURSE_ENROLMENT where APP_COURSE_ID = @b;";
                    
                    dropAcadmicYear.Items.Clear();

                    MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                    SqlDataReader sdr = db.readOperation(qu);
                    
                    while (sdr.Read())
                    {
                        ListItem li = new ListItem();
                        li.Text = sdr["ACDEMIC_YEAR"].ToString();
                        li.Value = sdr["ACDEMIC_YEAR"].ToString();

                        dropAcadmicYear.Items.Add(li);
                    }
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public void DeleteData()
        {
            try
            {
                int res = new Connections().DeleteDate("delete from APP_STUDENT where STUDENT_ID_PK = " + IDs + "");

                if (res == 1)
                {
                    Response.Redirect("~/AppPages/Students.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Error While Student Delete");
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error :", ex);
            }
        }

        public void SelectionData()
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader res = db.readOperation(@"select t1.STUDENT_ID as STDID,t1.STUDENT_NAME as STDN,t2.ACDEMIC_YEAR as ACAD,t3.course_name as CN,t4.CODE_VALUE as SM, t5.program_name as PN from APP_STUDENT t1 inner join APP_COURSE_ENROLMENT t2 on t1.COURSE_ENR_ID = t2.COURSE_ENROL_ID inner join App_Course t3 on t2.APP_COURSE_ID = t3.course_id inner join App_CODE t4 on t2.SEMESTER = t4.CODE_ID inner join App_Program t5 on t3.App_Program_program_id = t5.program_id where t1.STUDENT_ID_PK = " + IDs + "");

                while (res.Read())
                {
                    AdSPro.Items.Add(new ListItem() { Text = res["PN"].ToString(), Value = res["PN"].ToString() });
                    AdSCour.Items.Add(new ListItem() { Text = res["CN"].ToString(), Value = res["CN"].ToString() });
                    AdSAYear.Items.Add(new ListItem() { Value = res["ACAD"].ToString(), Text = res["ACAD"].ToString() });
                    AdSSem.Items.Add(new ListItem() { Text = res["SM"].ToString(), Value = res["SM"].ToString() });

                    StudentNameTxt.Text = res["STDN"].ToString();
                    StudentrollTxt.Text = res["STDID"].ToString();
                }

                AdSPro.SelectedIndex = AdSPro.Items.Count - 1;
                AdSCour.SelectedIndex = AdSCour.Items.Count - 1;
                AdSSem.SelectedIndex = AdSSem.Items.Count - 1;
                AdSAYear.SelectedIndex = AdSAYear.Items.Count - 1;

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error:", ex);                
            }

        }

        protected void Update_Click(object sender, EventArgs e)
        {            
            UpdateData();
            Response.Redirect("~/AppPages/Students.aspx");
        }

    }
}