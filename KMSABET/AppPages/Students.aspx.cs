using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(Program);
                SelectCourse(Course, Program);
                GetAcadmicYear(Program, Course, AcadmicYears);
                GetSamester(ViewSemester, AcadmicYears, Course);
            }
        }

      
        private void GetAllStd(string a)
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();

                SqlDataReader sdb = db.readOperation(a);
                List<App_Student> list = new List<App_Student>();

                while (sdb.Read())
                {
                    App_Student info = new App_Student() { CourseID = sdb["Course Enroll ID"].ToString(), StudentID = sdb["Student ID PK"].ToString(), StudentRoll = sdb["Student ID"].ToString(), StudentName = sdb["Student Name"].ToString() };
                    list.Add(info);
                }

                Student.DataSource = list;
                Student.DataBind();
                
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
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

        protected void Program_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCourse(Course, Program);
            GetAcadmicYear(Program, Course, AcadmicYears);
            GetSamester(ViewSemester, AcadmicYears, Course);
            ViewStudentBySemester();
        }

       
        

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAcadmicYear(Program, Course, AcadmicYears);            
        }

        protected void AcadmicYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSamester(ViewSemester, AcadmicYears, Course);
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

        public void GetSamester(DropDownList dropSemester, DropDownList dropAcadmicyear, DropDownList dropCourse, int a)
        {
            try
            {
                if (dropAcadmicyear.SelectedIndex != -1)
                {
                    string qus = @"select t3.CODE_VALUE from APP_COURSE_ENROLMENT t1 inner join App_Course t2 on t1.APP_COURSE_ID = t2.course_id "
                    + "inner join App_CODE t3 on t1.SEMESTER = t3.CODE_ID "
                    + "where t1.ACDEMIC_YEAR = " + dropAcadmicyear.SelectedValue + " and t2.course_name = '" + dropCourse.SelectedValue + "'  and t1.UNIVERSITY_ID = (select UNIVERSITY_ID from App_Instructor where instructor_id = " + Session["LoginID"] + ");";

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

        public void GetAcadmicYear(DropDownList dropProgram, DropDownList dropCourse, DropDownList dropAcadmicYear, int uni)
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
                    + "select distinct ACDEMIC_YEAR from APP_COURSE_ENROLMENT where APP_COURSE_ID = @b and UNIVERSITY_ID = (select UNIVERSITY_ID from App_Instructor where instructor_id = " + Session["LoginID"] + ");";

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
      
        protected void ViewSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewStudentBySemester();
        }

        public void ViewStudentBySemester()
        {
            try
            {
                if (ViewSemester.SelectedIndex != -1)
                {
                    string a = @"select t1.COURSE_ENROL_ID as CEID from APP_COURSE_ENROLMENT t1 inner join App_Course t2 on t1.APP_COURSE_ID = t2.course_id inner join App_CODE t3 on t1.SEMESTER = t3.CODE_ID where t1.ACDEMIC_YEAR = " + AcadmicYears.SelectedValue + " and t2.course_name = '" + Course.SelectedValue + "' and t3.CODE_VALUE = '" + ViewSemester.SelectedValue + "'";

                    MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                    SqlDataReader sdr = db.readOperation(a);
                    string enrollId = "";
                    while (sdr.Read())
                    {
                        enrollId = sdr["CEID"].ToString();
                    }

                    GetAllStd("Select STUDENT_ID_PK as 'Student ID PK',STUDENT_ID as 'Student ID', STUDENT_NAME as 'Student Name',COURSE_ENR_ID as 'Course Enroll ID' from APP_STUDENT where COURSE_ENR_ID = " + enrollId + "");
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error:", ex);
            }
        }

        public string GetCoureEnrollID(DropDownList dropSems, DropDownList dropCourse, DropDownList dropAcadmicyear)
        {
            try
            {
                if (dropSems.SelectedIndex != -1)
                {
                    string a = @"Declare @x int; Declare @y int; Declare @z int; " +
                    "select @y = CODE_ID from App_CODE where CODE_VALUE = '" + dropSems.SelectedValue + "';" +
                    "select @z = course_id from App_Course where course_name = '" + dropCourse.SelectedValue + "';" +
                    "select @x = COURSE_ENROL_ID from APP_COURSE_ENROLMENT where ACDEMIC_YEAR = '" + dropAcadmicyear.SelectedValue + "' and SEMESTER = @y and APP_COURSE_ID = @z;" +
                    "select @x as 'CEID';select @y,@x,@z";
                    
                    MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                    SqlDataReader sdr = db.readOperation(a);
                    string enrollId = "";
                    
                    while (sdr.Read())
                    {
                        enrollId = sdr["CEID"].ToString();
                    }

                    return enrollId;
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error:", ex);
                return "";
            }
        }

        protected void Student_Load(object sender, EventArgs e)
        {
            GetAllStd(@"SELECT STUDENT_ID_PK as 'Student ID PK',STUDENT_ID as 'Student ID', STUDENT_NAME as 'Student Name',COURSE_ENR_ID as 'Course Enroll ID' FROM APP_STUDENT");
        }

        protected void NewRecord_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppPages/StudentView.aspx");
        }

    }
}