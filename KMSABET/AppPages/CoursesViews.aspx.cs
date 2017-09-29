using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CoursesViews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Courses> list = new List<Courses>();
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select course_id as ID, t2.program_name as PN, course_name as CN, COURSE_NUMBER as CNU, t3.CODE_VALUE as CT, THEORY_CREDIT_HOURS as TCRH, LAB_CREDIT_HOURS as LCRH, THEORY_CONTACT_HOURS as TCH, LAB_CONTACT_HOURS as LCH from App_Course t1 inner join App_Program t2 on t1.App_Program_program_id = t2.program_id inner join App_CODE t3 on t1.COURSE_TYPE = t3.CODE_ID ");
                while (sdb.Read())
                {
                    list.Add(new Courses() { ID = sdb["ID"].ToString(), PN = sdb["PN"].ToString(), CN = sdb["CN"].ToString(), CNU = sdb["CNU"].ToString(), CT = sdb["CT"].ToString(), LCHOURS = sdb["LCH"].ToString(), LCRHOURS = sdb["LCRH"].ToString(), TCHOURS = sdb["TCH"].ToString(), TCRHOURS = sdb["TCRH"].ToString() });
                }

                MainGrid.DataSource = list;
                MainGrid.DataBind();

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Gatting Data", ex);
            }
        }
    }
}