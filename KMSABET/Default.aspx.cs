using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMSABET.MyUtilities;

namespace KMSABET
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/KMSPages/PageRedirection.aspx", false);
            //Testing Material for logging and DB operations are as follows:
            /*LogUtils.myLog.Debug("Default page is being loaded.");

            DBUtils dbUtils = new DBUtils();
            int rowsAffected = dbUtils.CUDOperations("insert into courses (title, credits) " +
                                     "values ('bio informatics1', 3)");

            LogUtils.myLog.Debug("Number of rows inserted = " + rowsAffected);*/
        }
    }
}