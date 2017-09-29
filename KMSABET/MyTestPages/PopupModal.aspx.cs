using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class PopupModal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void ServerButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key",
                "launchModal();", true);
        }





        protected void OpenWindow(object sender, EventArgs e)
        {
            string url = "PopupModal";
            string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }




        protected void btnShowPopup_Click(object sender, EventArgs e)
        {
            string message = "Message from server side";
            hello.Text = "How are you";
            MsgBox(message, this.Page);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + message + "');", true);
        }





        protected void btnShowPopup_Click1(object sender, EventArgs e)
        {
            Button2.PostBackUrl = ((Button)sender).CommandArgument.ToString();
            string message = "Do you want to delete record?";
            hello.Text = "How are you";
            MsgBox(message, this.Page);
        }
        public void MsgBox(string title, Page page)
        {
            AjaxControlToolkit.ModalPopupExtender ModalPopupExtender =
                page.FindControl("ctl00$MainContent$MsgBoxModalPopupExtender") as AjaxControlToolkit.ModalPopupExtender;
            System.Web.UI.WebControls.Label Label =
                page.FindControl("ctl00$MainContent$LabelMsgBox") as System.Web.UI.WebControls.Label;

            string message = "<b>" + title + "</b>";
            Label.Text = message;

            UpdatePanel UpdatePanel = page.FindControl("ctl00$MainContent$UpdatePanelMsgBox") as UpdatePanel;
            UpdatePanel.Update();
            ModalPopupExtender.Show();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.CommandArgument.ToString();
        }

    }
}