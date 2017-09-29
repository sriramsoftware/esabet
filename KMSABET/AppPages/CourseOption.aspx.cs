using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CourseOption : System.Web.UI.Page
    {
        bool Update;
        int IDs = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
                bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
                IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);

                if (Delete)
                {
                    new Connections().DeleteDate("delete from App_Course where course_id = " + IDs + "");
                    Response.Redirect("~/AppPages/CoursesViews.aspx");
                }
                else if (Update)
                {
                    if (!IsPostBack)
                        SelectionData();
                }
                else
                {
                    new Connections().GetProgram(Program);

                    SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select Code_Value as CT from App_CODE where CODE_TYPE_ID = 1700");
                    CT.Items.Clear();
                    while (sdb.Read())
                    {
                        CT.Items.Add(sdb["CT"].ToString());
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SelectionData()
        {
            try
            {
                new Connections().GetProgram(Program);

                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select course_id as ID, t2.program_name as PN, course_name as CN, COURSE_NUMBER as CNU, t3.CODE_VALUE as CT, THEORY_CREDIT_HOURS as TCRH, LAB_CREDIT_HOURS as LCRH, THEORY_CONTACT_HOURS as TCH, LAB_CONTACT_HOURS as LCH from App_Course t1 inner join App_Program t2 on t1.App_Program_program_id = t2.program_id inner join App_CODE t3 on t1.COURSE_TYPE = t3.CODE_ID  where course_id = " + IDs + "");
                while (sdb.Read())
                {
                    Program.Items.Add(sdb["PN"].ToString()); Program.SelectedIndex = Program.Items.Count - 1;
                    Course.Text = sdb["CN"].ToString();
                    CNU.Text = sdb["CNU"].ToString();
                    CT.Items.Add(sdb["CT"].ToString()); CT.SelectedIndex = CT.Items.Count - 1;
                    TCRH.Text = sdb["TCRH"].ToString();
                    TCH.Text = sdb["TCH"].ToString();
                    LCH.Text = sdb["LCH"].ToString();
                    LCRH.Text = sdb["LCRH"].ToString();
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Selecting Data", ex);
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            try
            {
                string q = "";
                if (!Update)
                {
                    q = "insert into App_Course (App_Program_program_id,course_name,COURSE_NUMBER,COURSE_TYPE,THEORY_CREDIT_HOURS,LAB_CREDIT_HOURS,THEORY_CONTACT_HOURS,LAB_CONTACT_HOURS) values ((select program_id from App_Program where program_name = '" + Program.SelectedValue + "'),'" + Course.Text + "','" + CNU.Text + "',(select Code_ID from APP_Code where CODE_VALUE = '" + CT.Text + "'),'" + TCRH.Text + "','" + LCRH.Text + "','" + TCH.Text + "','" + LCH.Text + "');";
                }
                else
                {

                    q = "update App_Course set App_Program_program_id = (select program_id from App_Program where program_name = '" + Program.SelectedValue + "'),course_name = '" + Course.Text + "',COURSE_NUMBER = '" + CNU.Text + "',COURSE_TYPE = (Select Code_ID from App_Code where Code_Value = '" + CT.SelectedItem + "'),THEORY_CREDIT_HOURS = '" + TCRH.Text + "',LAB_CREDIT_HOURS = '" + LCRH.Text + "',THEORY_CONTACT_HOURS = '" + TCH.Text + "',LAB_CONTACT_HOURS = '" + LCH.Text + "' where course_id = " + IDs + "";
                }

                int a = new Connections().InsertData(q);

                if (a == 1)
                {
                    Response.Redirect("~/AppPages/CoursesViews.aspx");
                }
                else
                {
                    MyUtilities.LogUtils.myLog.Error("Error While Inserting " + q);
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Inserting or Updating Data", ex);
            }
        }
    }
}