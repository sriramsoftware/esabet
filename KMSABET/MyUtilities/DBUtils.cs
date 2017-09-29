using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace KMSABET.MyUtilities
{
    public class DBUtils
    {
        SqlConnection myConnection;

        public DBUtils()
        {
            myConnection = new SqlConnection(MyConstants.DBConnectionString);
        }

        public int CUDOperations(String myQuery)
        {
            int numRowsAffected = 0;
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(myQuery, myConnection);
                numRowsAffected = myCommand.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                LogUtils.myLog.Debug(e.ToString(), e);
            }
            return numRowsAffected;
        }

        public int CUDOperationsScalar(String myQuery)
        {
            int firstColumnValue = 0;
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(myQuery, myConnection);
                firstColumnValue = (int) myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch (Exception e)
            {
                LogUtils.myLog.Debug(e.ToString(), e);
            }
            return firstColumnValue;
        }

        public SqlDataReader readOperation(String myQuery)
        {
            SqlDataReader myReader = null;
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(myQuery, myConnection);
                myReader = myCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                LogUtils.myLog.Debug(e.ToString(), e);
            }
            return myReader;
        }

        public void closeDBConnection() 
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                LogUtils.myLog.Debug(e.ToString(), e);
            }

        }
    }
}