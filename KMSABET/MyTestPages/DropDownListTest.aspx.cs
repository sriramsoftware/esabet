using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class DropDownListTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                fillSelectTableList();
                getPKFKColumnListItemsByTableName(DropDownList1.SelectedItem.Text);
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getPKFKColumnListItemsByTableName("I_CLO");
            LogUtils.myLog.Info(((DropDownList)sender).SelectedValue);
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPKFKColumnListItemsByTableName(DropDownList1.SelectedItem.Text);
        }

        protected void fillSelectTableList()
        {
            DBQueDao daoObj = new DBQueDao();
            List<DBQueTable> tableList = daoObj.getTableList();
            foreach (DBQueTable dbTable in tableList)
            {
                ListItem att2 = new ListItem();
                att2.Text = dbTable.tableName;
                att2.Value = dbTable.pkColumnName;
                DropDownList1.Items.Add(att2);
            }
            

        }

        protected void getPKFKColumnListItemsByTableName(String tableName)
        {
            DBQueDao daoObj = new DBQueDao();
            List<DBQuePKFKColumn> colList = daoObj.getPKFKColumnList(tableName, "A1");
            foreach (DBQuePKFKColumn col in colList)
            {

                ListItem columnObj = new ListItem();
                columnObj.Text = col.fkTableName;
                columnObj.Value = col.fkColumnName;

                DropDownList2.Items.Add(columnObj);
            }

        }

    }
}