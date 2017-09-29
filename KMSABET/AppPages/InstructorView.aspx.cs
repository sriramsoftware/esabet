using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class InstructorView : System.Web.UI.Page
    {
        bool Update = false;
        int IDs = 0;
        static int Values = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            Update = Request.QueryString["Update"] == null ? false : bool.Parse(Request.QueryString["Update"]);
            bool Delete = Request.QueryString["Delete"] == null ? false : bool.Parse(Request.QueryString["Delete"]);
            IDs = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            bool Edit = Request.QueryString["Edit"] == null ? false : bool.Parse(Request.QueryString["Edit"]);

            if (Delete)
            {
                DeleteData();
                Response.Redirect("~/AppPages/Instructor.aspx");
            }
            else if (Update)
            {
                Button1.Text = "Submit";

                if (Values == 1)
                {
                    SelectionData();
                    Values = 2;
                }
                else
                {
                    Values = 1;
                }

            }
            else if (Edit)
            {
                SelectionData();
                TextBox1.Enabled = false;
                TextBox2.Enabled = false;
                TextBox3.Enabled = false;
                TextBox4.Enabled = false;
                TextBox5.Enabled = false;
                TextBox6.Enabled = false;
                TextBox7.Enabled = false;
                TextBox8.Enabled = false;
                TextBox9.Enabled = false;
                unis.Enabled = false;
                Button1.Visible = false;
            }
            else
            {
                Button1.Text = "Submit";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!Update)
            {
                try
                {
                    Connections con = new Connections();

                    int res = con.InsertData("declare @a int; select @a = UNI_ID from APP_UNIVERSITY where UNI_NAME = '" + unis.SelectedValue.ToString() + "'; insert into dbo.App_Instructor (instructor_name,FIRST_NAME,MIDDLE_NAME,LAST_NAME,OFFICE_ROOM_NO,BUILDING,OFFICE_PHONE_EXT,EMAIL,CELL_PHONE_NUM,WEB_ADDRESS, UNI_ID) values ('" + TextBox1.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox9.Text + "',@a);");
                    if (res == 1)
                    {
                        Response.Write("Successfully Complete");
                        Response.Redirect("~/AppPages/Instructor.aspx");
                    }
                    else
                    {
                        Response.Write("Error");
                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                }
            }
            else
            {
                try
                {
                    Connections con = new Connections();

                    int res = con.InsertData("declare @a int; select @a = UNI_ID from APP_UNIVERSITY where UNI_NAME = '" + unis.SelectedValue.ToString() + "'; update App_Instructor set instructor_name = '" + TextBox1.Text + "',FIRST_NAME = '" + TextBox1.Text + "',MIDDLE_NAME = '" + TextBox2.Text + "',LAST_NAME = '" + TextBox3.Text + "',OFFICE_ROOM_NO = '" + TextBox4.Text + "',BUILDING = '" + TextBox5.Text + "',OFFICE_PHONE_EXT = '" + TextBox6.Text + "', EMAIL = '" + TextBox7.Text + "',CELL_PHONE_NUM = '" + TextBox8.Text + "', WEB_ADDRESS = '" + TextBox9.Text + "',UNI_ID = @a where instructor_id = " + IDs + ";");
                    if (res == 1)
                    {
                        Response.Write("Successfully Complete");
                        Response.Redirect("~/AppPages/Instructor.aspx");                        
                    }
                    else
                    {
                        Response.Write("update App_Instructor set instructor_name = '" + TextBox1.Text + "',FIRST_NAME = '" + TextBox1.Text + "',MIDDLE_NAME = '" + TextBox2.Text + "',LAST_NAME = '" + TextBox3.Text + "',OFFICE_ROOM_NO = '" + TextBox4.Text + "',BUILDING = '" + TextBox5.Text + "',OFFICE_PHONE_EXT = '" + TextBox6.Text + "', EMAIL = '" + TextBox7.Text + "',CELL_PHONE_NUM = '" + TextBox8.Text + "', WEB_ADDRESS = '" + TextBox9.Text + "',UNI_ID = '' where instructor_id = "+IDs+";");
                    }
                }
                catch (Exception ex)
                {
                    MyUtilities.LogUtils.myLog.Error("Error", ex);
                }
            }
        }

        public void DeleteData()
        {
            try
            {
                Connections con = new Connections();
                int res = con.InsertData("delete from App_Instructor where instructor_id = " + IDs + "");

                if (res == 1)
                {
                    Response.Write("Successfully Complete");
                    Response.Redirect("~/AppPages/Instructor.aspx");
                }
                else
                {
                    Response.Write("Error");
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public void SelectionData()
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader res = db.readOperation(@"Declare @b int; Declare @bb varchar(50); select @b = App_Instructor.UNI_ID from App_Instructor where instructor_id =" + IDs + "; Select @bb = UNI_NAME from  APP_UNIVERSITY where APP_UNIVERSITY.UNI_ID = @b; select *,@bb as Uni from App_Instructor where instructor_id =" + IDs + ";");

                while (res.Read())
                {
                    TextBox1.Text = res["FIRST_NAME"].ToString();
                    TextBox2.Text = res["MIDDLE_NAME"].ToString();
                    TextBox3.Text = res["LAST_NAME"].ToString();
                    TextBox4.Text = res["OFFICE_ROOM_NO"].ToString();
                    TextBox5.Text = res["BUILDING"].ToString();
                    TextBox6.Text = res["OFFICE_PHONE_EXT"].ToString();
                    TextBox7.Text = res["EMAIL"].ToString();
                    TextBox8.Text = res["CELL_PHONE_NUM"].ToString();
                    TextBox9.Text = res["WEB_ADDRESS"].ToString();
                    if (!Update)
                    {
                        uniss.Text = res["Uni"].ToString();
                        uniss.Visible = true;
                        unis.Visible = false;
                    }
                    else
                    {
                        unis.Items.Add(res["Uni"].ToString());    
                    }
                    
                }
                Button1.Text = "Submit";
            }

            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

    }
}