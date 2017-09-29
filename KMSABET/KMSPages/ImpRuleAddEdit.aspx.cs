using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class ImpRuleAddEdit : System.Web.UI.Page
    {
        
        protected override void OnPreInit(EventArgs e)
        {
            bool viewMode = Request.QueryString["viewMode"] == null ? false : Boolean.Parse(Request.QueryString["viewMode"]);
            bool editMode = Request.QueryString["editMode"] == null ? false : Boolean.Parse(Request.QueryString["editMode"]);
            bool deleteMode = Request.QueryString["deleteMode"] == null ? false : Boolean.Parse(Request.QueryString["deleteMode"]);
            this.Title = viewMode ? "View Rule" : editMode ? "Update Rule" : "Add Rule";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImpDao impDaoObj = new ImpDao();
            int cloID = Request.QueryString["cloID"] == null ? 0 : Int32.Parse(Request.QueryString["cloID"]);
            if (Page.IsPostBack == false)
            {
                int ruleID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
                bool viewMode = Request.QueryString["viewMode"] == null ? false : Boolean.Parse(Request.QueryString["viewMode"]);
                bool editMode = Request.QueryString["editMode"] == null ? false : Boolean.Parse(Request.QueryString["editMode"]);
                bool deleteMode = Request.QueryString["deleteMode"] == null ? false : Boolean.Parse(Request.QueryString["deleteMode"]);

                if (ruleID != 0)
                {
                    if (deleteMode)
                    {
                        deleteRule(ruleID);
                        Response.Redirect("~/KMSPages/ImpRuleList.aspx");
                    }

                    ImpRule rule = impDaoObj.getRuleByID(ruleID);
                    cloID = rule.cloData.cloId;
                    ruleStmt.Text = rule.ruleStatemet;

                    if (viewMode == true)
                    {
                        ruleStmt.Enabled = false;
                        DataList1.Enabled = false;
                    }

                    Button1.Visible = !viewMode;
                    cancelBtn.Text = viewMode ? "Close" : "Cancel";
                    Button1.Text = editMode ? "Update" : "Submit";
                }

                ViewState["cloID"] = cloID;

                AppCLO myClo = impDaoObj.getCloByCloID(cloID);
                TextBox1.Text = myClo.programName;
                TextBox2.Text = myClo.courseName;
                TextBox3.Text = myClo.cloStatement;

            }

            List<ImpRuleQuestion> quesList = impDaoObj.getRuleQuestionList(Int32.Parse(ViewState["cloID"].ToString()));
            DataList1.DataSource = quesList;
            DataList1.DataBind();
        }

        public void deleteRule(int ruleID)
        {
            DBUtils dbUtilsObj = new DBUtils();

            String deleteQuery = "DELETE FROM I_Rule"
                            + " WHERE rule_id = " + ruleID;
            dbUtilsObj.CUDOperations(deleteQuery);
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int ruleID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Get questionID here        
                int ruleQuesID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ruleQuesId"));
                int questionTypeID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ruleQuesTypeID"));
                HiddenField questionIDHidden = (HiddenField)e.Item.FindControl("ruleQuestionIDHidden");
                HiddenField ruleQuestionTypeIDHiddenBackend = (HiddenField)e.Item.FindControl("ruleQuestionTypeIDHidden");

                ruleQuestionTypeIDHiddenBackend.Value = questionTypeID.ToString();
                questionIDHidden.Value = ruleQuesID.ToString();

                if (questionTypeID == 1101)
                {
                    RadioButtonList RadioButtonList1 = (RadioButtonList)e.Item.FindControl("radlstPubs");
                    Panel queryPanelOneBackend = (Panel) e.Item.FindControl("queryPanelOne");
                    queryPanelOneBackend.Visible = false;

                    ImpDao impDaoObj = new ImpDao();
                    List<ImpRuleQuesAnswer> answerList = impDaoObj.getRuleAnswerList(ruleQuesID,questionTypeID);

                    RadioButtonList1.DataSource = answerList;
                    RadioButtonList1.DataTextField = "answerStatemet";
                    RadioButtonList1.DataValueField = "answerId";
                    RadioButtonList1.DataBind();

                    if (ruleID != 0)
                    {
                        ImpRuleQuesAnswer ruleAns = impDaoObj.getRuleAnswerByRuleIDQuestionID(ruleQuesID,ruleID);
                        RadioButtonList1.SelectedValue = ruleAns.answerId.ToString();
                    }
                }
                else if (questionTypeID == 1102)
                {
                    RadioButtonList RadioButtonList1 = (RadioButtonList)e.Item.FindControl("radlstPubs");
                    
                    DBUtils dbUtils1 = new DBUtils();
                    SqlDataReader attributeTypeReader1 = dbUtils1.readOperation("SELECT * FROM App_CODE Where CODE_TYPE_ID=1600");
                    while (attributeTypeReader1.Read())
                    {
                        RadioButtonList1.Items.Add(new ListItem(attributeTypeReader1["CODE_VALUE"].ToString(),
                            attributeTypeReader1["CODE_ID"].ToString()));
                    }
                    dbUtils1.closeDBConnection();

                    if (ruleID != 0)
                    {
                        ImpDao impDaoObj = new ImpDao();
                        ImpRuleQuesAnswer ruleAns = impDaoObj.getRuleDBValueByRuleIDQuestionID(ruleQuesID, ruleID);
                        RadioButtonList1.SelectedValue = ruleAns.comparisonTypeID.ToString();
                        TextBox comparisonValueTxtBox = (TextBox)e.Item.FindControl("dbQueryValue");
                        comparisonValueTxtBox.Text = ruleAns.comparisonValue.ToString();
                    }
                }

            }
        }

        protected void Submit_Button_Click1(object sender, EventArgs e)
        {
            int cloID = ViewState["cloID"] == null ? 0 : (int)ViewState["cloID"];
            int ruleID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
            if (ruleID != 0)
            {
                String updateQuery = "UPDATE I_Rule"
                        + " SET rule_statement = '"+ruleStmt.Text+"'"
                        + " WHERE rule_id = " + ruleID;
                DBUtils dbUtilsObj = new DBUtils();
                dbUtilsObj.CUDOperations(updateQuery);

                String deleteQuery = "DELETE FROM I_Rule_Case WHERE I_Rule_rule_id = " + ruleID;
                dbUtilsObj.CUDOperations(deleteQuery);
            }
            else
            {
                String insertionQuery
                    = "INSERT INTO I_Rule"
                       + " (I_CLO_clo_id"
                       + " ,rule_statement)"
                       + " output inserted.rule_id"
                       + " VALUES"
                       + " (" + cloID
                       + " , '" + ruleStmt.Text + "')";
                //LogUtils.myLog.Info("My Insert Query is: " + insertionQuery);
                DBUtils dbUtilsObj = new DBUtils();
                ruleID = dbUtilsObj.CUDOperationsScalar(insertionQuery);
            }
            //LogUtils.myLog.Info("Inserted ID of Question is: " + insertedID);

            foreach (DataListItem item in DataList1.Items)
            {
                if (((HiddenField)item.FindControl("ruleQuestionTypeIDHidden")).Value == "1101")
                {
                    if (((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem != null)
                    {
                        String selectedItemRdBtn = ((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem.Value;
                        String insertionQuery1
                            = "INSERT INTO I_Rule_Case"
                               + " (I_Rule_Ques_Answer_answer_id"
                               + " ,I_Rule_rule_id)"
                               + " VALUES (" + selectedItemRdBtn
                               + ", " + ruleID + ")";
                        DBUtils dbUtilsObj1 = new DBUtils();
                        dbUtilsObj1.CUDOperations(insertionQuery1);

                    }
                }
                else
                {
                    if (((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem != null)
                    {
                        String selectedItemRdBtn = ((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem.Value;
                        String insertionQuery1
                            = "INSERT INTO I_Rule_Case"
                               + " (COMPARISON_TYPE"
                               + " , COMPARISON_VALUE"
                               + " , I_RULE_QUESTION_ID"
                               + " ,I_Rule_rule_id)"
                               + " VALUES (" + selectedItemRdBtn
                               + " , " + ((TextBox)item.FindControl("dbQueryValue")).Text
                               + " , " + ((HiddenField)item.FindControl("ruleQuestionIDHidden")).Value
                               + ", " + ruleID + ")";
                        DBUtils dbUtilsObj1 = new DBUtils();
                        dbUtilsObj1.CUDOperations(insertionQuery1);

                    }
                }
            }
            Response.Redirect("~/KMSPages/ImpRuleList.aspx");
        }
    }
}