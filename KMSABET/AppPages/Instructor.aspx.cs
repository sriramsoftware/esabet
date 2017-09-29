using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class Instructor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

     
      
        protected void BtnSel_Click(object sender, EventArgs e)
        {
            
        }

        protected void Instructorgrid_Load(object sender, EventArgs e)
        {
            try
            {
                string a = "select instructor_id as 'Instructor ID', instructor_name as 'Name' ,FIRST_NAME +' '+ MIDDLE_NAME +' '+ LAST_NAME as 'Full Name', EMAIL as Email, CELL_PHONE_NUM as 'Cell Number', UNI_NAME as 'University Name' from App_Instructor t1 inner join APP_UNIVERSITY t2 on t1.UNI_ID = t2.UNI_ID";

                MyUtilities.DBUtils db = new MyUtilities.DBUtils();

                SqlDataReader sdb = db.readOperation(a);
                List<App_Instructor> list = new List<App_Instructor>();

                while (sdb.Read())
                {
                    App_Instructor info = new App_Instructor() { instructor_id = sdb["Instructor ID"].ToString(), instructor_name = sdb["Name"].ToString(), Full_Name = sdb["Full Name"].ToString(), EMAIL = sdb["Email"].ToString(), CELL_PHONE_NUM = sdb["Cell Number"].ToString(), UNI_ID = sdb["University Name"].ToString() };                    
                    list.Add(info);
                }

                Instructorgrid.DataSource = list;
                Instructorgrid.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Insert_New_Record_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppPages/InstructorView.aspx");
        }

    }
}