using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class QueAttributeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                LoadList();
            }
        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            attributeListTag.PageIndex = e.NewPageIndex;
            LoadList();
        }

        private void LoadList()
        {
            QueDao queDaoObj = new QueDao();

            String courseIdLocal = Session["CourseID"].ToString();
            List<QueAttribute> attrList = queDaoObj.getAttrbuteList(courseIdLocal);

            attributeListTag.DataSource = attrList;
            attributeListTag.DataBind();
        }


        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            Button deleteBtnInp = (Button)sender;
            deleteBtnInp.CommandArgument.ToString();
            Button2.PostBackUrl = "QueAttributeAddViewEdit.aspx?id=" + ((Button)sender).CommandArgument.ToString() + "&viewMode=false&editMode=false&delete=true";

            string message = "Do you want to delete record?";
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
    }
}