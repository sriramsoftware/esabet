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
    public partial class QueFavoriteListAddEdit : System.Web.UI.Page
    {
        List<QueQuestion> questionList = new List<QueQuestion>();
        List<QueQuestion> selectedQuestionList = new List<QueQuestion>();
        List<QueQuestionScore> questionScoreList = new List<QueQuestionScore>();
        Dictionary<String, String> selectedItemList = new Dictionary<String, String>();
        List<QuestionRangedValue> rangedValueList = new List<QuestionRangedValue>();

        QueDao queDaoObj = new QueDao();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false && Session["CourseID"] != null)
            {
                fillCLODropDown(Session["CourseID"].ToString());
                fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
                fillSODropDown(DropDownList3.SelectedItem.Value);

                ViewState["questionList"] = null;
                ViewState["selectedQuestionList"] = null;
                ViewState["questionScoreList"] = null;
                
                Button myNextBtn = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepNextButton");
                Button myPreviousBtn = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
                if (myPreviousBtn != null)
                {
                    //LogUtils.myLog.Info("My Previous Button is not null");
                    myPreviousBtn.Visible = false;
                }

                bool editMode = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
                if (editMode)
                {
                    label1.Text = "Update Assessment";
                    Wizard1.StartNextButtonText = "Add/Delete Questions";
                    setEditData();
                }
                else
                {
                    label1.Text = "Add Assessment";
                    Wizard1.ActiveStepIndex = 1;
                }
            }
            else
            {
                if (ViewState["questionList"] != null)
                {
                    questionList = (List<QueQuestion>)ViewState["questionList"];
                }
                
                if (ViewState["selectedQuestionList"] != null)
                {
                    selectedQuestionList = (List<QueQuestion>)ViewState["selectedQuestionList"];
                }
                if (ViewState["questionScoreList"] != null)
                {
                    questionScoreList = (List<QueQuestionScore>)ViewState["questionScoreList"];
                }

            }

            bool editMode1 = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
            if (editMode1) this.Title = "Update Assessment";
            else this.Title = "Add Assessment";

            if (Session["CourseID"] != null)
            {
                String courseIdLocal = Session["CourseID"].ToString();
                List<KMSABET.MyPocos.QueAttribute> attributeList = queDaoObj.getAttrbuteList(courseIdLocal);
                DataList12.DataSource = attributeList;
                DataList12.DataBind();
            }
        }

        public void setEditData()
        {
            int favQueListID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
            if (favQueListID != 0)
            {
                QueDao queDaoObj = new QueDao();
                MyPocos.QueFavQuestionList favList = queDaoObj.getFavQuestionListByID(favQueListID);

                questionList = queDaoObj.getQuestionListByFavID(favQueListID);
                ViewState["selectedQuestionList"] = questionList;
                
                favListName.Text = favList.favQuestionName;
                questionsListSelectedTag.DataSource = questionList;
                questionsListSelectedTag.DataBind();


                favListName2.Text = favList.favQuestionName;
                questionsListSelectedTag2.DataSource = questionList;
                questionsListSelectedTag2.DataBind();

                Wizard1.ActiveStepIndex = 0;
            }
        }

        protected void Wizard1_PreviousButtonClick(object sender, WizardNavigationEventArgs e)
        {
            setNextPrevBtnLbls(Wizard1.ActiveStepIndex-2);
            Button myPreviousBtn = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
            if (Wizard1.ActiveStepIndex == 2)
            {

            }

            bool editMode = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
            if (Wizard1.ActiveStepIndex == 2
                || (editMode && Wizard1.ActiveStepIndex == 1))
            {
                if (myPreviousBtn != null)
                {
                    //LogUtils.myLog.Info("My Previous Button is not null");
                    myPreviousBtn.Visible = false;
                }
            }
            else
            {
                if (myPreviousBtn != null)
                {
                    //LogUtils.myLog.Info("My Previous Button is not null");
                    myPreviousBtn.Visible = true;
                }

            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            setNextPrevBtnLbls(Wizard1.ActiveStepIndex);
            Button myNextBtn = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepNextButton");
            if (Wizard1.ActiveStepIndex == 1)
            {
                //Wizard1.ActiveStepIndex = 1;

                int selectedItemCount = 0;
                List<QuestionRangedValue> rangedValueList = new List<QuestionRangedValue>();
                
                selectedItemList = new Dictionary<String, String>();
                List<int> selectedItemIds = new List<int>();
                foreach (DataListItem item in DataList12.Items)
                {
                    String attributeTypeIDValue = ((HiddenField)item.FindControl("attributeTypeHidden")).Value;
                    String attributeIDValue = ((HiddenField)item.FindControl("attributeIDHidden")).Value;
                    if (attributeTypeIDValue.Equals("1001"))
                    {
                        RadioButtonList rdBtn = ((RadioButtonList)item.FindControl("radlstPubs"));
                        ListItem selItem = rdBtn.SelectedItem;
                        if (selItem != null)
                        {
                            Int32 ignoreThisValue = Int32.Parse("214748364") + Int32.Parse(attributeIDValue);
                            if (selItem.Value != ignoreThisValue.ToString())
                            {
                                selectedItemIds.Add(Int32.Parse(selItem.Value));
                                selectedItemCount++;
                            }
                            selectedItemList[selItem.Value] = selItem.Text;
                        }
                    }
                    else
                    {
                        QuestionRangedValue que = new QuestionRangedValue();
                        String fromValue = ((TextBox)item.FindControl("fromText")).Text;
                        String toValue = ((TextBox)item.FindControl("toText")).Text;
                        que.fromValue = fromValue;
                        que.toValue = toValue;
                        que.attributeID = attributeIDValue;
                        if(que.fromValue != "" && que.toValue != "") rangedValueList.Add(que);
                    }
                }
                ViewState["searchCriteria"] = selectedItemList;
                ViewState["searchCriteriaRangeValue"] = rangedValueList;

                selectedQuestionPanel.Visible = false;

                //LogUtils.myLog.Info("Selected Item of Radio Button is: " + selectedItems + " and length is : " + selectedItemCount);
                List<int> questionsIdsList = new List<int>();
                List<QueQuestion> quesScoreList = new List<QueQuestion>();

                if (selectedItemCount > 0)
                {
                    QueAssessmentSearchCriteria criteria = new QueAssessmentSearchCriteria();
                    criteria.courseId = Int32.Parse(Session["CourseID"].ToString());
                    criteria.cloId = Int32.Parse(DropDownList3.SelectedItem.Value);
                    criteria.soId = Int32.Parse(DropDownList4.SelectedItem.Value);
                    criteria.courseTopicId = Int32.Parse(DropDownList5.SelectedItem.Value);
                    criteria.selectedAttributeCount = selectedItemCount + rangedValueList.Count;

                    quesScoreList = queDaoObj.getQuesScoreListInferenceEngine(rangedValueList, selectedItemIds, "15", criteria);

                    String quesIds = "";
                    foreach (QueQuestion que in quesScoreList)
                    {
                        questionsIdsList.Add(que.questionId);
                        quesIds += que.questionId + ",";
                    }

                    LogUtils.myLog.Info(quesIds);
                }

                //questionList = queDaoObj.questionList(questionsIdsList);
                //questionList = updateScore(questionList, quesScoreList);

                questionList = quesScoreList;
                ViewState["questionList"] = questionList;

                questionListTag.DataSource = questionList;
                questionListTag.DataBind();

                bool editMode = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
                if (!editMode) Wizard1.ActiveStepIndex = 2;

            }
            else if (Wizard1.ActiveStepIndex == 2)
            {
                int count = 0;

                foreach (GridViewRow gvr in questionListTag.Rows)
                {
                    if ((gvr.FindControl("CheckBox1") as CheckBox).Checked)
                    {
                        if (alreadyExists(questionList[count].questionId) == false)
                        {
                            selectedQuestionList.Add(questionList[count]);
                            List<QuestionRangedValue> rangedValueList = new List<QuestionRangedValue>();
                            String selectedAttrOptIdsStr = getSelectedSearchCriteria(rangedValueList);
                            
                            List<QueQuestionScore> questionScoreListForCurrentQuestion =
                                queDaoObj.getQuestionScoreDetailsByID(questionList[count].questionId, rangedValueList, selectedAttrOptIdsStr);
                            questionScoreList.AddRange(questionScoreListForCurrentQuestion);
                        }
                        //LogUtils.myLog.Info("The cell selected value ID : " + questionList[count].questionId);
                    }
                    count++;
                }

                ViewState["selectedQuestionList"] = selectedQuestionList;
                ViewState["questionScoreList"] = questionScoreList;

                questionsListSelectedTag.DataSource = selectedQuestionList;
                questionsListSelectedTag.DataBind();
            }

            Button myPreviousBtn = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
            if (myPreviousBtn != null)
            {
                //LogUtils.myLog.Info("My Previous Button is not null");
                myPreviousBtn.Visible = true;
            }
        }

        protected void fillCLODropDown(String courseId)
        {
            DropDownList3.Items.Clear();
            AppDao appDaoObj = new AppDao();
            List<AppCLO> CLOList = appDaoObj.getCLOList(courseId);
            if (CLOList != null)
            {
                foreach (AppCLO CLO in CLOList)
                {
                    ListItem att2 = new ListItem();

                    att2.Value = CLO.cloId.ToString();
                    att2.Text = CLO.cloStatement.ToString();

                    DropDownList3.Items.Add(att2);
                }
                DropDownList3.SelectedIndex = 0;
            }
        }

        protected void fillCourseTopicDropDownByCloId(String cloId)
        {
            DropDownList5.Items.Clear();
            AppDao appDaoObj = new AppDao();
            List<AppCourseTopic> CLOList = appDaoObj.getCourseTopicListByCloId(cloId);
            if (CLOList != null)
            {
                foreach (AppCourseTopic CLO in CLOList)
                {
                    ListItem att2 = new ListItem();

                    att2.Value = CLO.id.ToString();
                    att2.Text = CLO.topic.ToString();

                    DropDownList5.Items.Add(att2);
                }
                DropDownList5.SelectedIndex = 0;
            }

        }

        protected void fillSODropDown(String cloId)
        {
            DropDownList4.Items.Clear();
            AppDao appDaoObj = new AppDao();
            List<AppSO> SOList = appDaoObj.getSOList(cloId);
            if (SOList != null)
            {
                foreach (AppSO SO in SOList)
                {
                    ListItem att2 = new ListItem();

                    String soStatemnet = SO.statement.ToString();

                    if (soStatemnet.Length > 135)
                    {
                        soStatemnet = soStatemnet.Substring(0, 135) + "...";
                    }

                    att2.Value = SO.id.ToString();
                    att2.Text = soStatemnet;

                    DropDownList4.Items.Add(att2);
                }
                DropDownList4.SelectedIndex = 0;
            }
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCLODropDown(Session["CourseID"].ToString());
            fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
            fillSODropDown(DropDownList3.SelectedItem.Value);
        }

        protected void ddlClo_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
            fillSODropDown(DropDownList3.SelectedItem.Value);
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCLODropDown(Session["CourseID"].ToString());
            fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
            fillSODropDown(DropDownList3.SelectedItem.Value);
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //Get attributeId here        
                int AttributeID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "attributeID"));
                int AttributeTypeID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "attributeTypeID"));
                //pass Question ID to your DB and get all available options for the question       
                //Bind the RadiobUttonList here  

                if (AttributeTypeID == 1001)
                {
                    RadioButtonList RadioButtonList1 = (RadioButtonList)e.Item.FindControl("radlstPubs");

                    List<KMSABET.MyPocos.QueAttributeOption> optionsList = new List<KMSABET.MyPocos.QueAttributeOption>();

                    List<QueAttributeOption> options = queDaoObj.getAttributeOptionListByAttrID(AttributeID.ToString());
                    foreach (QueAttributeOption option in options)
                    {
                        optionsList.Add(option);
                    }
                    QueAttributeOption optionIgnoreThis = new QueAttributeOption();
                    optionIgnoreThis.attributeOptionId = 214748364 + AttributeID;
                    optionIgnoreThis.optionStatement = "Ignore This";
                    optionsList.Add(optionIgnoreThis);

                    RadioButtonList1.DataSource = optionsList;

                    Boolean goneInside = false;
                    if (ViewState["searchCriteria"] != null)
                    {
                        selectedItemList = (Dictionary<String, String>)ViewState["searchCriteria"];
                        foreach (QueAttributeOption item in optionsList)
                        {
                            if (selectedItemList.ContainsKey(item.attributeOptionId.ToString()))
                            {
                                goneInside = true;
                                RadioButtonList1.SelectedValue = item.attributeOptionId.ToString();
                            }
                        }
                    }
                    if (goneInside == false)
                    {
                        RadioButtonList1.SelectedIndex = 0;
                    }

                    RadioButtonList1.DataTextField = "optionStatement";
                    RadioButtonList1.DataValueField = "attributeOptionId";
                    RadioButtonList1.DataBind();
                }
                else
                {
                    Label fromLabelComp = (Label)e.Item.FindControl("fromLabel");
                    Label toLabelComp = (Label)e.Item.FindControl("toLabel");
                    TextBox fromTextComp = (TextBox)e.Item.FindControl("fromText");
                    TextBox toTextComp = (TextBox)e.Item.FindControl("toText");
                    fromLabelComp.Visible = true;
                    toLabelComp.Visible = true;
                    fromTextComp.Visible = true;
                    toTextComp.Visible = true;
                    if (ViewState["searchCriteriaRangeValue"] != null)
                    {
                        rangedValueList = (List<QuestionRangedValue>) ViewState["searchCriteriaRangeValue"];
                        foreach(QuestionRangedValue rangedValue in rangedValueList) 
                        {
                            if (rangedValue.attributeID == AttributeID.ToString())
                            {
                                fromTextComp.Text = rangedValue.fromValue;
                                toTextComp.Text = rangedValue.toValue;
                            }
                        }
                    }
                }
            }
        }

        Boolean alreadyExists(int questionId)
        {
            foreach (QueQuestion questionLocal in selectedQuestionList)
            {
                if (questionLocal.questionId == questionId)
                {
                    return true;
                }
            }
            return false;
        }

        protected void Wizard1_FinishButtonClick(object sender, EventArgs e)
        {
            bool editMode = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
            int favID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);

            String selectedItems = "";
            selectedItemList = (Dictionary<String, String>)ViewState["searchCriteria"];
            foreach (KeyValuePair<string, string> entry in selectedItemList)
            {
                selectedItems += entry.Key + ",";
            }
            if (selectedItems.Length > 0)
                selectedItems = selectedItems.Substring(0, selectedItems.Length - 1);

            if (editMode)
            {
                try
                {
                    String assessmentName = favListName.Text;
                    String courseId = Session["CourseID"].ToString();
                    String instructorId = Session["userId"].ToString();
                    String updateQuery
                    = "UPDATE Q_Favorite_List"
                        + " SET Favorite_list_name = '" + assessmentName + "'"
                        + " , App_Instructor_instructor_id = " + instructorId
                        + " , App_Course_course_id = " + courseId
                        + " WHERE favorite_list_id = " + favID;
                    DBUtils dbUtilsObj = new DBUtils();
                    dbUtilsObj.CUDOperations(updateQuery);

                    String deleteQuery =
                        "DELETE FROM Q_Favorite_List_has_Q_Question WHERE Q_Favorite_List_favorite_list_id = " + favID;
                    dbUtilsObj.CUDOperations(deleteQuery);
                }
                catch (Exception ex)
                {
                    LogUtils.myLog.Error("Error while adding assessment : ", ex);
                }
            }
            else
            {
                String insertionQuery
                = "INSERT INTO Q_Favorite_List ( Favorite_list_name, App_Instructor_instructor_id, App_Course_course_id) "
                    + " output inserted.favorite_list_id"
                    + " VALUES ('"
                    + favListName.Text
                    + "', " + Session["userId"].ToString() + ", " + Session["CourseID"].ToString() + ")";
                //LogUtils.myLog.Info("My Insert Query is: " + insertionQuery);
                DBUtils dbUtilsObj = new DBUtils();
                favID = dbUtilsObj.CUDOperationsScalar(insertionQuery);
                //LogUtils.myLog.Info("Inserted ID of Question is: " + insertedID);
            }

            List<Int32> queList = new List<Int32>();
            foreach (QueQuestion que in selectedQuestionList)
            {
                queList.Add(que.questionId);
                String insertQuery
                    = "INSERT INTO  Q_Favorite_List_has_Q_Question   ( Q_Favorite_List_favorite_list_id   , Q_Question_question_id ) "
                    + "VALUES  (" + favID + "," + que.questionId + ")";
                DBUtils dbUtilsObj1 = new DBUtils();
                dbUtilsObj1.CUDOperations(insertQuery);
            }

            queDaoObj.insertAssessmentQuesScoreList(favID, questionScoreList);

            Response.Redirect("~/KMSPages/QueFavQuestionList.aspx");
        }

        protected void DeleteSelected(object sender, EventArgs e)
        {
            int count = 0;

            //LogUtils.myLog.Info("The count of selectedQUestionList : " + selectedQuestionList.Count);
            foreach (GridViewRow gvr in questionsListSelectedTag.Rows)
            {
                if ((gvr.FindControl("CheckBox123") as CheckBox).Checked)
                {
                    LogUtils.myLog.Info("Count is: " + count + " and The cell selected value ID : " + selectedQuestionList[count].questionId);
                    foreach (QueQuestionScore queScore in questionScoreList.ToList())
                    {
                        if (queScore.questionID == selectedQuestionList[count].questionId)
                        {
                            questionScoreList.Remove(queScore);
                        }
                    }
                    selectedQuestionList.RemoveAt(count);
                    count--;
                }
                count++;
            }

            ViewState["selectedQuestionList"] = selectedQuestionList;
            ViewState["questionScoreList"] = questionScoreList;

            questionsListSelectedTag.DataSource = selectedQuestionList;
            questionsListSelectedTag.DataBind();
        }

        private List<QueQuestion> updateScore(List<QueQuestion> quesList, List<QueQuestion> queScoreList)
        {
            foreach (QueQuestion queScore in queScoreList)
            {
                foreach (QueQuestion que in quesList)
                {
                    if (queScore.questionId == que.questionId)
                    {
                        que.sumScore = queScore.sumScore;
                    }
                }
            }
            return quesList;
        }

        public void MyBtnHandlerEditView(Object sender, EventArgs e)
        {

            selectedQuestionPanel.Visible = true;
            int favQueListID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
            Button btn = (Button)sender;
            selectedQuestionStatment.Text = btn.CommandName.ToString();
            int quesID = Int32.Parse(btn.CommandArgument);
            QueDao queDaoObj = new QueDao();
            List<FavListQueScore> favQueDetailList = queDaoObj.getQuestionDetailsByQuesIDAndAssessmentID(favQueListID, quesID);
            int sum = 0;
            foreach (FavListQueScore queScore in favQueDetailList)
            {
                sum += queScore.sumScoreValue;
            }

            Label4.Text = sum.ToString();
            selectedQuestionDetailsView.DataSource = favQueDetailList;
            selectedQuestionDetailsView.DataBind();

            AjaxControlToolkit.ModalPopupExtender ModalPopupExtender =
               this.Page.FindControl("ctl00$MainContent$MsgBoxModalPopupExtender") as AjaxControlToolkit.ModalPopupExtender;
            UpdatePanel UpdatePanel = this.Page.FindControl("ctl00$MainContent$UpdatePanelMsgBox") as UpdatePanel;
            UpdatePanel.Update();
            ModalPopupExtender.Show();
        }

        public void MyBtnHandler(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int quesID = Int32.Parse(btn.CommandArgument);
            LogUtils.myLog.Info("Question ID : " + quesID);
            selectedQuestionPanel.Visible = true;
            selectedQuestionStatment.Text = btn.CommandName.ToString();

            selectedItemList = (Dictionary<String, String>)ViewState["searchCriteria"];
            List<QuestionRangedValue> rangedValueList = new List<QuestionRangedValue>();
            String selectedAttrOptIdsStr = getSelectedSearchCriteria(rangedValueList);

            List<QueQuestionScore> questionScoreList = queDaoObj.getQuestionScoreDetailsByID(quesID, rangedValueList, selectedAttrOptIdsStr);

            int sum = 0;
            foreach ( QueQuestionScore queScore in questionScoreList) {
                sum += queScore.sumScoreValue;
            }

            Label4.Text = sum.ToString();
            selectedQuestionDetailsView.DataSource = questionScoreList;
            selectedQuestionDetailsView.DataBind();

            AjaxControlToolkit.ModalPopupExtender ModalPopupExtender =
               this.Page.FindControl("ctl00$MainContent$MsgBoxModalPopupExtender") as AjaxControlToolkit.ModalPopupExtender;
            UpdatePanel UpdatePanel = this.Page.FindControl("ctl00$MainContent$UpdatePanelMsgBox") as UpdatePanel;
            UpdatePanel.Update();
            ModalPopupExtender.Show();
        }

        private void setNextPrevBtnLbls(int stepNumber)
        {
            Button myPreviousBtn = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
            Button myNextBtn = (Button)Wizard1.FindControl("StepNavigationTemplateContainerID$StepNextButton");
            if (stepNumber == 0)
            {
                myPreviousBtn.Text = "View Questions of Assessment";
                myNextBtn.Text = "Search Questions";
            }
            else if (stepNumber == 1)
            {
                myNextBtn.Text = "Pick Selected Question";
                myPreviousBtn.Text = "Back to Search Criteria";
            }
            else if (stepNumber == 2)
            {
                Button myFinishBtn = (Button)Wizard1.FindControl("FinishNavigationTemplateContainerID$Button1");
                bool editMode = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
                if (editMode)
                {
                    myFinishBtn.Text = "Update Assessment";
                }
                else
                {
                    myFinishBtn.Text = "Add Assessment";
                }
            }
        }

        private String getSelectedSearchCriteria(List<QuestionRangedValue> rangedValueList)
        {
            String selectedAttrOptIdsStr = "";
            foreach (KeyValuePair<string, string> entry in selectedItemList)
            {
                selectedAttrOptIdsStr += entry.Key + ",";
            }
            if (selectedAttrOptIdsStr.Length > 0)
                selectedAttrOptIdsStr = selectedAttrOptIdsStr.Substring(0, selectedAttrOptIdsStr.Length - 1);

            foreach (DataListItem item in DataList12.Items)
            {
                String attributeTypeIDValue = ((HiddenField)item.FindControl("attributeTypeHidden")).Value;
                String attributeIDValue = ((HiddenField)item.FindControl("attributeIDHidden")).Value;
                if (attributeTypeIDValue.Equals("1002"))
                {
                    QuestionRangedValue que = new QuestionRangedValue();
                    String fromValue = ((TextBox)item.FindControl("fromText")).Text;
                    String toValue = ((TextBox)item.FindControl("toText")).Text;
                    que.fromValue = fromValue;
                    que.toValue = toValue;
                    que.attributeID = attributeIDValue;
                    rangedValueList.Add(que);
                }
            }
            return selectedAttrOptIdsStr;
        }
    }
}