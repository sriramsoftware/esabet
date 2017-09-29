using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Excel_Data
{
    public class ExcelQuestions
    {
        public string Question { get; set; }
        public string CLO { get; set; }
        public string SO { get; set; }
        public List<string> Attribute = new List<string>();        
        public string CourseID { get; set; }
        public string ProgramID { get; set; }        
        public string CoursetopicID { get; set; }
    }

    public class Data
    {
        public SqlDataReader readOperation(String myQuery,SqlConnection myConnection)
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
               
            }
            return myReader;
        }
    }

    public class Query
    {
        public string query { get; set; }
    }

    

}