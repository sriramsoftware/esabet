using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class PageRedirection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            NameValueCollection nvc = Request.Form;
            insertIntoSession("uniId", nvc);
            insertIntoSession("departId", nvc);
            insertIntoSession("uniName", nvc);
            insertIntoSession("departName", nvc);
            insertIntoSession("courseName", nvc);
            insertIntoSession("userId", nvc);
            insertIntoSession("userTypeId", nvc);
            insertIntoSession("expTypeId", nvc);
            insertIntoSession("username", nvc);
            insertIntoSession("password", nvc);

            if (Session["username"] != null && Session["password"] != null)
            {

                AppDao appDaoObj = new AppDao();
                AppUser appUser = appDaoObj.validateUser(Session["username"].ToString(), Session["password"].ToString());
                Session["courseId"] = appUser.courseId;
                Session["courseName"] = appUser.course;
            
                if (appUser.username != null)
                {
                    Session["loginStatus"] = true;
                    if (appUser.instructorTypeId.Equals(2001))
                        Session["userTypeId"] = "1";
                    else if (appUser.instructorTypeId.Equals(2002)) 
                        Session["userTypeId"] = "2";
                    else
                        Session["userTypeId"] = "3";

                    if (appUser.expertSystemTypeId.Equals(1901))
                        Session["expTypeId"] = "1";
                    else if (appUser.expertSystemTypeId.Equals(1902)) 
                        Session["expTypeId"] = "2";
                    else
                        Session["expTypeId"] = "3";

                    if (Session["userTypeId"] != null && Session["userTypeId"].ToString() == "1") //Admin
                    {
                        if (Session["expTypeId"].ToString() == "1") //QUESTION BANK
                        {
                            //FormsAuthentication.RedirectFromLoginPage(username.Text, true);

                            List<NameValueCollection> menuItems = new List<NameValueCollection>();

                            NameValueCollection nvc1 = new NameValueCollection();

                            nvc1["href"] = "QueQuestionAddEditView.aspx";
                            nvc1["title"] = "Add/Edit Question";
                            menuItems.Add(nvc1);

                            nvc1 = new NameValueCollection();
                            nvc1["href"] = "QueQuestionList.aspx";
                            nvc1["title"] = "View All Questions";
                            menuItems.Add(nvc1);

                            nvc1 = new NameValueCollection();
                            nvc1["href"] = "QueQuestionAddUpload.aspx";
                            nvc1["title"] = "Bulk Add Questions";
                            menuItems.Add(nvc1);

                            nvc1 = new NameValueCollection();
                            nvc1["href"] = "QueAttributeList.aspx";
                            nvc1["title"] = "Question Attributes";
                            menuItems.Add(nvc1);

                            Session["menuItems"] = menuItems;
                            Response.Redirect("/KMSPages/QueQuestionAddEditView");

                            //FormsAuthenticationTicket tkt;
                            //string cookiestr;
                            //HttpCookie ck;
                            //tkt = new FormsAuthenticationTicket(1, username.Text, DateTime.Now,
                            //    DateTime.Now.AddMinutes(30), true, "your custom data");
                            //cookiestr = FormsAuthentication.Encrypt(tkt);
                            //ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                            //if (true)
                            //    ck.Expires = tkt.Expiration;
                            //ck.Path = FormsAuthentication.FormsCookiePath;
                            //Response.Cookies.Add(ck);

                            //string strRedirect;
                            //strRedirect = Request["ReturnUrl"];
                            //if (strRedirect == null)
                            //    strRedirect = "~/default.aspx";
                            //Response.Redirect(strRedirect, true);
                        }
                        else
                        {
                            List<NameValueCollection> menuItems = new List<NameValueCollection>();

                            NameValueCollection nvc1 = new NameValueCollection();

                            nvc1["href"] = "ImpAskSuggestionOne.aspx";
                            nvc1["title"] = "Improvement Plan";
                            menuItems.Add(nvc1);

                            nvc1 = new NameValueCollection();
                            nvc1["href"] = "ImpRuleList.aspx";
                            nvc1["title"] = "Rules";
                            menuItems.Add(nvc1);

                            nvc1 = new NameValueCollection();
                            nvc1["href"] = "ImpRuleQuestionList.aspx";
                            nvc1["title"] = "Rule Questions";
                            menuItems.Add(nvc1);

                            Session["menuItems"] = menuItems;
                            Response.Redirect("/KMSPages/ImpRuleList.aspx");
                        }
                    }
                    else if (Session["userTypeId"] != null && Session["userTypeId"].ToString() == "2") //Instructor
                    {
                        if (Session["expTypeId"].ToString() == "1") //QUESTION BANK
                        {
                            //FormsAuthentication.RedirectFromLoginPage(username.Text, true);
                            List<NameValueCollection> menuItems = new List<NameValueCollection>();

                            NameValueCollection nvc1 = new NameValueCollection();

                            nvc1["href"] = "QueFavoriteListAddEdit.aspx";
                            nvc1["title"] = "Start New/Update Assessment";
                            menuItems.Add(nvc1);

                            nvc1 = new NameValueCollection();
                            nvc1["href"] = "QueQuestionList.aspx";
                            nvc1["title"] = "View All Questions";
                            menuItems.Add(nvc1);

                            nvc1 = new NameValueCollection();
                            nvc1["href"] = "QueFavQuestionList.aspx";
                            nvc1["title"] = "View/Edit Assessments";
                            menuItems.Add(nvc1);

                            Session["menuItems"] = menuItems;
                            Response.Redirect("/KMSPages/QueFavoriteListAddEdit");
                        }
                        else
                        {
                            //FormsAuthentication.RedirectFromLoginPage(username.Text, true);
                            List<NameValueCollection> menuItems = new List<NameValueCollection>();

                            NameValueCollection nvc1 = new NameValueCollection();

                            nvc1["href"] = "ImpAskSuggestionOne.aspx";
                            nvc1["title"] = "Improvement Plan";
                            menuItems.Add(nvc1);

                            Session["menuItems"] = menuItems;
                            Response.Redirect("/KMSPages/ImpAskSuggestionOne.aspx");
                        }
                    }
                    else if (Session["userTypeId"] != null && Session["userTypeId"].ToString() == "3") //Super Admin
                    {
                        List<NameValueCollection> menuItems = new List<NameValueCollection>();

                        NameValueCollection nvc1 = new NameValueCollection();

                        nvc1["href"] = "/KMSPages/UserManagement.aspx";
                        nvc1["title"] = "User";
                        menuItems.Add(nvc1);

                        NameValueCollection nvc5 = new NameValueCollection();
                        nvc5["href"] = "/AppPages/ProgramView.aspx";
                        nvc5["title"] = "Program";
                        menuItems.Add(nvc5);

                        NameValueCollection nvc2 = new NameValueCollection();
                        nvc2["href"] = "/AppPages/CoursesViews.aspx";
                        nvc2["title"] = "Course";
                        menuItems.Add(nvc2);

                        NameValueCollection nvc4 = new NameValueCollection();
                        nvc4["href"] = "/AppPages/CourseTopic.aspx";
                        nvc4["title"] = "Course Topic";
                        menuItems.Add(nvc4);

                        NameValueCollection nvc3 = new NameValueCollection();
                        nvc3["href"] = "/AppPages/CLO.aspx";
                        nvc3["title"] = "CLO";
                        menuItems.Add(nvc3);

                        Session["menuItems"] = menuItems;
                        Response.Redirect("/KMSPages/UserManagement");
                    }
                }
                else
                {
                    Session["loginStatus"] = false;
                    Response.Redirect("/KMSPages/LoginPage.aspx");
                }
            }
            else
            {
                Response.Redirect("/KMSPages/LoginPage.aspx");
            }

        }

        private void insertIntoSession(String key, NameValueCollection nvc)
        {
            if (!string.IsNullOrEmpty(nvc[key]))
            {
                LogUtils.myLog.Info("nvc[\"uniId\"] : " + nvc[key]);
                Session[key] = nvc[key];
            }

        }
    }
}