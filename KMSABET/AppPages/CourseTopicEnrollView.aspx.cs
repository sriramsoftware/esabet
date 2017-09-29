using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CourseTopicEnrollView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Connections.LIST.Count == 0)
            {
                Response.Redirect("~/AppPages/CourseTopicEnrollAdd.aspx");
            }

            if (Page.IsPostBack == false)
            {
                try
                {
                    SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select course_name as CN, program_name as PN from App_Course t1 inner join App_Program t2 on t1.App_Program_program_id = t2.program_id where t1.course_name = '" + Request.QueryString["CID"].ToString() + "'");

                    while (sdb.Read())
                    {
                        program.Items.Add(new ListItem() { Text = sdb["PN"].ToString(), Value = sdb["PN"].ToString() });
                        course.Items.Add(new ListItem() { Value = sdb["CN"].ToString(), Text = sdb["CN"].ToString() });
                    }

                    program.SelectedIndex = program.Items.Count - 1;
                    course.SelectedIndex = course.Items.Count - 1;

                    Acadmicyear.Items.Add(Request.QueryString["y"].ToString());
                    Acadmicyear.SelectedIndex = Acadmicyear.Items.Count - 1;
                    Acadmicyear.Enabled = false;
                    semster.Items.Add(Request.QueryString["s"].ToString());
                    semster.Enabled = false;
                    semster.SelectedIndex = semster.Items.Count - 1;
                    GetAllEnrollmentData();
                }
                catch (Exception ex)
                {
                    MyUtilities.LogUtils.myLog.Error("Error Occure While Run Page Load Of Course Topic Enroll View.", ex);

                }
            }
        }
        
        public void GetAllEnrollmentData()
        {
            try
            {
                List<APP_CourseTopic> MainList = new List<APP_CourseTopic>();
                List<string> li = Connections.LIST;

                string query = "select TOPIC_ID as ID, TOPIC_STATEMENT as TS,t2.course_name as 'CN' from APP_COURSE_TOPIC t1 inner join App_Course t2 on t1.COURSE_ID = t2.course_id where t2.course_name = '" + course.SelectedValue + "'";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(query);

                List<APP_CourseTopic> list = new List<APP_CourseTopic>();
                
                while (sdb.Read())
                {
                    list.Add(new APP_CourseTopic() { TOPIC_ID = sdb["ID"].ToString(), Course_ID = sdb["CN"].ToString(), TOPIC_STATEMENT = sdb["TS"].ToString() });                    
                }

                for (int i = 0; i < li.Count; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (li[i] == list[j].TOPIC_STATEMENT)
                        {
                            MainList.Add(list[j]);
                        }
                    }
                }

                MainGrid.DataSource = MainList;
                MainGrid.DataBind();

                foreach (GridViewRow item in MainGrid.Rows)
                {
                    DropDownList dropdown = (DropDownList)item.FindControl("drop");

                    if (dropdown.SelectedIndex == 0 || true)
                    {
                        for (int i = 0; i < MainList.Count; i++)
                        {
                            dropdown.Items.Add(new ListItem() { Text = i + 1 + "", Value = i + 1 + "" });
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
                
            }
        }

        

        protected void Acadmicyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetSamester(semster, Acadmicyear, course);
        }

        protected void semster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void program_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> drpval = new List<int>() { 0 };
                List<int> drpMahVal = new List<int>();
                bool DataValid = true;

                bool havedrpval = true;
                int a = 0;

                foreach (GridViewRow item in MainGrid.Rows)
                {
                    DropDownList drop = (DropDownList)item.FindControl("drop");

                    if (!(drpval.Contains(Convert.ToInt32(drop.SelectedValue))))
                    {
                        havedrpval = true;
                        drpval.Add(Convert.ToInt32(drop.SelectedValue));                        
                    }
                    else
                    {
                        havedrpval = false;
                        drpval.Add(Convert.ToInt32(drop.SelectedValue));
                        drpMahVal.Add(Convert.ToInt32(drop.SelectedValue));
                    }


                    if (!havedrpval)
                    {
                        DataValid = false;
                        break;
                    }

                }

                for (int i = 0; i < drpval.Count; i++)
                {
                    Response.Write(drpval[i]);                    
                    
                }

                for (int i = 0; i < drpMahVal.Count; i++)
                {
                    Response.Write(drpMahVal[i]);
                }

                if (DataValid)
                {
                    foreach (GridViewRow item in MainGrid.Rows)
                    {
                        DropDownList drop = (DropDownList)item.FindControl("drop");

                        Connections n = new Connections();

                        string Query = "insert into APP_COURSE_TOPIC_ENROL (COURSE_TOPIC_ID,COURSE_ENR_ID, TOPIC_SEQ_NUM) values ((SELECT TOPIC_ID FROM APP_COURSE_TOPIC WHERE TOPIC_STATEMENT = '" + Connections.LIST[a] + "')," + new Students().GetCoureEnrollID(semster, course, Acadmicyear) + "," + drop.SelectedValue + ");";
                        a++;                        
                        int res = n.InsertData(Query);
                        havedrpval = false;
                        drop.ForeColor = Color.Black;
                        Error.Text = "";
                    }

                    Response.Redirect("~/AppPages/Course.aspx");

                }
                else
                {
                    foreach (GridViewRow item in MainGrid.Rows)
                    {
                        DropDownList drop = (DropDownList)item.FindControl("drop");

                        if (drpMahVal.Contains(Convert.ToInt32(drop.SelectedValue)))
                        {
                            drop.ForeColor = Color.Red;
                        }
                        else
                        {
                            drop.ForeColor = Color.Black;
                        }
                    }

                    Error.Text = "Highlighted Values are Duplicate.";

                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Submit", ex);
            }
        }
    }
}