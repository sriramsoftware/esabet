using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class CourseTopic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {   
                new Connections().GetProgram(Program);
                new Students().SelectCourse(Course, Program);
                new ClOSO().GETCLOs(CLO, Course);
                GetRecords();

            }

        }

        public void GetRecords()
        {
            try
            {
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select TOPIC_ID as TID,TOPIC_STATEMENT as TS,t2.course_name as CID,LECTURE_HOURS as LH,LAB_HOURS as LABH from APP_COURSE_TOPIC t1 inner join App_Course t2 on t1.COURSE_ID = t2.course_id  where t2.course_id =  " + new Connections().GetCourseID(Course) + " and CLO_ID = "+CLO.SelectedValue+"");
                List<APP_CourseTopic> list = new List<APP_CourseTopic>();
                while (sdb.Read())
                {
                    list.Add(new APP_CourseTopic() { TOPIC_ID = sdb["TID"].ToString(), TOPIC_STATEMENT = sdb["TS"].ToString(), Course_ID = sdb["CID"].ToString(), LAB_HOURS = sdb["LABH"].ToString(), LECTURE_HOURS = sdb["LH"].ToString() });
                }

                GridView1.DataSource = list;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error ", ex);
            }
        }


        protected void button_Click(object sender, EventArgs e)
        {
            
        }


        protected void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string qu = "declare @a int; select top 1 @a = TOPIC_ID from dbo.APP_COURSE_TOPIC order by TOPIC_ID Desc; delete from dbo.APP_COURSE_TOPIC where TOPIC_ID = @a;";
                int res = new Connections().InsertData(qu);
                if (res == 1)
                {
                    Response.Write("Successfully Complete.");
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
                else
                {
                    Response.Write(qu);
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Deleting :", ex);
            }
        }
        
        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AppPages/CourseTopicView.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetRecords();
        }

        protected void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            new ClOSO().GETCLOs(CLO, Course);
            GetRecords();
        }

        protected void Program_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(Course, Program);
            new ClOSO().GETCLOs(CLO, Course);
            GetRecords();
        }

        protected void CLO_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRecords();
        }

        
                
    }
}