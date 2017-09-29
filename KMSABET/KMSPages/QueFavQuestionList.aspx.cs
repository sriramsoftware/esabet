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
    public partial class QueFavQuestionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                LoadList(Session["CourseID"].ToString());
            }
        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            attributeListTag.PageIndex = e.NewPageIndex;
            LoadList(Session["CourseID"].ToString());
        }

        private void LoadList(String courseId)
        {
            QueDao queDaoObj = new QueDao();
            List<KMSABET.MyPocos.QueFavQuestionList> quesList = new List<MyPocos.QueFavQuestionList>();
            if (Session["InstructorID"] == null || Session["userTypeId"].ToString().Equals("1"))
                quesList = queDaoObj.getFavQuestionList(courseId);
            else quesList = queDaoObj.getFavQuestionList(courseId, Session["InstructorID"].ToString());
            attributeListTag.DataSource = quesList;
            attributeListTag.DataBind();
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            LoadList(Session["CourseID"].ToString());
        }

        protected void attributeListTag_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (Session["LoginID"] != null && Session["userTypeId"].ToString() == "2")
            {
                ((DataControlField)attributeListTag.Columns
                .Cast<DataControlField>()
                .Where(fld => fld.HeaderText == "Instructor Name")
                .SingleOrDefault()).Visible = false;
            }
        }

        protected void deleteBtn_Click(object sender, EventArgs e)
        {
            Button deleteBtnInp = (Button)sender;
            deleteBtnInp.CommandArgument.ToString();
            Button2.PostBackUrl = "QueFavoriteListView.aspx?id=" + ((Button)sender).CommandArgument.ToString() + "&viewMode=false&delete=true";

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