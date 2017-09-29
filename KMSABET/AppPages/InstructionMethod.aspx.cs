using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class InstructionMethod : System.Web.UI.Page
    {
        bool Update = false;
        int IDs = 0, Up = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
                bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
                IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
                Up = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["U"]);

                if (Delete)
                {
                    new Connections().DeleteDate("delete from APP_INSTRUCTION_METHOD where INSTRUCTION_METHOD_ID = " + IDs + "");
                    Response.Redirect("~/AppPages/insmedview.aspx");
                }
                else if (Update)
                {
                    if (!IsPostBack)
                        
                        SelectionData();
                }
                else
                {

                    if (!IsPostBack)
                    {
                        new Connections().GetProgram(program);
                        new Students().SelectCourse(Course, program);
                        new Students().GetAcadmicYear(program, Course, Year, 1);
                        new Students().GetSamester(Semsester, Year, Course);
                        new ClOSO().GETCLOs(Clo, Course);
                        GetInsMed(insMethod);
                    }
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error while Get Data", ex);
            }
        }

        private void SelectionData()
        {
            new Connections().GetProgram(program);
            new Students().SelectCourse(Course, program);
            new Students().GetAcadmicYear(program, Course, Year, 1);
            new Students().GetSamester(Semsester, Year, Course);
            new ClOSO().GETCLOs(Clo, Course);
            GetInsMed(insMethod);

            string qu = "select t4.program_name as PN, t2.course_name as CN, ACDEMIC_YEAR as Y, t3.CODE_VALUE as S,(select I_CLO.clo_statement as CLOS from I_CLO join APP_INSTRUCTION_METHOD tt1 on I_CLO.clo_id = tt1.CLO_ID where I_CLO.clo_id = tt1.CLO_ID) as CLOS, (select App_CODE.CODE_VALUE as IT from APP_INSTRUCTION_METHOD tq1 join App_CODE on tq1.INSTRUCTION_ID = App_CODE.CODE_ID) as IT from APP_COURSE_ENROLMENT t1 inner join App_Course t2 on t1.APP_COURSE_ID = t2.course_id inner join App_CODE t3 on t1.SEMESTER = t3.CODE_ID inner join App_Program t4 on t2.App_Program_program_id = t4.program_id where COURSE_ENROL_ID = " + IDs + " and t1.UNIVERSITY_ID = (select UNI_ID from App_Instructor where instructor_id = " + Session["LoginID"].ToString() + " ) ";

            SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(qu);

            while (sdb.Read())
            {
                program.SelectedValue = sdb["PN"].ToString();
                Course.Items.Add(sdb["CN"].ToString()); Course.SelectedIndex = Course.Items.Count - 1;
                Year.Items.Add(sdb["Y"].ToString()); Year.SelectedIndex = Year.Items.Count - 1;
                Semsester.Items.Add(sdb["S"].ToString()); Semsester.SelectedIndex = Semsester.Items.Count - 1;

                Clo.SelectedValue = sdb["CLOS"].ToString();
                insMethod.SelectedValue = sdb["IT"].ToString();
            }

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                string qu = "";
                if (Update)
                {
                    qu = "Update APP_INSTRUCTION_METHOD set COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semsester, Course, Year) + ", INSTRUCTION_ID = " + insMethod.SelectedValue + ", CLO_ID = " + Clo.SelectedValue + " where INSTRUCTION_METHOD_ID = " + Up + ";";
                }
                else
                {
                    qu = "insert into APP_INSTRUCTION_METHOD (COURSE_ENR_ID, INSTRUCTION_ID, CLO_ID) values (" + new Students().GetCoureEnrollID(Semsester, Course, Year) + "," + insMethod.SelectedValue + "," + Clo.SelectedValue + ");";
                }

                int a = new Connections().InsertData(qu);
                if (a == 1)
                {
                    Response.Redirect("~/AppPages/insmedview.aspx");
                }
                else
                {
                    Response.Redirect("~/AppPages/insmedview.aspx");
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Inserted.", ex);
            }
        }

        protected void program_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().SelectCourse(Course, program);
                new Students().GetAcadmicYear(program, Course, Year, 1);
                new Students().GetSamester(Semsester, Year, Course, 1);
                new ClOSO().GETCLOs(Clo, Course);
                
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("error", ex);
            }
        }

        public void GetInsMed(DropDownList dropInsMetd)
        {
            try
            {
                string qu = "select CODE_ID as ID, CODE_VALUE as N from App_CODE where CODE_TYPE_ID = 1800";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(qu);
                dropInsMetd.Items.Clear();
                while (sdb.Read())
                {
                    dropInsMetd.Items.Add(new ListItem() { Text = sdb["N"].ToString(), Value = sdb["iD"].ToString() });
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().GetAcadmicYear(program, Course, Year);
                new Students().GetSamester(Semsester, Year, Course);
                new ClOSO().GETCLOs(Clo, Course);
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().GetSamester(Semsester, Year, Course, 1);
                new ClOSO().GETCLOs(Clo, Course);
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void Semsester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new ClOSO().GETCLOs(Clo, Course);
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void Clo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}