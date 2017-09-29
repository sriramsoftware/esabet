using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace KMSABET
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            AppEmailData emailData = new AppEmailData();
            GeneralUtils generalUtils = new GeneralUtils();
            emailData.toAddress = MyConstants.SupportEmailAdd;
            emailData.subject = "ESABET Error Report - " + DateTime.Now.ToString();
            emailData.bodyHtml = Request.Url.AbsoluteUri + "<br/><br/>" + "<a href='http://esabet.azurewebsites.net/log.txt'>View Log File: http://esabet.azurewebsites.net/log.txt </a>";
            LogUtils.myLog.Error(emailData.subject);
            LogUtils.myLog.Error(emailData.bodyHtml);
            generalUtils.sendEmail(emailData);
            Exception err = Server.GetLastError();
            if (err != null)
            {
                err = err.GetBaseException();
                LogUtils.myLog.Error("Message : " + err.Message);
                LogUtils.myLog.Error("Source : " + err.Source);
                LogUtils.myLog.Error("InnerException : " + ((err.InnerException != null) ? err.InnerException.ToString() : ""));
                LogUtils.myLog.Error("StackTrace : " + err.StackTrace);
                LogUtils.myLog.Error("Full StackTrace : " + err.Message, err);
                LogUtils.myLog.Error("Message : " + err.Message);
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            
        }
    }
}