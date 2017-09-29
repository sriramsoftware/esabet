using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class ImpAskSuggestionTwo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int cloID = Request.QueryString["cloID"] == null ? 0 : Int32.Parse(Request.QueryString["cloID"]);
            ImpDao impDaoObj = new ImpDao();
            GeneralUtils genUtilObj = new GeneralUtils();
            List<ImpRuleQuestion> dbTypeQuestionList = impDaoObj.getRuleQuestionList(cloID, 1102);
            List<AskUserWhereAttributes> listAskWhereUserDetails = new List<AskUserWhereAttributes>();

            for (int i = 0; i < dbTypeQuestionList.Count; i++)
            {
                String dbQueryValue = dbTypeQuestionList[i].dBQueryStatement;
                List<int> openBracketOccurences = genUtilObj.getCharacterOccurancesInString(dbQueryValue, '{');
                if (openBracketOccurences.Count > 0)
                {
                    List<int> closeBracketOccurences = genUtilObj.getCharacterOccurancesInString(dbQueryValue, '}');
                    List<int> endBracketCalculation = genUtilObj.getEndDifferenceValue(openBracketOccurences, closeBracketOccurences);
                    List<String> jsonAskUserList = genUtilObj.getJSONStringAskUser(dbQueryValue, openBracketOccurences, endBracketCalculation);
                    for (int j = 0; j < jsonAskUserList.Count; j++)
                    {
                        AskUserWhereAttributes localAskUserAttribute = JsonConvert.DeserializeObject<AskUserWhereAttributes>(jsonAskUserList[j]);
                        localAskUserAttribute.questionID = dbTypeQuestionList[i].ruleQuesId;
                        localAskUserAttribute.questionStatement = dbTypeQuestionList[i].ruleQuestionStatemet;
                        localAskUserAttribute.whereAskUserToReplace = jsonAskUserList[j];
                        listAskWhereUserDetails.Add(localAskUserAttribute);
                    }
                }
            }

            DataList1.DataSource = listAskWhereUserDetails;
            DataList1.DataBind();
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int cloID = Request.QueryString["cloID"] == null ? 0 : Int32.Parse(Request.QueryString["cloID"]);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Get questionID here        
                String tableName = DataBinder.Eval(e.Item.DataItem, "tableName").ToString();
                String pkColumnName = DataBinder.Eval(e.Item.DataItem, "pkColumnName").ToString();
                String displayColumn = DataBinder.Eval(e.Item.DataItem, "displayColumn").ToString();
                String headingLabel = DataBinder.Eval(e.Item.DataItem, "headingLabel").ToString();
                String questionStatement = DataBinder.Eval(e.Item.DataItem, "questionStatement").ToString();
                int questionID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "questionID"));

                DropDownList DropDownList1 = (DropDownList)e.Item.FindControl("ddlItemTemp");

                DBQueDao dbQueDaoObj = new DBQueDao();
                List<ListItem> ddValueList = dbQueDaoObj.getColumnValues(tableName, displayColumn);

                DropDownList1.DataSource = ddValueList;
                DropDownList1.DataTextField = "Text";
                DropDownList1.DataValueField = "Value";
                DropDownList1.DataBind();
            }
        }

        protected void Button1_Click(object Source, EventArgs e)
        {
            List<AskUserWhereAttributes> listAttributes = new List<AskUserWhereAttributes>();
            ImpDao impDaoObj = new ImpDao();
            int cloID = Request.QueryString["cloID"] == null ? 0 : Int32.Parse(Request.QueryString["cloID"]);
            List<ImpRuleQuestion> dbTypeQuestionList = impDaoObj.getRuleQuestionList(cloID, 1102);
            
            foreach (DataListItem item in DataList1.Items)
            {
                if (((DropDownList)item.FindControl("ddlItemTemp")).SelectedItem != null)
                {
                    AskUserWhereAttributes localAttr = new AskUserWhereAttributes();
                    HiddenField jsonStringHiddenElem = (HiddenField)item.FindControl("jsonStringHidden");
                    String jsonValue = jsonStringHiddenElem.Value;
                    HiddenField questionIDHiddenElem = (HiddenField)item.FindControl("questionIDHidden");
                    int questionID = Convert.ToInt32(questionIDHiddenElem.Value);
                    String selectedItemDDL = ((DropDownList)item.FindControl("ddlItemTemp")).SelectedItem.Value;
                    localAttr.whereAskUserToReplace = jsonValue;
                    localAttr.questionID = questionID;
                    localAttr.selectedItemValue = Convert.ToInt32(selectedItemDDL);
                    listAttributes.Add(localAttr);
                }
            }

            String questionJSON = "[";
            foreach (ImpRuleQuestion quesItem in dbTypeQuestionList)
            {
                foreach (AskUserWhereAttributes attr in listAttributes)
                {
                    if (quesItem.ruleQuesId == attr.questionID)
                    {
                        quesItem.dBQueryStatement = 
                            quesItem.dBQueryStatement.Replace(attr.whereAskUserToReplace, attr.selectedItemValue.ToString());
                    }
                }
                int returnedResult = impDaoObj.executeSavedDBQuery(quesItem.dBQueryStatement);
                quesItem.dbQueryCalculatedValue = returnedResult;
                questionJSON += "{ruleQuesId : " + quesItem.ruleQuesId + ", dbQueryCalculatedValue : " + quesItem.dbQueryCalculatedValue + "},";
                
            }
            questionJSON = questionJSON.Substring(0, questionJSON.Length - 1);
            questionJSON += "]";
            Response.Redirect("~/KMSPages/ImpAskSuggestion.aspx?cloID=" + cloID + "&dbQueryValues="+questionJSON);
        }

    }
}