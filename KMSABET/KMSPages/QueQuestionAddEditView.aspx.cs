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
    public partial class CQIQuestionAdd : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            Page.Title = string.Format("Add a Question");
            String value = ClientQueryString;
            String[] valueArr = value.Split('=', '&');
            if (valueArr.Count() > 1)
            {
                bool viewMode = valueArr[3] == "true" ? true : false;
                bool editMode = valueArr[5] == "true" ? true : false;
                this.Title = viewMode ? "View a Question" : editMode ? "Update a Question" : "Add a Question";
            }
            else
            {
                this.Title = "Add a Question";
            }
                     
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            QueDao queDaoObj = new QueDao();

            if (Page.IsPostBack == false)
            {
                fillRelevanceScore(scoreTxtBx1, true);
                fillRelevanceScore(TextBox1, true);
                fillRelevanceScore(TextBox2, true);
                fillCLODropDown(Session["CourseID"].ToString());
                if (DropDownList3.SelectedItem != null)
                {
                    fillSODropDown(DropDownList3.SelectedItem.Value);
                    fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
                }

                String value = ClientQueryString;
                String[] valueArr = value.Split('=', '&');
                if (valueArr.Count() > 1)
                {
                    int inputId = Int32.Parse(valueArr[1]);
                    bool deleteMode = valueArr[7] == "true" ? true : false;
                    bool viewMode = valueArr[3] == "true" ? true : false;
                    bool editMode = valueArr[5] == "true" ? true : false;

                    if (deleteMode) deleteQuestion(inputId);

                    this.Title = viewMode ? "View a Question" : "Update a Question";
                    ViewState["viewMode"] = viewMode;
                    ViewState["editMode"] = editMode;
                    ViewState["deleteMode"] = deleteMode;

                    List<int> idsList = new List<int>();
                    idsList.Add(inputId);
                    List<QueQuestion> queObjectTemp = queDaoObj.questionList(idsList);
                    if (queObjectTemp.Count > 0)
                    {
                        QueQuestion queObje = queObjectTemp[0];
                        quesStmt.Text = queObje.questionStatement;
                        fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
                        DropDownList5.SelectedValue = queObje.courseTopicId.ToString();
                        fillCLODropDown(Session["CourseID"].ToString());
                        DropDownList3.SelectedValue = queObje.cloId.ToString();
                        fillSODropDown(DropDownList3.SelectedItem.Value);
                        DropDownList4.SelectedValue = queObje.soId.ToString();
                    }

                    List<QueAttributeScore> attScoreList = queDaoObj.getQuestionScoreListByID(inputId);
                    foreach (QueAttributeScore attSocre in attScoreList)
                    {
                        if (attSocre.attributeID == 1301) // CLO
                        {
                            TextBox1.SelectedValue = attSocre.scoreValue.ToString();
                        }
                        else if (attSocre.attributeID == 1302) // SO
                        {
                            TextBox2.SelectedValue = attSocre.scoreValue.ToString();
                        }
                        else if (attSocre.attributeID == 1303) // Course Topic
                        {
                            scoreTxtBx1.SelectedValue = attSocre.scoreValue.ToString();
                        }
                    }

                    DropDownList5.Enabled = !viewMode;
                    DropDownList3.Enabled = !viewMode;
                    DropDownList4.Enabled = !viewMode;
                    scoreTxtBx1.Enabled = !viewMode;
                    Button1.Visible = !viewMode;
                    DataList12.Enabled = !viewMode;
                    TextBox1.Enabled = !viewMode;
                    TextBox2.Enabled = !viewMode;
                    quesStmt.Enabled = !viewMode;
                    cancelBtn.Text = viewMode ? "Close" : "Cancel";
                    Button1.Text = editMode ? "Update" : "Submit";
                }
                else
                {
                    //FirstGridViewRow();
                    this.Title = "Add a Question";
                }
            }

            String courseIdLocal = Session["CourseID"].ToString();
            List<KMSABET.MyPocos.QueAttribute> attributeList = queDaoObj.getAttrbuteList(courseIdLocal);
            DataList12.DataSource = attributeList;
            DataList12.DataBind();

            //MyUtilities.LogUtils.myLog.Info("Selected drop down value is: " + DropDownList1.SelectedValue);
            
        }

        public void deleteQuestion(int questionID)
        {
            DBUtils dbUtilsObj = new DBUtils();

            String deleteQuery = "DELETE FROM Q_Favorite_List_has_Q_Question"
                            + " WHERE Q_Question_question_id = " + questionID;
            dbUtilsObj.CUDOperations(deleteQuery);

            deleteQuery = "DELETE FROM Q_QUESTION_SCORE"
                            + " WHERE QUESTION_ID = " + questionID;
            dbUtilsObj.CUDOperations(deleteQuery);
            
            deleteQuery = "delete from Q_Question_Attribute_Option"
                            + " WHERE Q_Question_question_id = " + questionID;
            dbUtilsObj.CUDOperations(deleteQuery);
            
            deleteQuery
                = "delete from Q_Question"
                    + " where question_id = " + questionID;
            dbUtilsObj.CUDOperations(deleteQuery);

            Response.Redirect("~/KMSPages/QueQuestionList.aspx");
        }

        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCLODropDown(Session["CourseID"].ToString());
            fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
            fillSODropDown(DropDownList3.SelectedItem.Value);
        }

        protected void ddlClo_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSODropDown(DropDownList3.SelectedItem.Value);
            fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCLODropDown(Session["CourseID"].ToString());
            fillCourseTopicDropDownByCloId(DropDownList3.SelectedItem.Value);
            fillSODropDown(DropDownList3.SelectedItem.Value);
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int questionID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Get questionID here        
                int attributeID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "attributeID"));
                int AttributeTypeID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "attributeTypeID"));
                //pass Question ID to your DB and get all available options for the question       
                //Bind the RadiobUttonList here  
                RadioButtonList scoreTxtBoxElm = (RadioButtonList)e.Item.FindControl("scoreTxtBx");
                HiddenField attributeIDText = (HiddenField)e.Item.FindControl("attributeID");
                fillRelevanceScore(scoreTxtBoxElm, false);
                    
                //if (AttributeTypeID == 1001)
                {
                    RadioButtonList RadioButtonList1 = (RadioButtonList)e.Item.FindControl("radlstPubs");

                    List<KMSABET.MyPocos.QueAttributeOption> optionsList = new List<KMSABET.MyPocos.QueAttributeOption>();

                    Boolean isRelevanceApplicable = false;
                    DBUtils dbUtils = new DBUtils();
                    String query = "SELECT * FROM Q_Attribute_Options QAO"
                        + " JOIN Q_Attribute ON attribute_id = Q_Attribute_attribute_id"
                        + " WHERE Q_Attribute_attribute_id = " + attributeID.ToString()
                        + " ORDER BY QAO.PRIORITY_OPTION";
                    SqlDataReader attributeListReader = dbUtils.readOperation(query);
                    while (attributeListReader.Read())
                    {
                        KMSABET.MyPocos.QueAttributeOption optionTemp = new KMSABET.MyPocos.QueAttributeOption();

                        isRelevanceApplicable = (Boolean)attributeListReader["IS_RELEVANCE_APPLICABLE"];
                        optionTemp.attributeOptionId = (int)attributeListReader["attribute_option_id"];
                        optionTemp.optionStatement = attributeListReader["PRIORITY_OPTION"].ToString() 
                            + " - " + attributeListReader["option_statement"].ToString();
                        //optionTemp.attributeId = (int)attributeListReader["Q_Attribute_attribute_id"];
                        optionsList.Add(optionTemp);
                    }

                    dbUtils.closeDBConnection();
                    QueAttributeOption optionIgnoreThis = new QueAttributeOption();
                    optionIgnoreThis.attributeOptionId = 214748364 + attributeID;
                    optionIgnoreThis.optionStatement = "Ignore This";
                    optionsList.Add(optionIgnoreThis);

                    RadioButtonList1.DataSource = optionsList;
                    RadioButtonList1.DataTextField = "optionStatement";
                    RadioButtonList1.DataValueField = "attributeOptionId";
                    RadioButtonList1.DataBind();

                    attributeIDText.Value = attributeID.ToString();

                    if (isRelevanceApplicable == false)
                    {
                        Label scoreTxtBoxLabel = (Label)e.Item.FindControl("Label1");
                        scoreTxtBoxLabel.Visible = false;
                        scoreTxtBoxElm.Visible = false;
                    }

                    Boolean goneInside = false;
                    if (questionID != 0)
                    {
                        QueDao dbUtils1 = new QueDao();
                        List<QueAttributeOption> attOptions = dbUtils1.getOptionsListByAttributeIDQuestionID(attributeID, questionID);
                        foreach (QueAttributeOption attOption in attOptions)
                        {
                            scoreTxtBoxElm.SelectedValue = attOption.score.ToString();
                            RadioButtonList1.SelectedValue = attOption.attributeOptionId.ToString();
                            goneInside = true;
                        }
                    }
                    if (goneInside == false)
                    {
                        RadioButtonList1.SelectedValue = optionIgnoreThis.attributeOptionId.ToString();
                    }
                }
                /*else if (AttributeTypeID == 1002)
                {
                    CheckBoxList CheckBoxList1 = (CheckBoxList)e.Item.FindControl("chkBxList1");

                    List<KMSABET.MyPocos.QueAttributeOptions> optionsList = new List<KMSABET.MyPocos.QueAttributeOptions>();

                    DBUtils dbUtils = new DBUtils();
                    SqlDataReader attributeListReader = dbUtils.readOperation("SELECT * FROM Q_Attribute_Options"
                        + " JOIN Q_Attribute ON attribute_id = Q_Attribute_attribute_id AND attribute_type_id = 1002"
                        + " WHERE Q_Attribute_attribute_id = " + attributeID.ToString());
                    while (attributeListReader.Read())
                    {
                        KMSABET.MyPocos.QueAttributeOptions optionTemp = new KMSABET.MyPocos.QueAttributeOptions();

                        optionTemp.attributeOptionId = (int)attributeListReader["attribute_option_id"];
                        optionTemp.optionStatement = attributeListReader["option_statement"].ToString();
                        optionTemp.attributeId = (int)attributeListReader["Q_Attribute_attribute_id"];
                        optionsList.Add(optionTemp);
                    }
                    dbUtils.closeDBConnection();

                    CheckBoxList1.DataSource = optionsList;
                    CheckBoxList1.DataTextField = "optionStatement";
                    CheckBoxList1.DataValueField = "attributeOptionId";
                    CheckBoxList1.DataBind();

                    if (questionID != 0)
                    {
                        QueDao dbUtils1 = new QueDao();
                        List<QueAttributeOptions> attOptions = dbUtils1.getOptionsListByAttributeIDQuestionID(attributeID, questionID);
                        TextBox scoreTxtBoxElm = (TextBox)e.Item.FindControl("scoreTxtBx");
                        foreach (QueAttributeOptions attOption in attOptions)
                        {
                            scoreTxtBoxElm.Text = attOption.scoreValue.ToString();
                            CheckBoxList1.Items.FindByValue(attOption.attributeOptionId.ToString()).Selected = true;
                        }
                    }
                }*/

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

                    att2.Value = SO.id.ToString();
                    att2.Text = SO.statement.ToString();

                    DropDownList4.Items.Add(att2);
                }
                DropDownList4.SelectedIndex = 0;
            }
            
        }

        protected void Submit_Button_Click1(object sender, EventArgs e)
        {
            int questionID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
            if (questionID != 0)
            {
                String updateQuery = "UPDATE Q_Question"
                        + " SET App_Instructor_instructor_id = 1"
                        + " , App_Course_course_id = " + Session["CourseID"].ToString()
                        + " , App_Course_Topic_id = " + DropDownList5.SelectedValue
                        + " , App_Clo_id = " + DropDownList3.SelectedValue
                        + " , App_So_id = " + DropDownList4.SelectedValue
                        + " , question_statement = '" + quesStmt.Text 
                        + "' ,question_type = 1"
                        + " WHERE question_id = " + questionID;
                DBUtils dbUtilsObj = new DBUtils();
                dbUtilsObj.CUDOperations(updateQuery);

                String deleteQuery = "DELETE FROM Q_Question_Attribute_Option WHERE Q_Question_question_id = " + questionID;
                dbUtilsObj.CUDOperations(deleteQuery);

                deleteQuery = "DELETE FROM Q_QUESTION_SCORE WHERE QUESTION_ID = " + questionID;
                dbUtilsObj.CUDOperations(deleteQuery);
            }
            else
            {
                String insertionQuery
                    = "INSERT INTO Q_Question (App_Instructor_instructor_id, App_Course_course_id,"
                        + " App_Course_Topic_id,"
                        + " App_Clo_id, App_So_id, question_statement, question_type) "
                        + " output inserted.question_id"
                        + " VALUES (1," + Session["CourseID"].ToString() + "," 
                        + DropDownList5.SelectedValue + "," + DropDownList3.SelectedValue + ","
                        + DropDownList4.SelectedValue + "," + "'" + quesStmt.Text + "',1)";
                //LogUtils.myLog.Info("My Insert Query is: " + insertionQuery);
                DBUtils dbUtilsObj = new DBUtils();
                questionID = dbUtilsObj.CUDOperationsScalar(insertionQuery);
            }
            //LogUtils.myLog.Info("Inserted ID of Question is: " + insertedID);

            insertAttribueScore(questionID, 1301, Int32.Parse(TextBox1.Text));
            insertAttribueScore(questionID, 1302, Int32.Parse(TextBox2.Text));
            insertAttribueScore(questionID, 1303, Int32.Parse(scoreTxtBx1.Text));

            foreach (DataListItem item in DataList12.Items)
            {
                HiddenField attributeIDText = (HiddenField)item.FindControl("attributeID");
                String scoreValue = ((RadioButtonList)item.FindControl("scoreTxtBx")).Text;
                String selectedItemRdBtn = ((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem.Value;
                RadioButtonList rdBtn = ((RadioButtonList)item.FindControl("radlstPubs"));
                ListItem selItem = rdBtn.SelectedItem;
                if (selItem != null)
                {
                    Int32 ignoreThisValue = Int32.Parse("214748364") + Int32.Parse(attributeIDText.Value);
                    if (selItem.Value == ignoreThisValue.ToString())
                    {
                        continue;
                    }
                }
                //LogUtils.myLog.Info("Score of the Attribute is : " + scoreValue);

                if (((RadioButtonList)item.FindControl("radlstPubs")).SelectedItem != null)
                {
                    if (scoreValue == "")
                    {
                        scoreValue = "10";
                    }
                    String insertionQuery1 
                        = "INSERT INTO Q_Question_Attribute_Option (Q_Attribute_Options_attribute_option_id"
                        + ", Q_Question_question_id, SCORE) "
                        + " output inserted.ques_att_option_id"
                        + " VALUES (" + selectedItemRdBtn + "," + questionID + "," + scoreValue + ")";
                    DBUtils dbUtilsObj1 = new DBUtils();
                    int insertedID1 = dbUtilsObj1.CUDOperationsScalar(insertionQuery1);
                    
                }

                if (((CheckBoxList)item.FindControl("chkBxList1")).SelectedItem != null)
                {
                    CheckBoxList chkBoxLst1 = ((CheckBoxList)item.FindControl("chkBxList1"));
                    foreach(ListItem itemTemp in chkBoxLst1.Items){
                        if(itemTemp.Selected == true){
                            String selectedItemChckBox = itemTemp.Value;
                            String insertionQuery1 
                                = "INSERT INTO Q_Question_Attribute_Option (Q_Attribute_Options_attribute_option_id, Q_Question_question_id) "
                                + " output inserted.ques_att_option_id"
                                + " VALUES (" + selectedItemChckBox + "," + questionID + "," + scoreValue + ")";
                            DBUtils dbUtilsObj2 = new DBUtils();
                            int insertedID2 = dbUtilsObj2.CUDOperationsScalar(insertionQuery1);
                            //LogUtils.myLog.Info("Inserted ID of Check box is: " + insertedID2);
                        }
                    }
                }
            }
            Response.Redirect("~/KMSPages/QueQuestionList.aspx");
        }

        private void insertAttribueScore(int questionId, int attributeId, int score)
        {
            String insertionQuery1
                    = "INSERT INTO [Q_QUESTION_SCORE] ([SCORE], [ATTRIBUTE_ID], [QUESTION_ID])"
                        + " VALUES ("+ score + ", " + attributeId + ", " + questionId + ")";
            DBUtils dbUtilsObj2 = new DBUtils();
            int insertedID2 = dbUtilsObj2.CUDOperations(insertionQuery1);
        }

        private void fillRelevanceScore(RadioButtonList scoreTxtBx11111, Boolean selectScore)
        {
            for (var i = 0; i <= 10; i++) scoreTxtBx11111.Items.Add(new ListItem(i.ToString(), i.ToString()));
            if(selectScore) scoreTxtBx11111.SelectedIndex = 10;
        }

    }

}