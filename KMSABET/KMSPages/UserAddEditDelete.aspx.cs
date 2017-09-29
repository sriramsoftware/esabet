using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class UserAddEditDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false) 
            {
                AppDao cmnDaoObj = new AppDao();
                List<ListItem> moduleTypeList = cmnDaoObj.getCodeListByCodeTypeId(1900);
                removeSuperAdmin(moduleTypeList, 1903);
                moduleTypeDdl.Items.AddRange(moduleTypeList.ToArray());
                
                List<ListItem> userTypeList = cmnDaoObj.getCodeListByCodeTypeId(2000);
                removeSuperAdmin(userTypeList, 2003);
                userTypeDdl.Items.AddRange(userTypeList.ToArray());
                
                AppDao appDaoObj = new AppDao();
                List<AppCourse> appCourseList = appDaoObj.getCourseList();
                courseDdl.DataSource = appCourseList;
                courseDdl.DataTextField = "courseName";
                courseDdl.DataValueField = "courseId";
                courseDdl.DataBind();

            }
        }

        protected void removeSuperAdmin(List<ListItem> listItems, int superAdminValue)
        {
            foreach (ListItem li in listItems)
            {
                if (li.Value == superAdminValue.ToString())
                {
                    listItems.Remove(li); 
                    break;
                }
            }
        }

        protected void b_Click(object sender, EventArgs e)
        {
            AppUser appUserObj = new AppUser();
            appUserObj.username = usernameTb.Text;
            appUserObj.pwd = passwordTb.Text;
            String pwdConfirm = ConfirmPwdTb.Text;
            appUserObj.fullName = fullNameTb.Text;
            appUserObj.email = emailTb.Text;
            appUserObj.module = moduleTypeDdl.SelectedValue;
            appUserObj.userType = userTypeDdl.SelectedValue;
            appUserObj.course = courseDdl.SelectedValue;
            Boolean isError = false;

            if (!appUserObj.pwd.Equals(pwdConfirm)) 
            { 
                confirmPwdError.Text = "Confirm Password did not match.";
                isError = true;
            }

            DBUtils dbUtils1 = new DBUtils();
            SqlDataReader attributeTypeReader1 = dbUtils1.readOperation("SELECT Username FROM Users");
            while (attributeTypeReader1.Read())
            {
                if (attributeTypeReader1[0].ToString().Equals(appUserObj.username))
                {
                    usernameError.Text = "Username already exists";
                    isError = true;
                    break;
                }
            }
            dbUtils1.closeDBConnection();

            if (isError) return;

            AppDao appDaoObj = new AppDao();
            appUserObj.instructorId = appDaoObj.insertInstructor(appUserObj);
            appDaoObj.insertUser(appUserObj);

            GeneralUtils guObj = new GeneralUtils();
            AppEmailData emailDataObj = new AppEmailData();
            emailDataObj.toAddress = appUserObj.email;
            emailDataObj.ccAddress = (new AppDao()).getEmailConfiguration().fromAddress;
            emailDataObj.subject = "Expert System - Account Created";
            emailDataObj.bodyHtml = "Dear " + appUserObj.fullName + ","
                + "<br/><br/>Your account has been successfully created on"
                + " <a href='http://esabet.azurewebsites.net'>Expert System for AEBT</a>"
                + " for the course of " + courseDdl.SelectedItem.Text
                + " with the following credentials:"
                + "<br/><b>Username: </b>" + appUserObj.username
                + "<br/><b>Password: </b>" + appUserObj.pwd
                + "<br/><br/>Please change your password after first login."
                + " If you are not the intended person kindly ignore this email."
                + "<br/><br/>Regards,"
                + "<br/>Admin of Expert System for ABET";
            guObj.sendEmail(emailDataObj);

            Response.Redirect("UserManagement.aspx");

        }

    }
}