using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class LogoutActions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["uniId"] = null;
            Session["departId"] = null;
            Session["courseId"] = null;
            Session["uniName"] = null;
            Session["departName"] = null;
            Session["courseName"] = null;
            Session["username"] = null;
            Session["userId"] = null;
            Session["userTypeId"] = null;
            Session["expTypeId"] = null;

            Response.Redirect("/KMSPages/PageRedirection.aspx");
        }
    }
}