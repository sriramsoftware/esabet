using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class GridViewWithCheckBox : System.Web.UI.Page
    {
        List<QueQuestion> questionList = new List<QueQuestion>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FirstGridViewRow();
            }
            else
            {
                if (Session["questionList"] != null)
                {
                    questionList = (List<QueQuestion>)Session["questionList"];
                }
            }
        }

        private void FirstGridViewRow()
        {
            QueQuestion ques1 = new QueQuestion()
            {
                questionId = 112,
                questionStatement = "This is question One"
            };

            QueQuestion ques2 = new QueQuestion()
            {
                questionId = 2,
                questionStatement = "This is question Two"
            };

            
            questionList.Add(ques1);
            questionList.Add(ques2);

            Session["questionList"] = questionList;

            AttributeOptions.DataSource = questionList;
            AttributeOptions.DataBind();

        }

        protected void Print(object sender, EventArgs e)
        {
            int count = 0;

            foreach (GridViewRow gvr in AttributeOptions.Rows)
            {
                if ((gvr.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    LogUtils.myLog.Info("The cell selected value ID : " + questionList[count].questionId);
                }
                count++;
            }
        }
    }
}