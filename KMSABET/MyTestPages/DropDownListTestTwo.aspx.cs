using KMSABET.MyDaos;
using KMSABET.MyPocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class DropDownListTestTwo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                AppDao appDaoObj = new AppDao();
                List<AppProgram> progList = appDaoObj.getProgramList();
                foreach (AppProgram prog in progList)
                {
                    ListItem att2 = new ListItem();
                    att2.Value = prog.programId.ToString();
                    att2.Text = prog.programName;
                    DropDownList1.Items.Add(att2);
                }

                fillCourseDropDown(DropDownList1.SelectedValue);
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCourseDropDown(DropDownList1.SelectedItem.Value);
            
        }

        protected void fillCourseDropDown(String programId)
        {
            DropDownList2.Items.Clear();
            AppDao appDaoObj = new AppDao();
            List<AppCourse> courseList = appDaoObj.getCourseList(programId);
            if (courseList != null)
                foreach (AppCourse course in courseList)
                {
                    ListItem att2 = new ListItem();

                    att2.Value = course.courseId.ToString();
                    att2.Text = course.courseName.ToString();

                    DropDownList2.Items.Add(att2);
                }
        }

    }
}