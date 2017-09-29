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
    public partial class ImpRuleList : System.Web.UI.Page
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
            ImpDao impDaoObj = new ImpDao();
            List<ImpRule> ruleList = impDaoObj.getRuleList();

            attributeListTag.DataSource = ruleList;
            attributeListTag.DataBind();
        }
    }
}