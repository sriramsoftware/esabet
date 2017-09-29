using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class insmedview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new Connections().GetProgram(program);
                new Students().SelectCourse(Course, program);
                new Students().GetAcadmicYear(program, Course, Year, 1);
                new Students().GetSamester(Semsester, Year, Course);
                new ClOSO().GETCLOs(Clo, Course);
                Genrate(grid);

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
                Genrate(grid);
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("error", ex);
            }
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                new Students().GetAcadmicYear(program, Course, Year);
                new Students().GetSamester(Semsester, Year, Course);
                new ClOSO().GETCLOs(Clo, Course);
                Genrate(grid);
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
                Genrate(grid);
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
                Genrate(grid);
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        protected void Clo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Genrate(grid);
        }

        public void Genrate(GridView grids)
        {
            string qu = "select INSTRUCTION_METHOD_ID as ID,COURSE_ENR_ID as CID, t3.CODE_VALUE as IID, t2.clo_statement as S from APP_INSTRUCTION_METHOD t1 inner join I_CLO t2 on t1.CLO_ID = t2.clo_id inner join App_CODE t3 on t1.INSTRUCTION_ID = t3.CODE_ID where t1.CLO_ID = " + Clo.SelectedValue + " and COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semsester, Course, Year) + "";
            SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(qu);

            List<insMethod> list = new List<insMethod>();

            while (sdb.Read())
            {
                list.Add(new insMethod() { ID = sdb["ID"].ToString(), CID = sdb["CID"].ToString(), Clo = sdb["S"].ToString(), IID = sdb["IID"].ToString() });
            }

            grids.DataSource = list;
            grids.DataBind();
        }

    }
}