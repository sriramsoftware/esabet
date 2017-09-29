using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CourseViewData : System.Web.UI.Page
    {
        public static int Values = 1;
        bool Update = false;
        int IDs = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(program);
                SelectPrograms();
                new Connections().GetSemester(Semester);
            }

            if (Session["Instrutor"] != null)
                TextBox1.Text = Session["Instrutor"].ToString();

            Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
            bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
            IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            bool View = Request.QueryString["View"] == null ? false : bool.Parse(Request.QueryString["View"]);

            if (Delete)
            {
                DeleteData();
                Response.Redirect("~/AppPages/Course.aspx");
            }
            else if (Update)
            {
                Button1.Text = "Submit";

                if (Values == 1)
                {

                    SelectionData();
                    Values = 2;
                    Button1.Text = "Update";
                }
                else
                {
                    Values = 1;
                }

            }
            else if (View)
            {
                program.Enabled = false;
                DropDownList1.Enabled = false;
                acadmicyear.Enabled = false;
                Semester.Enabled = false;
                Button1.Visible = false;
                SelectionData();
                unis.Enabled = false;
                Cancel.Text = "Close";
            }
            else
            {
                Button1.Text = "Submit";
            }

            MyUtilities.DBUtils db = new MyUtilities.DBUtils();
            SqlDataReader sd = db.readOperation("select UNI_NAME as N from APP_UNIVERSITY where UNI_ID = (select UNI_ID from App_Instructor where instructor_id = " + Session["LoginID"] + ");");

            while (sd.Read())
            {
                unis.Items.Add(sd["N"].ToString());
            }
        }
        

        protected void Instructor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void program_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectPrograms();
        }

        public void SelectPrograms()
        {
            try
            {
                DropDownList1.Items.Clear();
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader res = db.readOperation(@"declare @a int select @a = program_id from App_Program where program_name = '" + program.SelectedValue + "'; select (course_name) as Course from App_Course where App_Program_program_id = @a;");

                while (res.Read())
                {
                    ListItem listItem = new ListItem();

                    listItem.Text = res["Course"].ToString();
                    listItem.Value = res["Course"].ToString();

                    DropDownList1.Items.Add(listItem);
                }
                              

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Exception :", ex);
            }
        }

        public void InsertData()
        {
            string query = null;
            try
            {
                Connections conn = new Connections();
                
                if (!(DropDownList1.SelectedIndex == -1 || DropDownList1.SelectedIndex == -1 || acadmicyear.SelectedIndex == -1 || Semester.SelectedIndex == -1))
                {
                    query = "declare @a int; declare @b int; declare @c int; select @a = course_id from App_Course where course_name = '" + DropDownList1.SelectedValue + "'; select @c = CODE_ID from App_CODE where CODE_VALUE = '" + Semester.SelectedValue + "'; insert into APP_COURSE_ENROLMENT (APP_COURSE_ID,ACDEMIC_YEAR,SEMESTER,INSTRUCTOR_ID,UNIVERSITY_ID) values (@a," + acadmicyear.SelectedValue + ",@c," + Session["LoginID"] + ", (select UNI_ID from App_Instructor where instructor_id = " + Session["LoginID"] + ") );";
                    
                    int res = conn.InsertData(query);

                    if (res == 1)
                    {
                        Response.Redirect("~/AppPages/Course.aspx");
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Course View Data Error While Insert Data" + query, ex);
            }
        }

        public void UpdateData()
        {
            Connections conn = new Connections();

            if (!(DropDownList1.SelectedIndex == -1 || DropDownList1.SelectedIndex == -1 || acadmicyear.SelectedIndex == -1 || Semester.SelectedIndex == -1))
            {
                string query = "Update APP_COURSE_ENROLMENT set APP_COURSE_ID = (select course_id from App_Course where course_name = '" + DropDownList1.SelectedValue + "'), ACDEMIC_YEAR = '" + acadmicyear.SelectedValue + "',SEMESTER = (select CODE_ID from App_CODE where CODE_VALUE = '" + Semester.SelectedValue + "'), UNIVERSITY_ID = (select UNI_ID from APP_UNIVERSITY where UNI_NAME = '" + unis.SelectedValue + "') where COURSE_ENROL_ID = " + IDs + ";";
                
                int res = conn.InsertData(query);

                if (res == 1)
                {   
                    Response.Redirect("~/AppPages/Course.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Course View Data Error While Update Data" + query);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Update)
            {
                UpdateData();
                
            }
            else
            {
                InsertData();
            }
        }
        public void SelectionData()
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader res = db.readOperation(@"select * from APP_COURSE_ENROLMENT where COURSE_ENROL_ID = " + IDs + ";");

                string insid = "";
                string couid = "";
                string sems = "";

                while (res.Read())
                {
                    acadmicyear.Items.Add(res["ACDEMIC_YEAR"].ToString());
                    
                    acadmicyear.SelectedIndex = acadmicyear.Items.Count - 1;
                    

                    insid = res["INSTRUCTOR_ID"].ToString();
                    couid = res["APP_COURSE_ID"].ToString();
                    sems = res["SEMESTER"].ToString();
                }



                MyUtilities.DBUtils dbs = new MyUtilities.DBUtils();
                SqlDataReader ress = dbs.readOperation(@"Declare @a varchar(50); Declare @aa varchar(50); Declare @b varchar(50); declare @c varchar(50); select @a = course_name, @aa = App_Program_program_id from App_Course where course_id = " + couid + "; select @b = CODE_VALUE from App_CODE where CODE_ID = " + sems + "; select @c =instructor_name from App_Instructor where instructor_id = " + insid + "; select @aa = program_name from App_Program where program_id = @aa; select @a as 'CourseName',@b as 'Semester', @c as 'InsName', @aa as PID");

                while (ress.Read())
                {
                    //TextBox1.Text = ress["InsName"].ToString();

                    program.SelectedValue = ress["PID"].ToString();
                    ListItem li = new ListItem();
                    li.Text = ress["CourseName"].ToString();
                    li.Value = ress["CourseName"].ToString();

                    DropDownList1.Items.Add(li);
                    DropDownList1.SelectedIndex = DropDownList1.Items.Count - 1;

                    li = new ListItem();

                    li.Text = ress["Semester"].ToString();
                    li.Value = ress["Semester"].ToString();

                    Semester.Items.Add(li);
                    Semester.SelectedIndex = Semester.Items.Count - 1;

                }

                SqlDataReader sd = db.readOperation("select UNI_NAME as N from APP_UNIVERSITY where UNI_ID = (select UNI_ID from App_Instructor where instructor_id = " + Session["LoginID"] + ");");

                while (sd.Read())
                {
                    unis.Items.Add(sd["N"].ToString());
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error:", ex);
            }

        }

        public void DeleteData()
        {
            try
            {
                Connections con = new Connections();
                int a = con.DeleteDate("update APP_STUDENT set COURSE_ENR_ID = null where COURSE_ENR_ID = " + IDs + "; delete from APP_COURSE_ENROLMENT where APP_COURSE_ENROLMENT.COURSE_ENROL_ID = " + IDs + "");

                if (a == 1)
                {   
                    Response.Redirect("~/AppPages/Course.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Course View Data Error While Delete Data");
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error:", ex);
            }
        }

        protected void acadmicyear_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                
                try
                {
                    for (int i = DateTime.Now.Year; i > 2000; i--)
                    {
                        acadmicyear.Items.Add(new ListItem() { Text = i.ToString(), Value = i.ToString() });
                    }
                }
                catch (Exception ex)
                {
                    MyUtilities.LogUtils.myLog.Error("Error:", ex);
                }
            }
        }

    }
}