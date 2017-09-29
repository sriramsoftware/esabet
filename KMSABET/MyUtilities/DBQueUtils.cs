using KMSABET.MyDaos;
using KMSABET.MyPocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KMSABET.MyUtilities
{
    public class DBQueUtils
    {
        public void fillSelectAggregateFuncList(DropDownList ddl)
        {
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeTypeReader = dbUtils.readOperation("SELECT * FROM App_CODE Where CODE_TYPE_ID=1200");
            while (attributeTypeReader.Read())
            {
                ddl.Items.Add(new ListItem(attributeTypeReader["CODE_VALUE"].ToString(),
                    attributeTypeReader["CODE_ID"].ToString()));
            }
            dbUtils.closeDBConnection();

        }

        public void changeInitOfSelectTableList(DropDownList selectTableFKTableName2, Label selectTableFkColumnName2,
            DropDownList DropDownSelectTable1, Label selectTablePKName2, Label selectTablePkColumnName2,
            DropDownList DropDownFKTb2, DropDownList DropDownPKTb2, DropDownList DropDownSelectCol1, String aliasFK, String aliasPK)
        
        {
            fillFKTableListForPKTable(DropDownPKTb2.SelectedItem.Text, DropDownFKTb2, aliasFK);
            if (DropDownFKTb2.Items.Count == 0) DropDownPKTb2.Items.RemoveAt(DropDownPKTb2.SelectedIndex);
            fillSelectTableColumList(DropDownSelectCol1, DropDownPKTb2, aliasPK);
            if (selectTableFKTableName2.SelectedItem != null)
                selectTableFkColumnName2.Text = selectTableFKTableName2.SelectedItem.Value;
            else selectTableFkColumnName2.Text = "";
            selectTablePKName2.Text = DropDownSelectTable1.SelectedItem.Text;
            DBQueDao daoObj = new DBQueDao();
            if (selectTableFKTableName2.SelectedItem != null)
                selectTablePkColumnName2.Text = aliasPK + "." + daoObj.getPKFKColumnValue(selectTableFKTableName2.SelectedItem.Text, DropDownPKTb2.SelectedItem.Text);//DropDownSelectTable1.SelectedValue;
        }

        public void fillSelectTableColumList(DropDownList DropDownSelectCol1, DropDownList DropDownPKTb2, String alias)
        {
            List<DBQueColumn> colList = getColumnListByTableName(DropDownPKTb2.SelectedItem.Text, alias);
            DropDownSelectCol1.DataSource = colList;
            DropDownSelectCol1.DataTextField = "columnName";
            DropDownSelectCol1.DataValueField = "columnNameAlias";
            DropDownSelectCol1.DataBind();
        }

        public void fillFKTableListForPKTable(String tableName, DropDownList ddListName, String alias)
        {

            DBQueDao daoObj = new DBQueDao();
            List<DBQuePKFKColumn> colList = daoObj.getPKFKColumnList(tableName, alias);
            ddListName.Items.Clear();

            foreach (DBQuePKFKColumn col in colList)
            {

                ListItem columnObj = new ListItem();
                columnObj.Text = col.fkTableName;
                columnObj.Value = col.fkColumnName;

                ddListName.Items.Add(columnObj);
                
            }

        }


        public void fillSelectTableList(DropDownList DropDownPKTb2, String alias)
        {
            DBQueDao daoObj = new DBQueDao();
            List<DBQueTable> tableList = daoObj.getTableList();
            foreach (DBQueTable dbTable in tableList)
            {
                ListItem att2 = new ListItem();
                att2.Text = dbTable.tableName;
                att2.Value = dbTable.tableName + " " + alias;
                DropDownPKTb2.Items.Add(att2);
            }

        }

        public List<DBQueColumn> getColumnListByTableName(String tableName, String alias)
        {
            List<DBQueColumn> columnList = new List<DBQueColumn>();

            String myQuery = "SELECT"
                + " c.name 'Column Name',"
                + " ISNULL(i.is_primary_key, 0) 'Primary Key'"
                + " FROM"
                + " sys.columns c"
                + " LEFT OUTER JOIN"
                + " sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id"
                + " LEFT OUTER JOIN"
                + " sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id"
                + " WHERE"
                + " c.object_id = OBJECT_ID('" + tableName + "')";

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);

            while (listReader.Read())
            {
                DBQueColumn columnObj = new DBQueColumn();
                columnObj.columnName = listReader["Column Name"].ToString();
                columnObj.columnNameAlias = alias + "." + listReader["Column Name"].ToString();
                columnObj.isPrimary = Boolean.Parse(listReader["Primary Key"].ToString());

                columnList.Add(columnObj);
            }
            dbUtils.closeDBConnection();

            return columnList;
        }

        public List<ListItem> fillPKFKTableListItem(DropDownList ddFK, DropDownList ddPK, List<ListItem> listOfTables)
        {
            if (ddFK.SelectedItem != null)
            {
                if (alreadyExistsStrValue(ddFK.SelectedItem.Value, listOfTables) == false)
                {
                    listOfTables.Add(new ListItem(ddFK.SelectedItem.Text,
                        ddFK.SelectedItem.Text + " " + ddFK.SelectedItem.Value.Split('.')[0]));
                }
            }
            if (ddPK.SelectedItem != null)
            {
                if (alreadyExistsStrValue(ddPK.SelectedItem.Value, listOfTables) == false)
                {
                    listOfTables.Add(new ListItem(ddPK.SelectedItem.Text,
                        ddPK.SelectedItem.Value));
                }
            }

            return listOfTables;
        }

        public void ddlPKChangeAction(int lastIndex, List<DropDownList> ddPKList, List<DropDownList> ddFKList,
            List<Label> lblPKList, List<Label> lblFKList)
        {
            for (int i = 10; i > lastIndex; i--)
            {
                ddPKList[i].Items.Clear();
                ddFKList[i].Items.Clear();
                lblPKList[i].Text = String.Empty;
                lblFKList[i].Text = String.Empty;
            }

            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            DBQueDao daoObj = new DBQueDao();

            dbQueUtilsObj.fillFKTableListForPKTable(ddPKList[lastIndex].SelectedItem.Text, ddFKList[lastIndex], ("A" + (lastIndex - 1).ToString()));
            if (ddFKList[lastIndex].Items.Count == 0) ddPKList[lastIndex].Items.RemoveAt(ddPKList[lastIndex].SelectedIndex);
            lblFKList[lastIndex].Text = ddFKList[lastIndex].SelectedValue;
            String fkColumnValue = "";
            if (ddFKList[lastIndex].SelectedItem != null && ddFKList[lastIndex].SelectedItem.Text != "" 
                    && ddPKList[lastIndex].SelectedItem != null && ddPKList[lastIndex].SelectedItem.Text != "")
                fkColumnValue = daoObj.getPKFKColumnValue(ddFKList[lastIndex].SelectedItem.Text, 
                    ddPKList[lastIndex].SelectedItem.Text);
            lblPKList[lastIndex].Text = ddPKList[lastIndex].SelectedItem.Value.Split(' ')[1] + "."
                + fkColumnValue;

        }

        public void fillPKTableList(DropDownList ddPK, List<ListItem> listOfTables)
        {
            ddPK.Items.Clear();
            DBQueDao dbQueDaoObj = new DBQueDao();
            foreach (ListItem item in listOfTables)
            {
                if (item.Text != "" && dbQueDaoObj.getPKFKColumnList(item.Text, "BBB").Count > 0)
                {
                    ddPK.Items.Add(new ListItem(item.Text, item.Value));
                }
                
            }
        }

        public void fillColumnValueList(DropDownList ddPK, List<ListItem> listOfTables)
        {
            ddPK.Items.Clear();
            DBQueDao dbQueDaoObj = new DBQueDao();
            foreach (ListItem item in listOfTables)
            {
                ddPK.Items.Add(new ListItem(item.Text, item.Value));
            }
        }

        public bool alreadyExistsStr(String inputStr, List<ListItem> listItemsList)
        {
            bool matched = false;
            foreach (ListItem item in listItemsList)
            {
                if (item.Text == inputStr) matched = true;
            }
            return matched;
        }

        public bool alreadyExistsStrValue(String inputStrValue, List<ListItem> listItemsList)
        {
            bool matched = false;
            foreach (ListItem item in listItemsList)
            {
                if (item.Value == inputStrValue) matched = true;
            }
            return matched;
        }

        public int getIndexOfDDList(object sender)
        {
            int lastIndex = 1;

            if (sender != null)
            {
                String senderID = ((DropDownList)sender).ID.ToString();
                try
                {
                    int startInd = senderID.Length -2;
                    int endInd = 2;
                    String last = senderID.Substring(startInd, endInd);
                    lastIndex = Int32.Parse(last.ToString());
                }
                catch (Exception ex)
                {
                    char last = senderID[senderID.Length - 1];
                    lastIndex = Int32.Parse(last.ToString());
                }
            }

            return lastIndex;
        }

        public int getIndexOfCheckBoxList(object sender)
        {
            int lastIndex = 1;

            if (sender != null)
            {
                String senderID = ((CheckBox)sender).ID.ToString();
                try
                {
                    int startInd = senderID.Length - 2;
                    int endInd = 2;
                    String last = senderID.Substring(startInd, endInd);
                    lastIndex = Int32.Parse(last.ToString());
                }
                catch (Exception ex)
                {
                    char last = senderID[senderID.Length - 1];
                    lastIndex = Int32.Parse(last.ToString());
                }
            }

            return lastIndex;
        }

        public List<ListItem> fillWhereClauseDropDownTable(DropDownList DropDownPKTb2, String alias, List<ListItem> listItems)
        {
            String dropDownText = DropDownPKTb2.SelectedItem.Text;
            String dropDownValue = alias;
            //if (alreadyExistsStr(dropDownText, listItems) == false && DropDownPKTb2.SelectedItem.Text != "")
            //{
                ListItem itemLocal = new ListItem();
                itemLocal.Text = dropDownText;
                itemLocal.Value = dropDownValue;
                listItems.Add(itemLocal);
            //}

            return listItems;
        }

        public void fillSelectedColumnValues(DropDownList ddWhereTable, DropDownList ddWhereColumn, DropDownList ddColumnValue)
        {
            String tableName = ddWhereTable.SelectedItem.Text;
            String pkColumnName = ddWhereTable.SelectedItem.Value;
            String columnSelected = ddWhereColumn.SelectedItem.Text;
            DBQueDao dbQueDaoObj = new DBQueDao();
            List<ListItem> listItems = dbQueDaoObj.getColumnValues(tableName, columnSelected);
            fillColumnValueList(ddColumnValue, listItems);
        }

        public String leftJoinQueryAppender(DropDownList DropDownFKTb2, Label FKCol2, Label PKCol2, String myQuery, String alias)
        {
            if (DropDownFKTb2 != null && DropDownFKTb2.SelectedItem != null && DropDownFKTb2.SelectedItem.Text != "")
            {
                myQuery += " LEFT OUTER JOIN " + DropDownFKTb2.SelectedItem.Text + " " + alias + " ON "
                    + FKCol2.Text
                    + "=" + PKCol2.Text;
            }
            return myQuery;
        }
    }
}