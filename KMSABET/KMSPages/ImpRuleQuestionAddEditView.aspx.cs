using KMSABET.MyDaos;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class ImpRuleQuestionAddEditView : System.Web.UI.Page
    {
        ImpDao impDaoObj = new ImpDao();

        protected override void OnPreInit(EventArgs e)
        {
            bool editMode = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
            bool viewMode = Request.QueryString["viewMode"] == null ? false : bool.Parse(Request.QueryString["viewMode"]);
            Page.Title = editMode == true ? "Update a Rule Question" : viewMode == true ? "View a Rule Question" : "Add a Rule Question";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                initDbQuery();
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
                fillCLODropDown(DropDownList2.SelectedValue);

                //LogUtils.myLog.Info("My Page of Attribute Add has loaded");
                DBUtils dbUtils = new DBUtils();
                SqlDataReader attributeTypeReader = dbUtils.readOperation("SELECT * FROM App_CODE Where CODE_TYPE_ID=1100");
                while (attributeTypeReader.Read())
                {
                    RadioButtonList1.Items.Add(new ListItem(attributeTypeReader["CODE_VALUE"].ToString(),
                        attributeTypeReader["CODE_ID"].ToString()));
                }
                dbUtils.closeDBConnection();

                String value = ClientQueryString;
                String[] valueArr = value.Split('=', '&');
                if (valueArr.Count() > 1)
                {
                    int inputId = Int32.Parse(valueArr[1]);
                    bool deleteMode = valueArr[7] == "true" ? true : false;
                    bool viewMode = valueArr[3] == "true" ? true : false;
                    bool editMode = valueArr[5] == "true" ? true : false;
                    bool duplicateMode = valueArr[9] == "true" ? true : false;

                    if (deleteMode) deleteAttribute(inputId);

                    ViewState["viewMode"] = viewMode;
                    ViewState["editMode"] = editMode;
                    ImpRuleQuestion ruleQuestion = impDaoObj.getRuleQuestionByID(inputId);

                    DropDownList1.SelectedValue = ruleQuestion.cloData.programId.ToString();
                    fillCourseDropDown(DropDownList1.SelectedValue);
                    DropDownList2.SelectedValue = ruleQuestion.cloData.courseId.ToString();
                    fillCLODropDown(DropDownList2.SelectedValue);
                    DropDownList3.SelectedValue = ruleQuestion.cloData.cloId.ToString();

                    RadioButtonList1.SelectedValue = ruleQuestion.ruleQuesTypeID.ToString();
                    if (RadioButtonList1.SelectedValue == "1102")
                    {
                        dbQueryQuestionGenerator.Visible = true;
                        testQueryBtn.Visible = true;
                        ruleQuestionStatementPanelTwo.Visible = false;
                        queryOutput.Text = ruleQuestion.dBQueryStatement;
                        RadioButtonList1.Enabled = false;
                        if (duplicateMode)
                        {
                            dbQueryQuestionGenerator.Visible = false;
                            testQueryBtn.Visible = false;
                            ruleQuestionStatementPanelTwo.Visible = false;
                            queryOutput.Text = "DB Query Statement cannot be duplicated";
                            queryOutput.ForeColor = System.Drawing.Color.Red;
                            Button1.Visible = false;
                            DropDownList1.Enabled = false;
                            DropDownList2.Enabled = false;
                            DropDownList3.Enabled = false;
                            attStmt.Enabled = false;
                        }
                    }
                    else
                    {
                        dbQueryQuestionGenerator.Visible = false;
                        dbQueryQuestionGeneratorTwo.Visible = false;
                        ruleQuestionStatementPanelTwo.Visible = true;
                    }
                    attStmt.Text = ruleQuestion.ruleQuestionStatemet;
                    FirstGridViewRow(inputId);

                    if (viewMode)
                    {
                        dbQueryQuestionGenerator.Visible = false;
                        testQueryBtn.Visible = false;
                        RadioButtonList1.Enabled = false;
                        attStmt.Enabled = false;
                        AttributeOptions.Enabled = false;
                        if (AttributeOptions.FooterRow != null)
                        {
                            Button btnAdd = (Button)AttributeOptions.FooterRow.Cells[1].FindControl("ButtonAdd");
                            btnAdd.Visible = false;
                        }
                        AttributeOptions.Columns[2].Visible = false;
                        DropDownList1.Enabled = false;
                        DropDownList2.Enabled = false;
                        DropDownList3.Enabled = false;
                        Button1.Visible = false;
                    
                    }
                    
                    CancelBtnId.Text = viewMode ? "Close" : "Cancel";
                    cautionLbl.Visible = editMode;
                    Button1.Text = editMode ? "Update" : "Submit";
                }
                else
                {
                    FirstGridViewRow();
                    cautionLbl.Visible = false;
                    RadioButtonList1.SelectedIndex = 0;
                    dbQueryQuestionGenerator.Visible = false;
                    dbQueryQuestionGeneratorTwo.Visible = false;
                    ruleQuestionStatementPanelTwo.Visible = true;
                }
            }
        }

        public void deleteAttribute(int attributeID)
        {
            DBUtils dbUtilsObj = new DBUtils();

            String deleteQuery
                = "delete from I_Rule_Ques_Answer"
                            + " where I_Rule_Question_rule_question_id = " + attributeID;
            dbUtilsObj.CUDOperations(deleteQuery);

            String updateQuery
                        = "delete from I_Rule_Question"
                            + " where rule_question_id = " + attributeID;
            dbUtilsObj.CUDOperations(updateQuery);

            Response.Redirect("~/KMSPages/ImpRuleQuestionList.aspx");
        }

        private void FirstGridViewRow(int inputId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));

            List<ImpRuleQuesAnswer> queAnsOptionList = impDaoObj.getRuleAnswerList(inputId);
            int rowCount = 0;
            foreach (ImpRuleQuesAnswer option in queAnsOptionList)
            {
                DataRow dr = null;
                dr = dt.NewRow();
                dr["RowNumber"] = rowCount + 1;
                dr["Col1"] = option.answerStatemet;
                dt.Rows.Add(dr);
                ViewState["CurrentTable"] = dt;

                rowCount++;
            }

            AttributeOptions.DataSource = dt;
            AttributeOptions.DataBind();

            for (int count = 0; count < rowCount; count++)
            {
                TextBox TextBoxName = (TextBox)AttributeOptions.Rows[count].Cells[1].FindControl("txtName");
                TextBoxName.Text = queAnsOptionList[count].answerStatemet;
            }

        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            AttributeOptions.DataSource = dt;
            AttributeOptions.DataBind();

            TextBox txn = (TextBox)AttributeOptions.Rows[0].Cells[1].FindControl("txtName");
            txn.Focus();
            Button btnAdd = (Button)AttributeOptions.FooterRow.Cells[1].FindControl("ButtonAdd");
            Page.Form.DefaultFocus = btnAdd.ClientID;

        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox TextBoxName = (TextBox)AttributeOptions.Rows[rowIndex].Cells[1].FindControl("txtName");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.Text;
                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    AttributeOptions.DataSource = dtCurrentTable;
                    AttributeOptions.DataBind();

                    TextBox txn = (TextBox)AttributeOptions.Rows[rowIndex].Cells[1].FindControl("txtName");
                    txn.Focus();
                    // txn.Focus;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox TextBoxName = (TextBox)AttributeOptions.Rows[rowIndex].Cells[1].FindControl("txtName");
                        // drCurrentRow["RowNumber"] = i + 1;

                        AttributeOptions.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        TextBoxName.Text = dt.Rows[i]["Col1"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void AttributeOptions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                SetRowData();
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    int rowIndex = Convert.ToInt32(e.RowIndex);
                    if (dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();
                        ViewState["CurrentTable"] = dt;
                        AttributeOptions.DataSource = dt;
                        AttributeOptions.DataBind();

                        for (int i = 0; i < AttributeOptions.Rows.Count - 1; i++)
                        {
                            AttributeOptions.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        }
                        SetPreviousData();
                    }
                }
            }
            catch (Exception exc)
            {
                LogUtils.myLog.Info(exc, exc);
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox TextBoxName = (TextBox)AttributeOptions.Rows[rowIndex].Cells[1].FindControl("txtName");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.Text;
                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    //AttributeOptions.DataSource = dtCurrentTable;
                    //AttributeOptions.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        public void Button1_Click(object Source, EventArgs e)
        {
            //LogUtils.myLog.Info("OptionID : " + DataBinder.Eval(AttributeOptions.Rows[0].DataItem, "attributeID"));
            bool editMode = (bool)(ViewState["editMode"] == null ? false : ViewState["editMode"]);
            if (editMode)
            {
                int attributeID = Int32.Parse(Request.QueryString["id"]);
                if (RadioButtonList1.SelectedIndex > -1 && RadioButtonList1.SelectedValue == "1101")
                {
                    String updateQuery
                        = "update I_Rule_Question"
                            + " set I_CLO_clo_id = " + DropDownList3.SelectedValue
                            + " , rule_ques_statement = '" + attStmt.Text + "'"
                            + " , rule_question_type_id = " + RadioButtonList1.SelectedItem.Value.ToString()
                            + " where rule_question_id = " + attributeID;
                    
                    DBUtils dbUtilsObj = new DBUtils();
                    dbUtilsObj.CUDOperations(updateQuery);

                    String deleteQuery
                        = "delete from I_Rule_Ques_Answer"
                            + " where I_Rule_Question_rule_question_id = " + attributeID;
                    int rowDeleted = dbUtilsObj.CUDOperations(deleteQuery);

                    btnSave_Click(attributeID);
                }
                else
                {
                    String updateQuery
                        = "update I_Rule_Question"
                            + " set I_CLO_clo_id = " + DropDownList3.SelectedValue
                            + " , rule_ques_statement = '" + attStmt.Text + "'"
                            + " , rule_question_type_id = " + RadioButtonList1.SelectedItem.Value.ToString()
                            + " , DATABASE_QUERY_STATEMENT = '" + generateQuery()
                            + "' where rule_question_id = " + attributeID;

                    DBUtils dbUtilsObj = new DBUtils();
                    //LogUtils.myLog.Info("My Update Query is: " + updateQuery);
                    dbUtilsObj.CUDOperations(updateQuery);
                }
            }
            else
            {
                if (RadioButtonList1.SelectedIndex > -1 && RadioButtonList1.SelectedValue == "1101")
                {
                    //Label1.Text = "You selected: " + RadioButtonList1.SelectedItem.Text
                    //    + ", Selected Value: " + RadioButtonList1.SelectedItem.Value;
                    String insertionQuery
                        = "INSERT INTO I_Rule_Question (I_CLO_clo_id, rule_ques_statement, rule_question_type_id)"
                            + " output inserted.rule_question_id"
                            + " VALUES ('"
                            + DropDownList3.SelectedItem.Value.ToString()
                            + "','" + attStmt.Text
                            + "','" + RadioButtonList1.SelectedItem.Value.ToString()
                            + "')";
                    //LogUtils.myLog.Info("My Insert Query is: " + insertionQuery);
                    DBUtils dbUtilsObj = new DBUtils();
                    int insertedID = dbUtilsObj.CUDOperationsScalar(insertionQuery);
                    LogUtils.myLog.Info("Inserted ID of Attribute is: " + insertedID);
                    btnSave_Click(insertedID);

                }
                else
                {
                    String insertionQuery
                        = "INSERT INTO I_Rule_Question (I_CLO_clo_id, rule_ques_statement, rule_question_type_id, DATABASE_QUERY_STATEMENT)"
                            + " output inserted.rule_question_id"
                            + " VALUES ('"
                            + DropDownList3.SelectedItem.Value.ToString()
                            + "','" + attStmt.Text
                            + "','" + RadioButtonList1.SelectedItem.Value.ToString()
                            + "','" + generateQuery()
                            + "')";
                    //LogUtils.myLog.Info("My Insert Query is: " + insertionQuery);
                    DBUtils dbUtilsObj = new DBUtils();
                    int insertedID = dbUtilsObj.CUDOperationsScalar(insertionQuery);
                    //LogUtils.myLog.Info("Inserted ID of Attribute is: " + insertedID);
                }
            }
            Response.Redirect("~/KMSPages/ImpRuleQuestionList.aspx");
        }

        protected void btnSave_Click(int attributeID)
        {
            try
            {
                SetRowData();
                DataTable table = ViewState["CurrentTable"] as DataTable;

                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string txName = row.ItemArray[1] as string;

                        if (txName != null)
                        {
                            // Do whatever is needed with this data, 
                            // Possibily push it in database
                            // I am just printing on the page to demonstrate that it is working.
                            DBUtils dbUtilObj = new DBUtils();
                            String insertStmt = "INSERT INTO I_Rule_Ques_Answer"
                                + " (I_Rule_Question_rule_question_id, answer_statement)"
                                + " VALUES ("
                                + " \'" + attributeID.ToString() + "\', \'"
                                + txName + "\')";
                            dbUtilObj.CUDOperations(insertStmt);
                            //LogUtils.myLog.Info(string.Format("{0}", txName));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtils.myLog.Info("Exception : " + ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCLODropDown(DropDownList2.SelectedItem.Value);
        }
        
        protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCourseDropDown(DropDownList1.SelectedItem.Value);
            fillCLODropDown(DropDownList2.SelectedItem.Value);
        }

        protected void fillCLODropDown(String courseId)
        {
            DropDownList3.Items.Clear();
            AppDao appDaoObj = new AppDao();
            List<AppCLO> CLOList = appDaoObj.getCLOList(courseId);
            if (CLOList != null)
                foreach (AppCLO CLO in CLOList)
                {
                    ListItem att2 = new ListItem();

                    att2.Value = CLO.cloId.ToString();
                    att2.Text = CLO.cloStatement.ToString();

                    DropDownList3.Items.Add(att2);
                }
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

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "1102")
            {
                dbQueryQuestionGenerator.Visible = true;
                dbQueryQuestionGeneratorTwo.Visible = true;
                ruleQuestionStatementPanelTwo.Visible = false;
            }
            else
            {
                dbQueryQuestionGenerator.Visible = false;
                dbQueryQuestionGeneratorTwo.Visible = false;
                ruleQuestionStatementPanelTwo.Visible = true;
            }
        }

        private void initDbQuery()
        {
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            dbQueUtilsObj.fillSelectTableList(DropDownPKTb2, "B1");
            dbQueUtilsObj.changeInitOfSelectTableList(DropDownFKTb2, FKCol2,
                DropDownPKTb2, selectTablePKName2, PKCol2, DropDownFKTb2, DropDownPKTb2, DropDownSelectCol1, "A1", "B1");
            dbQueUtilsObj.fillSelectAggregateFuncList(DropDownList4);
        }

        protected void ddlProgram_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            dbQueUtilsObj.changeInitOfSelectTableList(DropDownFKTb2, FKCol2,
                DropDownPKTb2, selectTablePKName2, PKCol2, DropDownFKTb2, DropDownPKTb2, DropDownSelectCol1, "A1", "B1");
            changeOfPKFKTable(2);
        }

        protected void ddlProgram_SelectedIndexChangedPKTable(object sender, EventArgs e)
        {
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            int lastIndex = dbQueUtilsObj.getIndexOfDDList(sender);
            bool emptySelected = ((DropDownList)sender).SelectedValue == String.Empty ? true : false;
            List<DropDownList> ddPKList = new List<DropDownList>();
            ddPKList.Add(new DropDownList());
            ddPKList.Add(new DropDownList());
            ddPKList.Add(new DropDownList());
            ddPKList.Add(DropDownPKTb3);
            ddPKList.Add(DropDownPKTb4);
            ddPKList.Add(DropDownPKTb5);
            ddPKList.Add(DropDownPKTb6);
            ddPKList.Add(DropDownPKTb7);
            ddPKList.Add(DropDownPKTb8);
            ddPKList.Add(DropDownPKTb9);
            ddPKList.Add(DropDownPKTb10);

            List<DropDownList> ddFKList = new List<DropDownList>();
            ddFKList.Add(new DropDownList());
            ddFKList.Add(new DropDownList());
            ddFKList.Add(new DropDownList());
            ddFKList.Add(DropDownFKTb3);
            ddFKList.Add(DropDownFKTb4);
            ddFKList.Add(DropDownFKTb5);
            ddFKList.Add(DropDownFKTb6);
            ddFKList.Add(DropDownFKTb7);
            ddFKList.Add(DropDownFKTb8);
            ddFKList.Add(DropDownFKTb9);
            ddFKList.Add(DropDownFKTb10);

            List<Label> lblPKList = new List<Label>();
            lblPKList.Add(new Label());
            lblPKList.Add(new Label());
            lblPKList.Add(new Label());
            lblPKList.Add(PKCol3);
            lblPKList.Add(PKCol4);
            lblPKList.Add(PKCol5);
            lblPKList.Add(PKCol6);
            lblPKList.Add(PKCol7);
            lblPKList.Add(PKCol8);
            lblPKList.Add(PKCol9);
            lblPKList.Add(PKCol10);

            List<Label> lblFKList = new List<Label>();
            lblFKList.Add(new Label());
            lblFKList.Add(new Label());
            lblFKList.Add(new Label());
            lblFKList.Add(FKCol3);
            lblFKList.Add(FKCol4);
            lblFKList.Add(FKCol5);
            lblFKList.Add(FKCol6);
            lblFKList.Add(FKCol7);
            lblFKList.Add(FKCol8);
            lblFKList.Add(FKCol9);
            lblFKList.Add(FKCol10);

            dbQueUtilsObj.ddlPKChangeAction(lastIndex, ddPKList, ddFKList, lblPKList, lblFKList);

            if (emptySelected == false)
                changeOfPKFKTable(lastIndex);
        }

        protected void ddlProgram_SelectedIndexChangedFKTable(object sender, EventArgs e)
        {
            FKCol2.Text = DropDownFKTb2.SelectedItem.Value;
            DBQueDao daoObj = new DBQueDao();
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            PKCol2.Text = DropDownPKTb2.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb2.SelectedItem.Text, DropDownPKTb2.SelectedItem.Text);
            int lastIndex = dbQueUtilsObj.getIndexOfDDList(sender);

            changeOfPKFKTable(lastIndex);
            if (lastIndex == 2)
            {
                PKCol3.Text = String.Empty;
                DropDownFKTb3.Items.Clear();
                FKCol3.Text = String.Empty;
            }

            if (lastIndex == 3)
            {
                FKCol3.Text = DropDownFKTb3.SelectedValue;
                PKCol3.Text = DropDownPKTb3.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb3.SelectedItem.Text, DropDownPKTb3.SelectedItem.Text);
            }
            if (lastIndex == 4)
            {
                FKCol4.Text = DropDownFKTb4.SelectedValue;
                PKCol4.Text = DropDownPKTb4.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb4.SelectedItem.Text, DropDownPKTb4.SelectedItem.Text);
            }
            if (lastIndex == 5)
            {
                FKCol5.Text = DropDownFKTb5.SelectedValue;
                PKCol5.Text = DropDownPKTb5.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb5.SelectedItem.Text, DropDownPKTb5.SelectedItem.Text);
            }
            if (lastIndex == 6)
            {
                FKCol6.Text = DropDownFKTb6.SelectedValue;
                PKCol6.Text = DropDownPKTb6.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb6.SelectedItem.Text, DropDownPKTb6.SelectedItem.Text);
            }
            if (lastIndex == 7)
            {
                FKCol7.Text = DropDownFKTb7.SelectedValue;
                PKCol7.Text = DropDownPKTb7.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb7.SelectedItem.Text, DropDownPKTb7.SelectedItem.Text);
            }
            if (lastIndex == 8)
            {
                FKCol8.Text = DropDownFKTb8.SelectedValue;
                PKCol8.Text = DropDownPKTb8.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb8.SelectedItem.Text, DropDownPKTb8.SelectedItem.Text);
            }
            if (lastIndex == 9)
            {
                FKCol9.Text = DropDownFKTb9.SelectedValue;
                PKCol9.Text = DropDownPKTb9.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb9.SelectedItem.Text, DropDownPKTb9.SelectedItem.Text);
            }
            if (lastIndex == 10)
            {
                FKCol10.Text = DropDownFKTb10.SelectedValue;
                PKCol10.Text = DropDownPKTb10.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb10.SelectedItem.Text, DropDownPKTb10.SelectedItem.Text);
            }
            if (lastIndex == 11)
            {
                FKCol11.Text = DropDownFKTb11.SelectedValue;
                PKCol11.Text = DropDownPKTb11.SelectedItem.Value.Split(' ')[1] + "." + daoObj.getPKFKColumnValue(DropDownFKTb11.SelectedItem.Text, DropDownPKTb11.SelectedItem.Text);
            }

        }

        private bool alreadyExistsStr(String inputStr, List<ListItem> listItemsList)
        {
            bool matched = false;
            foreach (ListItem item in listItemsList)
            {
                if (item.Text == inputStr) matched = true;
            }
            return matched;
        }

        private void changeOfPKFKTable(int lastIndex)
        {
            List<ListItem> listOfTables = new List<ListItem>();
            listOfTables.Add(new ListItem(String.Empty, String.Empty));
            DBQueUtils dbQueUtilsObj = new DBQueUtils();

            if (lastIndex >= 2)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb2, DropDownPKTb2, listOfTables);
                if (lastIndex == 2)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb3, listOfTables);
                }
            }

            if (lastIndex >= 3)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb3, DropDownPKTb3, listOfTables);
                if (lastIndex == 3)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb4, listOfTables);
                }
            }

            if (lastIndex >= 4)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb4, DropDownPKTb4, listOfTables);
                if (lastIndex == 4)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb5, listOfTables);
                }
            }
            if (lastIndex >= 5)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb5, DropDownPKTb5, listOfTables);
                if (lastIndex == 5)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb6, listOfTables);
                }
            }
            if (lastIndex >= 6)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb6, DropDownPKTb6, listOfTables);
                if (lastIndex == 6)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb7, listOfTables);
                }
            }
            if (lastIndex >= 7)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb7, DropDownPKTb7, listOfTables);
                if (lastIndex == 7)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb8, listOfTables);
                }
            }
            if (lastIndex >= 8)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb8, DropDownPKTb8, listOfTables);
                if (lastIndex == 8)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb9, listOfTables);
                }
            }
            if (lastIndex >= 9)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb9, DropDownPKTb9, listOfTables);
                if (lastIndex == 9)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb10, listOfTables);
                }
            }
            if (lastIndex >= 10)
            {
                listOfTables = dbQueUtilsObj.fillPKFKTableListItem(DropDownFKTb10, DropDownPKTb10, listOfTables);
                if (lastIndex == 10)
                {
                    dbQueUtilsObj.fillPKTableList(DropDownPKTb11, listOfTables);
                }
            }

            fillTableListForWhereClause();
        }

        protected void fillTableListForWhereClause()
        {
            List<ListItem> listItems = new List<ListItem>();
            DBQueUtils dbQueUtilsObj = new DBQueUtils();

            if (DropDownPKTb2 != null && DropDownPKTb2.SelectedItem != null && DropDownPKTb2.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownPKTb2, "B1", listItems);
            }
            if (DropDownFKTb2 != null && DropDownFKTb2.SelectedItem != null && DropDownFKTb2.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb2, "A1", listItems);
            }
            if (DropDownFKTb3 != null && DropDownFKTb3.SelectedItem != null && DropDownFKTb3.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb3, "A2", listItems);
            }
            if (DropDownFKTb4 != null && DropDownFKTb4.SelectedItem != null && DropDownFKTb4.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb4, "A3", listItems);
            }
            if (DropDownFKTb5 != null && DropDownFKTb5.SelectedItem != null && DropDownFKTb5.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb5, "A4", listItems);
            }
            if (DropDownFKTb6 != null && DropDownFKTb6.SelectedItem != null && DropDownFKTb6.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb6, "A5", listItems);
            }
            if (DropDownFKTb7 != null && DropDownFKTb7.SelectedItem != null && DropDownFKTb7.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb7, "A6", listItems);
            }
            if (DropDownFKTb8 != null && DropDownFKTb8.SelectedItem != null && DropDownFKTb8.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb8, "A7", listItems);
            }
            if (DropDownFKTb9 != null && DropDownFKTb9.SelectedItem != null && DropDownFKTb9.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb9, "A8", listItems);
            }
            if (DropDownFKTb10 != null && DropDownFKTb10.SelectedItem != null && DropDownFKTb10.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb10, "A9", listItems);
            }
            if (DropDownFKTb11 != null && DropDownFKTb11.SelectedItem != null && DropDownFKTb11.SelectedItem.Value != "")
            {
                listItems = dbQueUtilsObj.fillWhereClauseDropDownTable(DropDownFKTb11, "A10", listItems);
            }

            List<DropDownList> ddWhereTableList = getListOfWhereTables();
            foreach (DropDownList ddItem in ddWhereTableList)
            {
                fillWhereTableData(listItems, ddItem);
            }
            //dbQueUtilsObj.fillPKTableList(DropDownWhereTable1, listItems);
            //if (DropDownWhereTable1.SelectedValue != "")
            //{
            //    dbQueUtilsObj.fillSelectTableColumList(DropDownWhereColumnList1, DropDownWhereTable1, DropDownWhereTable1.SelectedValue);
            //    dbQueUtilsObj.fillSelectedColumnValues(DropDownWhereTable1, DropDownWhereColumnList1, DropDownColumnValues1);
            //    if (DropDownColumnValues1 != null && DropDownColumnValues1.SelectedItem != null)
            //        SelectedColumnID1.Text = DropDownColumnValues1.SelectedItem.Value;
            //    else SelectedColumnID1.Text = "";
            //    DBQueDao dbQueDaoObj = new DBQueDao();
            //    String pkColName = dbQueDaoObj.getPKColumnByTableName(DropDownWhereTable1.SelectedItem.Text);
            //    pkColumnName1.Text = DropDownWhereTable1.SelectedValue + "." + pkColName;
            //}
        }

        protected void fillWhereTableData(List<ListItem> listItems, DropDownList DropDownWhereTable1)
        {
            DropDownWhereTable1.Items.Clear();
            DropDownWhereTable1.Items.Add(new ListItem("", ""));
            foreach (ListItem item in listItems)
            {
                DropDownWhereTable1.Items.Add(item);
            }
            DropDownWhereTable1.SelectedValue = DropDownWhereTable1.Items[0].Value;
        }

        protected List<DropDownList> getListOfWhereTables()
        {
            List<DropDownList> ddList = new List<DropDownList>();
            ddList.Add(DropDownWhereTable1);
            ddList.Add(DropDownWhereTable2);
            ddList.Add(DropDownWhereTable3);
            ddList.Add(DropDownWhereTable4);
            ddList.Add(DropDownWhereTable5);
            ddList.Add(DropDownWhereTable6);
            ddList.Add(DropDownWhereTable7);
            ddList.Add(DropDownWhereTable8);
            ddList.Add(DropDownWhereTable9);
            ddList.Add(DropDownWhereTable10);
            return ddList;
        }

        protected List<DropDownList> getListOfWhereColumns()
        {
            List<DropDownList> ddList = new List<DropDownList>();
            ddList.Add(DropDownWhereColumnList1);
            ddList.Add(DropDownWhereColumnList2);
            ddList.Add(DropDownWhereColumnList3);
            ddList.Add(DropDownWhereColumnList4);
            ddList.Add(DropDownWhereColumnList5);
            ddList.Add(DropDownWhereColumnList6);
            ddList.Add(DropDownWhereColumnList7);
            ddList.Add(DropDownWhereColumnList8);
            ddList.Add(DropDownWhereColumnList9);
            ddList.Add(DropDownWhereColumnList10);
            return ddList;
        }

        protected List<DropDownList> getListOfWhereColumnValues()
        {
            List<DropDownList> ddList = new List<DropDownList>();
            ddList.Add(DropDownColumnValues1);
            ddList.Add(DropDownColumnValues2);
            ddList.Add(DropDownColumnValues3);
            ddList.Add(DropDownColumnValues4);
            ddList.Add(DropDownColumnValues5);
            ddList.Add(DropDownColumnValues6);
            ddList.Add(DropDownColumnValues7);
            ddList.Add(DropDownColumnValues8);
            ddList.Add(DropDownColumnValues9);
            ddList.Add(DropDownColumnValues10);
            return ddList;
        }

        protected List<Label> getListOfWhereSelectedIdList()
        {
            List<Label> ddList = new List<Label>();
            ddList.Add(SelectedColumnID1);
            ddList.Add(SelectedColumnID2);
            ddList.Add(SelectedColumnID3);
            ddList.Add(SelectedColumnID4);
            ddList.Add(SelectedColumnID5);
            ddList.Add(SelectedColumnID6);
            ddList.Add(SelectedColumnID7);
            ddList.Add(SelectedColumnID8);
            ddList.Add(SelectedColumnID9);
            ddList.Add(SelectedColumnID10);
            return ddList;
        }

        protected List<Label> getListOfPkColumnList()
        {
            List<Label> ddList = new List<Label>();
            ddList.Add(pkColumnName1);
            ddList.Add(pkColumnName2);
            ddList.Add(pkColumnName3);
            ddList.Add(pkColumnName4);
            ddList.Add(pkColumnName5);
            ddList.Add(pkColumnName6);
            ddList.Add(pkColumnName7);
            ddList.Add(pkColumnName8);
            ddList.Add(pkColumnName9);
            ddList.Add(pkColumnName10);
            return ddList;
        }

        protected List<CheckBox> getListOfAskUsers()
        {
            List<CheckBox> cbList = new List<CheckBox>();
            cbList.Add(askUser1);
            cbList.Add(askUser2);
            cbList.Add(askUser3);
            cbList.Add(askUser4);
            cbList.Add(askUser5);
            cbList.Add(askUser6);
            cbList.Add(askUser7);
            cbList.Add(askUser8);
            cbList.Add(askUser9);
            cbList.Add(askUser10);
            return cbList;
        }

        protected List<TextBox> getListOfHeadingWhereTable()
        {
            List<TextBox> txtBxList = new List<TextBox>();
            txtBxList.Add(headingWhereTable1);
            txtBxList.Add(headingWhereTable2);
            txtBxList.Add(headingWhereTable3);
            txtBxList.Add(headingWhereTable4);
            txtBxList.Add(headingWhereTable5);
            txtBxList.Add(headingWhereTable6);
            txtBxList.Add(headingWhereTable7);
            txtBxList.Add(headingWhereTable8);
            txtBxList.Add(headingWhereTable9);
            txtBxList.Add(headingWhereTable10);
            return txtBxList;
        }

        protected void ddlWhere_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            int lastIndex = dbQueUtilsObj.getIndexOfDDList(sender) - 1;
            List<DropDownList> ddWhereTableList = getListOfWhereTables();
            List<DropDownList> ddWhereColumnsList = getListOfWhereColumns();
            List<DropDownList> ddWhereColumnValueList = getListOfWhereColumnValues();
            List<Label> lblSelectedColumnIdList = getListOfWhereSelectedIdList();
            List<Label> lblPKColumnList = getListOfPkColumnList();
            List<CheckBox> cbAskUserList = getListOfAskUsers();
            List<TextBox> txbBxHeadingWhereTableList = getListOfHeadingWhereTable();

            if (ddWhereTableList[lastIndex].SelectedValue != "")
            {
                dbQueUtilsObj.fillSelectTableColumList(ddWhereColumnsList[lastIndex], ddWhereTableList[lastIndex], ddWhereTableList[lastIndex].SelectedValue);
                dbQueUtilsObj.fillSelectedColumnValues(ddWhereTableList[lastIndex], ddWhereColumnsList[lastIndex], ddWhereColumnValueList[lastIndex]);
                if (ddWhereColumnValueList[lastIndex] != null && ddWhereColumnValueList[lastIndex].SelectedItem != null)
                    lblSelectedColumnIdList[lastIndex].Text = ddWhereColumnValueList[lastIndex].SelectedItem.Value;
                else lblSelectedColumnIdList[lastIndex].Text = "";
                DBQueDao dbQueDaoObj = new DBQueDao();
                String pkColName = dbQueDaoObj.getPKColumnByTableName(ddWhereTableList[lastIndex].SelectedItem.Text);
                lblPKColumnList[lastIndex].Text = ddWhereTableList[lastIndex].SelectedValue + "." + pkColName;
            }
            else
            {
                //do empty other dropdowns and fields
                ddWhereColumnsList[lastIndex].Items.Clear();
                cbAskUserList[lastIndex].Checked = false;
                ddWhereColumnValueList[lastIndex].Items.Clear();
                lblSelectedColumnIdList[lastIndex].Text = "";
                lblPKColumnList[lastIndex].Text = "";
                txbBxHeadingWhereTableList[lastIndex].Text = "";
            }
        }

        protected void ddlWhere_SelectedColumnChanged(object sender, EventArgs e)
        {
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            int lastIndex = dbQueUtilsObj.getIndexOfDDList(sender) - 1;
            List<DropDownList> ddWhereTableList = getListOfWhereTables();
            List<DropDownList> ddWhereColumnList = getListOfWhereColumns();
            List<DropDownList> ddWhereColumnValueList = getListOfWhereColumnValues();
            List<Label> lblWhereSelectedColumnList = getListOfWhereSelectedIdList();

            dbQueUtilsObj.fillSelectedColumnValues(ddWhereTableList[lastIndex], ddWhereColumnList[lastIndex], ddWhereColumnValueList[lastIndex]);
            if (ddWhereColumnValueList[lastIndex] != null && ddWhereColumnValueList[lastIndex].SelectedItem != null)
                lblWhereSelectedColumnList[lastIndex].Text = ddWhereColumnValueList[lastIndex].SelectedItem.Value;
            else lblWhereSelectedColumnList[lastIndex].Text = "";
        }

        protected void ddlWhere_SelectedColumnValueChanged(object sender, EventArgs e)
        {
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            int lastIndex = dbQueUtilsObj.getIndexOfDDList(sender) - 1;
            List<DropDownList> ddWhereColumnValueList = getListOfWhereColumnValues();
            List<Label> lblWhereSelectedColumnList = getListOfWhereSelectedIdList();

            if (ddWhereColumnValueList[lastIndex] != null && ddWhereColumnValueList[lastIndex].SelectedItem != null)
                lblWhereSelectedColumnList[lastIndex].Text = ddWhereColumnValueList[lastIndex].SelectedItem.Value;
            else lblWhereSelectedColumnList[lastIndex].Text = "";
        }

        protected void changeAskUserWhereClause(object sender, EventArgs e)
        {
            DBQueUtils dbQueUtilsObj = new DBQueUtils();
            int lastIndex = dbQueUtilsObj.getIndexOfCheckBoxList(sender) - 1;
            List<DropDownList> ddWhereTableList = getListOfWhereTables();
            List<DropDownList> ddWhereColumnValueList = getListOfWhereColumnValues();
            List<TextBox> txbBxHeadingWhereTableList = getListOfHeadingWhereTable();
            List<Label> lblWhereSelectedColumnId = getListOfWhereSelectedIdList();

            if (((CheckBox)sender).Checked)
            {
                ddWhereColumnValueList[lastIndex].Visible = false;
                lblWhereSelectedColumnId[lastIndex].Visible = false;
                txbBxHeadingWhereTableList[lastIndex].Enabled = true;
                if (txbBxHeadingWhereTableList[lastIndex].Text == "")
                    txbBxHeadingWhereTableList[lastIndex].Text = ddWhereTableList[lastIndex].SelectedItem.Text.Replace("_", " ");
            }
            else
            {
                ddWhereColumnValueList[lastIndex].Visible = true;
                lblWhereSelectedColumnId[lastIndex].Visible = true;
                txbBxHeadingWhereTableList[lastIndex].Enabled = false;
            }
        }

        protected String generateQuery()
        {
            String selectColumnName = DropDownSelectCol1.SelectedValue;
            String selectTableName = DropDownPKTb2.SelectedItem.Text;
            String selectAggregateFuncName = DropDownList4.SelectedItem.Text;
            String myQuery = "Select " + selectAggregateFuncName + "(" + selectColumnName + ") AS \'\'QUERY_RESULT\'\' From " + selectTableName + " B1";
            DBQueUtils dbQueUtilsObj = new DBQueUtils();

            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb2, FKCol2, PKCol2, myQuery, "A1");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb3, FKCol3, PKCol3, myQuery, "A2");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb4, FKCol4, PKCol4, myQuery, "A3");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb5, FKCol5, PKCol5, myQuery, "A4");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb6, FKCol6, PKCol6, myQuery, "A5");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb7, FKCol7, PKCol7, myQuery, "A6");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb8, FKCol8, PKCol8, myQuery, "A7");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb9, FKCol9, PKCol9, myQuery, "A8");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb10, FKCol10, PKCol10, myQuery, "A9");
            myQuery = dbQueUtilsObj.leftJoinQueryAppender(DropDownFKTb11, FKCol11, PKCol11, myQuery, "A10");

            List<DropDownList> ddWhereTableList = getListOfWhereTables();
            List<DropDownList> ddWhereDisplayColumnsList = getListOfWhereColumns();
            List<Label> lblPKWhereColumnList = getListOfPkColumnList();
            List<Label> lblSelectedColumnIDList = getListOfWhereSelectedIdList();
            List<CheckBox> chBxAskUserList = getListOfAskUsers();
            List<TextBox> txtBxHeadingLabel = getListOfHeadingWhereTable();

            List<String> whereClauseList = new List<String>();

            for (int i = 0; i < ddWhereTableList.Count; i++)
            {
                if (ddWhereTableList[i].SelectedItem != null && ddWhereTableList[i].SelectedItem.Value != "")
                {
                    if (chBxAskUserList[i].Checked)
                    {
                        whereClauseList.Add(lblPKWhereColumnList[i].Text
                            + " = { id: \"" + (i + 1) + "\","
                            + " tableName: \"" + ddWhereTableList[i].SelectedItem.Text + "\","
                            + " alias: \"" + ddWhereTableList[i].SelectedItem.Value + "\","
                            + " pkColumnName: \"" + lblPKWhereColumnList[i].Text.Split('.')[1] + "\","
                            + " displayColumn: \"" + ddWhereDisplayColumnsList[i].SelectedItem.Text + "\","
                            + " headingLabel: \"" + txtBxHeadingLabel[i].Text + "\" }");
                    }
                    else
                    {
                        whereClauseList.Add(lblPKWhereColumnList[i].Text + " = " + lblSelectedColumnIDList[i].Text);
                    }
                }
            }

            if (whereClauseList.Count > 0)
            {
                myQuery += " Where " + whereClauseList[0];
            }
            for (int i = 1; i < whereClauseList.Count; i++)
            {
                myQuery += " AND " + whereClauseList[i];
            }

            return myQuery;
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            queryOutput.Text = generateQuery();
        }
    }
}