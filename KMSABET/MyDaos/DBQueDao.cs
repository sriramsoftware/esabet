using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KMSABET.MyDaos
{
    public class DBQueDao
    {

        public String getPKColumnName(String tableName)
        {
            String pkColumnName = "";
            String myQuery = 
                "SELECT Col.Column_Name from "
                    + " INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab,"
                    + " INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col"
                + " WHERE"
                    + " Col.Constraint_Name = Tab.Constraint_Name"
                    + " AND Col.Table_Name = Tab.Table_Name"
                    + " AND Constraint_Type = 'PRIMARY KEY'"
                    + " AND Col.Table_Name = '"+ tableName +"'";

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);

            while (listReader.Read())
            {
                pkColumnName = listReader["Column_Name"].ToString();
            }
            dbUtils.closeDBConnection();

            return pkColumnName;
        }

        public List<DBQueTable> getTableList()
        {
            List<DBQueTable> tableList = new List<DBQueTable>();
            String myQuery = "SELECT T.[name] AS [table_name], AC.[name] AS [column_name]"
                + " FROM sys.[tables] AS T"
                + " INNER JOIN sys.[all_columns] AC ON T.[object_id] = AC.[object_id]"
                + " LEFT OUTER JOIN"
                + " sys.index_columns ic ON ic.object_id = AC.object_id AND ic.column_id = AC.column_id"
                + " LEFT OUTER JOIN"
                + " sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id"
                + " WHERE T.[is_ms_shipped] = 0"
                + " AND I.is_primary_key = 1"
                + " ORDER BY T.[name], AC.[column_id]";

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);

            while (listReader.Read())
            {
                DBQueTable att2 = new DBQueTable();
                att2.tableName = listReader["table_name"].ToString();
                att2.pkColumnName = att2.tableName + "." + listReader["column_name"].ToString();
                tableList.Add(att2);
            }
            dbUtils.closeDBConnection();

            return tableList;
        }

        public String getPKColumnByTableName(String tableName)
        {
            String pkColumnName = "";
            String myQuery = "SELECT T.[name] AS [table_name], AC.[name] AS [column_name]"
                + " FROM sys.[tables] AS T"
                + " INNER JOIN sys.[all_columns] AC ON T.[object_id] = AC.[object_id]"
                + " LEFT OUTER JOIN"
                + " sys.index_columns ic ON ic.object_id = AC.object_id AND ic.column_id = AC.column_id"
                + " LEFT OUTER JOIN"
                + " sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id"
                + " WHERE T.[is_ms_shipped] = 0"
                + " AND I.is_primary_key = 1"
                + " AND T.name = '" + tableName + "'";

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);

            while (listReader.Read())
            {
                pkColumnName = listReader["column_name"].ToString();
            }
            dbUtils.closeDBConnection();

            return pkColumnName;
        }


        public List<DBQuePKFKColumn> getPKFKColumnList(String tableName, String alias)
        {
            List<DBQuePKFKColumn> columnList = new List<DBQuePKFKColumn>();
            //String myQuery = "EXEC sp_fkeys '" + tableName + "'";

            String myQuery = "SELECT"
                + " FK_Table = FK.TABLE_NAME,"
                + " FK_Column = CU.COLUMN_NAME,"
                + " PK_Table = PK.TABLE_NAME,"
                + " PK_Column = PT.COLUMN_NAME,"
                + " Constraint_Name = C.CONSTRAINT_NAME"
                + " FROM"
                + " INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C"
                + " INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK"
                + " ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME"
                + " INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK"
                + " ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME"
                + " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU"
                + " ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME"
                + " INNER JOIN ("
                + " SELECT"
                + " i1.TABLE_NAME,"
                + " i2.COLUMN_NAME"
                + " FROM"
                + " INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1"
                + " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2"
                + " ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME"
                + " WHERE"
                + " i1.CONSTRAINT_TYPE = 'PRIMARY KEY'"
                + " ) PT"
                + " ON PT.TABLE_NAME = PK.TABLE_NAME"
                + " WHERE FK.TABLE_NAME = '" + tableName + "'";

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);

            while (listReader.Read())
            {

                DBQuePKFKColumn columnObj = new DBQuePKFKColumn();
                columnObj.fkTableName = listReader["PK_Table"].ToString();
                columnObj.fkColumnName = alias + "." + listReader["PK_Column"].ToString();

                columnList.Add(columnObj);
            }
            dbUtils.closeDBConnection();

            return columnList;
        }

        public String getPKFKColumnValue(String pkTableName, String fkTableName)
        {
            String myQuery = "SELECT"
                + " FK_Table = FK.TABLE_NAME,"
                + " FK_Column = CU.COLUMN_NAME,"
                + " PK_Table = PK.TABLE_NAME,"
                + " PK_Column = PT.COLUMN_NAME,"
                + " Constraint_Name = C.CONSTRAINT_NAME"
                + " FROM"
                + " INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C"
                + " INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK"
                + " ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME"
                + " INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK"
                + " ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME"
                + " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU"
                + " ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME"
                + " INNER JOIN ("
                + " SELECT"
                + " i1.TABLE_NAME,"
                + " i2.COLUMN_NAME"
                + " FROM"
                + " INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1"
                + " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2"
                + " ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME"
                + " WHERE"
                + " i1.CONSTRAINT_TYPE = 'PRIMARY KEY'"
                + " ) PT"
                + " ON PT.TABLE_NAME = PK.TABLE_NAME"
                + " WHERE FK.TABLE_NAME = '" + fkTableName + "'"
                + " AND PK.TABLE_NAME = '" + pkTableName + "'";

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);
            String toReturn = "";

            while (listReader.Read())
            {

                toReturn = listReader["FK_Column"].ToString();

            }
            dbUtils.closeDBConnection();

            return toReturn;
        }

        public List<ListItem> getColumnValues(String tableName, String selectedColumnName)
        {
            String tablePKName = getPKColumnName(tableName);
            List<ListItem> columnValueList = new List<ListItem>();
            String myQuery = "SELECT " + tablePKName + ", " + selectedColumnName
                + " FROM " + tableName;

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);

            while (listReader.Read())
            {
                ListItem columnValue = new ListItem();
                String pkColName = tablePKName;
                String selColName = selectedColumnName;
                columnValue.Text = listReader[selColName].ToString();
                columnValue.Value = listReader[pkColName].ToString();
                columnValueList.Add(columnValue);
            }
            dbUtils.closeDBConnection();

            return columnValueList;
        }

    }
}