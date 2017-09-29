using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CLO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                new Connections().GetProgram(Program);
                new Students().SelectCourse(Course, Program);
                gridpop();
            }
        }

        public void gridpop()
        {
            try
            {
                string qu = "select clo_id as ID, t2.course_name as CN, clo_statement as ST  from I_CLO t1 inner join App_Course t2 on t1.App_Course_course_id = t2.course_id where t2.course_name = '" + Course.Text + "'";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(qu);
                List<ICLO> list = new List<ICLO>();

                while (sdb.Read())
                {
                    list.Add(new ICLO() { ID = sdb["ID"].ToString(), Clo_statement = sdb["St"].ToString(), Course = sdb["CN"].ToString() });
                }

                GridView1.DataSource = list;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While getting Data", ex);
            }
        }


        protected void Program_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(Course, Program);
            gridpop();
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridpop();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }
    }
}