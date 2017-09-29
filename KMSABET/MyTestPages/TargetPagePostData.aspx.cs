using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class TargetPagePostData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PreviousPage != null)
            {
                //Using Component
                ContentPlaceHolder contentBox = (ContentPlaceHolder)PreviousPage.Master.FindControl("MainContent");
                TextBox SourceTextBox = (TextBox)contentBox.FindControl("textBox1");
                if (SourceTextBox != null)
                {
                    Label1.Text = SourceTextBox.Text;
                }

                //Using Class Attributes
                Label2.Text = PreviousPage.sourceData.ToString() + " Attribute ID: " + PreviousPage.attributeData.attributeID
                    + " Attribute Statemet: " + PreviousPage.attributeData.attributeStatement;
            }

            String value = ClientQueryString;
            String[] valueArr = value.Split('=');
            LogUtils.myLog.Info("myValue : " + valueArr[1]);
        }

    }
}