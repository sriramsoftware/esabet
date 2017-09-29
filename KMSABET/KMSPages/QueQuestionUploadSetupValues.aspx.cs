using KMSABET.MyDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class QueQuestionUploadSetupValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
                LoadCourseTopicList();
            }
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
            LoadCourseTopicList();
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
            LoadCLOList();
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 2;
            LoadSOList();
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Clicked";
            MainView.ActiveViewIndex = 3;
            LoadAttributeOptionsList();
        }

        private void LoadCourseTopicList()
        {
            QueDao queDaoObj = new QueDao();
            List<KMSABET.MyPocos.AppCourseTopic> quesList = queDaoObj.getCourseTopicList();

            courseTopicListTag.DataSource = quesList;
            courseTopicListTag.DataBind();
        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            courseTopicListTag.PageIndex = e.NewPageIndex;
            LoadCourseTopicList();
        }

        private void LoadCLOList()
        {
            QueDao queDaoObj = new QueDao();
            List<KMSABET.MyPocos.AppCLO> quesList = queDaoObj.getCLOList();

            CLOListGridView.DataSource = quesList;
            CLOListGridView.DataBind();
        }

        protected void CLOListGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CLOListGridView.PageIndex = e.NewPageIndex;
            LoadCLOList();
        }

        private void LoadSOList()
        {
            QueDao queDaoObj = new QueDao();
            List<KMSABET.MyPocos.AppSO> quesList = queDaoObj.getSOList();

            SO_GridView1.DataSource = quesList;
            SO_GridView1.DataBind();
        }

        protected void SO_GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SO_GridView1.PageIndex = e.NewPageIndex;
            LoadSOList();
        }

        private void LoadAttributeOptionsList()
        {
            QueDao queDaoObj = new QueDao();
            List<KMSABET.MyPocos.QueAttributeOption> quesList = queDaoObj.getAttributeOptionsList();

            AttributeOptionsGridView1.DataSource = quesList;
            AttributeOptionsGridView1.DataBind();
        }

        protected void AttributeOptionsGridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            AttributeOptionsGridView1.PageIndex = e.NewPageIndex;
            LoadAttributeOptionsList();
        }
    }
}