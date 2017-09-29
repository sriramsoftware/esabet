using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class ScoreInputView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            try
            {
                string Query = "select t1.APP_SCORE_INPUT_ID as ID, t3.STUDENT_NAME as N, t2.ASSESSMENT_NAME as AN, MARKS_OBTAINED as M from APP_SCORE_INPUT t1 inner join APP_SCORE_DESIGN t2 on t1.APP_SCORE_DESIGN_ID = t2.APP_SCORE_DESIGN_ID inner join APP_STUDENT t3 on t1.STUDENT_ID = t3.STUDENT_ID";

                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(Query);
                List<Scoreinput> list = new List<Scoreinput>();
                while (sdb.Read())
                {
                    list.Add(new Scoreinput() { Assessment_Name = sdb["AN"].ToString(), ID = sdb["ID"].ToString(), Marks = sdb["M"].ToString(), Student_Name = sdb["N"].ToString() });
                }

                MainGrid.DataSource = list;
                MainGrid.DataBind();

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

    }
}