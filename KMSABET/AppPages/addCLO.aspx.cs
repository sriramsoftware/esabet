using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
   
    public partial class addCLO : System.Web.UI.Page
    {
        bool Update = false;
        int IDs = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                new Connections().GetProgram(Program);
                new Students().SelectCourse(Course, Program);
            }


            Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
            bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
            IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            bool View = Request.QueryString["View"] == null ? false : bool.Parse(Request.QueryString["View"]);

            if (Delete)
            {
                DeleteData();
                Response.Redirect("~/AppPages/CLO.aspx");
            }
            else if (Update)
            {
                btnadd.Text = "Update";

                if (!Page.IsPostBack)
                {
                    SelectionData();
                }

            }
            else if (View)
            {
                Program.Enabled = false;
                Course.Enabled = false;                
                btnadd.Visible = false;
                clos.Enabled = false;
                SelectionData();                
            }
            else
            {
                btnadd.Text = "Submit";
            }

        }

        private void SelectionData()
        {
            try
            {
                string qu = "select t1.clo_statement as CLOS, t3.program_name as PN, t2.course_name as CN from I_CLO t1 join App_Course t2 on t1.App_Course_course_id = t2.course_id join App_Program t3 on t2.App_Program_program_id = t3.program_id where clo_id = " + IDs + "";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(qu);

                while (sdb.Read())
                {
                    Program.SelectedValue = sdb["PN"].ToString();
                    Course.Items.Add(sdb["CN"].ToString()); Course.SelectedIndex = Course.Items.Count - 1;
                    clos.Text = sdb["CLOS"].ToString();
                    break;
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        private void DeleteData()
        {
            try
            {
                string qu = "delete from I_CLO where clo_id = " + IDs + "";
                int a = new Connections().DeleteDate(qu);

                if (a == 1)
                {
                    Response.Redirect("~/AppPages/CLO.aspx");
                }
                else
                {
                    Response.Redirect("~/AppPages/CLO.aspx");
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Erorr", ex);
            }
        }

        protected void Program_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Connections().GetProgram(Program);
            new Students().SelectCourse(Course, Program);
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                string qu = "";

                if (Update)
                {
                    qu = "update I_CLO set App_Course_course_id = (select course_id from App_Course where App_Program_program_id = (select program_id from App_Program where program_name = '" + Program.SelectedValue + "') and course_name = '" + Course.SelectedValue + "'), clo_statement = '" + clos.Text + "' where clo_id = " + IDs + "";
                }
                else
                {
                    qu = "insert into I_CLO (App_Course_course_id,clo_statement) values ((select course_id from App_Course where App_Program_program_id = (select program_id from App_Program where program_name = '" + Program.SelectedValue + "') and course_name = '" + Course.SelectedValue + "'),'" + clos.Text + "');";
                }

                int a = new Connections().InsertData(qu);

                if (a == 1)
                {
                    Response.Redirect("~/AppPages/CLO.aspx");
                }
                else
                {
                    Response.Redirect("~/AppPages/CLO.aspx");
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Insert ClO", ex);
                Response.Redirect("~/AppPages/CLO.aspx");
            }
        }
    }
}