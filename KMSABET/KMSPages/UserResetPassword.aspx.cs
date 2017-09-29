using KMSABET.MyDaos;
using KMSABET.MyPocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class UserResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                usernameTb.Text = Session["LoginID"].ToString();
                if (Session["FIRST_LOGIN"].ToString() == "true")
                {
                    noteId.Visible = true;
                }
                else 
                {
                    noteId.Visible = false; 
                }
            }
        }

        protected void b_Click(object sender, EventArgs e)
        {
            AppUser appUserObj = new AppUser();
            appUserObj.username = usernameTb.Text;
            appUserObj.pwd = passwordTb.Text;
            String pwdConfirm = ConfirmPwdTb.Text;
            Boolean isError = false;

            if (!appUserObj.pwd.Equals(pwdConfirm))
            {
                confirmPwdError.Text = "Confirm Password did not match.";
                isError = true;
            }

            if (isError) return;
            AppDao daoObj = new AppDao();
            daoObj.updatedPassword(appUserObj);

            Session["FIRST_LOGIN"] = false;
            Response.Redirect("PageRedirection.aspx");

        }
    }
}