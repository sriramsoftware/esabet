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
    public partial class UserManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                loadUsersList();
            }
        }

        protected void usersList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            usersList.PageIndex = e.NewPageIndex;
            loadUsersList();
        }

        protected void usersList_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        private void loadUsersList()
        {
            AppDao queDaoObj = new AppDao();
            List<AppUser> quesList = queDaoObj.getListOfUsers();

            usersList.DataSource = quesList;
            usersList.DataBind();
        }
    }
}