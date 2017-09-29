using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class ImpAskSuggestion1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImpDao impDaoObj = new ImpDao();
            int cloID = Request.QueryString["cloID"] == null ? 0 : Int32.Parse(Request.QueryString["cloID"]);
            
            if (Page.IsPostBack == false)
            {
                AppCLO myClo = impDaoObj.getCloByCloID(cloID);
                TextBox1.Text = myClo.programName;
                TextBox2.Text = myClo.courseName;
                TextBox3.Text = myClo.cloStatement;
                expertSugLbl.Visible = false;
            }

            List<ImpRuleQuestion> quesList = impDaoObj.getRuleQuestionList(cloID);
            DataList1.DataSource = quesList;
            DataList1.DataBind();
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            String questionJSON = Request.QueryString["dbQueryValues"];
            List<ImpRuleQuestion> listOfQuestionValues = new List<ImpRuleQuestion>();
            if(questionJSON != "]")
                listOfQuestionValues = JsonConvert.DeserializeObject<List<ImpRuleQuestion>>(questionJSON);

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Get questionID here
                int ruleQuesID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ruleQuesId"));
                int questionTypeID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ruleQuesTypeID"));
                RadioButtonList RadioButtonList1 = (RadioButtonList)e.Item.FindControl("radlstPubs");
                HiddenField HiddenFieldQuesType = (HiddenField)e.Item.FindControl("hiddenQuesType");
                HiddenFieldQuesType.Value = questionTypeID.ToString();
                HiddenField HiddenFieldQuesID = (HiddenField)e.Item.FindControl("hiddenQuesID");
                HiddenFieldQuesID.Value = ruleQuesID.ToString();

                if (questionTypeID == 1101)
                {
                    ImpDao impDaoObj = new ImpDao();
                    List<ImpRuleQuesAnswer> answerList = impDaoObj.getRuleAnswerList(ruleQuesID, questionTypeID);

                    RadioButtonList1.DataSource = answerList;
                    RadioButtonList1.DataTextField = "answerStatemet";
                    RadioButtonList1.DataValueField = "answerId";
                    RadioButtonList1.DataBind();
                }
                else if (questionTypeID == 1102)
                {
                    String calculatedValue = "";

                    foreach (ImpRuleQuestion quesInner in listOfQuestionValues)
                    {
                        if (quesInner.ruleQuesId == ruleQuesID)
                        {
                            calculatedValue = quesInner.dbQueryCalculatedValue.ToString();
                        }
                    }
                    HiddenField HiddenFieldCalculatedValue = (HiddenField)e.Item.FindControl("HiddenCalculatedValue");
                    HiddenFieldCalculatedValue.Value = calculatedValue.ToString();

                    DBUtils dbUtils1 = new DBUtils();
                    SqlDataReader attributeTypeReader1 = dbUtils1.readOperation("SELECT * FROM App_CODE Where CODE_TYPE_ID=1600");
                    while (attributeTypeReader1.Read())
                    {
                        RadioButtonList1.Items.Add(new ListItem(attributeTypeReader1["CODE_VALUE"].ToString() + " = " + calculatedValue,
                            attributeTypeReader1["CODE_ID"].ToString()));
                    }
                    dbUtils1.closeDBConnection();

                }

            }
        }

        protected void Submit_Button_Click1(object sender, EventArgs e)
        {
            int selectedItemCount = 0;
            String selectedItems = "";
            String questionJSON = Request.QueryString["dbQueryValues"];
            List<ImpRuleQuestion> listOfQuestionValues = new List<ImpRuleQuestion>();
            if (questionJSON != "]")
                listOfQuestionValues = JsonConvert.DeserializeObject<List<ImpRuleQuestion>>(questionJSON);
            String dbQueryStr = "";

            foreach (DataListItem item in DataList1.Items)
            {
                if (((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem != null)
                {
                    HiddenField HiddenFieldQuesType = (HiddenField)item.FindControl("hiddenQuesType");
                    if (HiddenFieldQuesType.Value == "1101")
                    {
                        selectedItemCount++;
                        selectedItems += ((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem.Value + ",";
                    }
                    else
                    {
                        String selectedValue = ((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem.Value;
                        HiddenField HiddenFieldQuesID = (HiddenField)item.FindControl("hiddenQuesID");
                        HiddenField HiddenFieldCalculatedValue = (HiddenField)item.FindControl("HiddenCalculatedValue");
                        ImpRuleQuestion quesRet = searchQuestionDetails(Int32.Parse(HiddenFieldQuesID.Value), listOfQuestionValues);
                        if (dbQueryStr != "")
                        {
                            dbQueryStr += " OR";
                        }
                        dbQueryStr += " (IR2.I_RULE_QUESTION_ID = " + HiddenFieldQuesID.Value;
                        if (selectedValue.Equals("1601"))
                            dbQueryStr += " AND IR2.COMPARISON_VALUE = " + HiddenFieldCalculatedValue.Value + " )";
                        else if (selectedValue.Equals("1602"))
                            dbQueryStr += " AND IR2.COMPARISON_VALUE > " + HiddenFieldCalculatedValue.Value + " )";
                        else if (selectedValue.Equals("1603"))
                            dbQueryStr += " AND IR2.COMPARISON_VALUE < " + HiddenFieldCalculatedValue.Value + " )";
                        else if (selectedValue.Equals("1604"))
                            dbQueryStr += " AND IR2.COMPARISON_VALUE >= " + HiddenFieldCalculatedValue.Value + " )";
                        else if (selectedValue.Equals("1605"))
                            dbQueryStr += " AND IR2.COMPARISON_VALUE <= " + HiddenFieldCalculatedValue.Value + " )";
                    }
                }
            }
            if (selectedItems.Length > 0)
                selectedItems = selectedItems.Substring(0, selectedItems.Length-1);
            //LogUtils.myLog.Info("Selected Item of Radio Button is: " + selectedItems + " and length is : " + selectedItemCount);
            List<int> ruleIdsList = new List<int>();

            if (selectedItemCount > 0 || !dbQueryStr.Equals(""))
            {
                expertSugLbl.Visible = true;
                String myQuery = "SELECT * FROM I_Rule R"
                        + " WHERE R.rule_id IN ("
	                    + " SELECT I_Rule_rule_id"
	                    + " FROM I_Rule_Case IR2"
	                    + " WHERE 1=1 AND (";
                if (selectedItemCount > 0)
                {
                    myQuery += " I_Rule_Ques_Answer_answer_id IN (" + selectedItems + ")";
                    if (dbQueryStr != "") myQuery += " OR ";
                }
                
                myQuery += dbQueryStr
                        + ") GROUP BY I_Rule_rule_id"
                        + " HAVING COUNT(IR2.I_Rule_rule_id) ="
                            + " (SELECT COUNT(*) FROM I_RULE_CASE IR1"
                            + " WHERE IR1.I_Rule_rule_id = IR2.I_Rule_rule_id)"
                        + " )";

                DBUtils dbUtilObj = new DBUtils();
                SqlDataReader readerQueriesList = dbUtilObj.readOperation(myQuery);

                List<ImpRule> impRuleListObj = new List<ImpRule>();
                if (readerQueriesList != null && readerQueriesList.HasRows)
                {

                    while (readerQueriesList.Read())
                    {
                        
                        ImpRule impRuleObj = new ImpRule();
                        impRuleObj.ruleId = (int)readerQueriesList["rule_id"];
                        impRuleObj.ruleStatemet = readerQueriesList["rule_statement"].ToString();
                        impRuleListObj.Add(impRuleObj);
                        
                    }
                    readerQueriesList.Close();

                }
                else
                {
                    ImpRule impRuleObj = new ImpRule();
                    impRuleObj.ruleId = 1;
                    impRuleObj.ruleStatemet = "No suggestion is available";
                    impRuleListObj.Add(impRuleObj);
                }

                ruleListTag.DataSource = impRuleListObj;
                ruleListTag.DataBind();
            }
        }

        public ImpRuleQuestion searchQuestionDetails(int quesID, List<ImpRuleQuestion> quesList)
        {
            ImpRuleQuestion quesFound = null;
            foreach (ImpRuleQuestion ques in quesList)
            {
                if (ques.ruleQuesId == quesID)
                {
                    quesFound = ques;
                }
            }
            return quesFound;
        }
    }
}