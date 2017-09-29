using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class UniversityPortalMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Instrutor"] != null)
            {
                Instructor.Text += Session["Instrutor"].ToString();
            }
            else
            {
                Response.Redirect("Authentication.aspx");
            }
        }
    }
}