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
    public partial class QueAttributeView : System.Web.UI.Page
    {
        QueDao queDaoObj = new QueDao();

        protected override void OnPreInit(EventArgs e)
        {
            bool editMode = Request.QueryString["editMode"] == null ? false : bool.Parse(Request.QueryString["editMode"]);
            bool viewMode = Request.QueryString["viewMode"] == null ? false : bool.Parse(Request.QueryString["viewMode"]);
            Page.Title = editMode == true ? "Update an Attribute" : viewMode == true ? "View an Attribute" : "Add an Attribute";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                DBUtils dbUtils = new DBUtils();
                SqlDataReader attributeTypeReader = dbUtils.readOperation("SELECT * FROM App_CODE Where CODE_TYPE_ID=1000");
                while (attributeTypeReader.Read())
                {
                    RadioButtonList1.Items.Add(new ListItem(attributeTypeReader["CODE_VALUE"].ToString(),
                        attributeTypeReader["CODE_ID"].ToString()));
                }
                dbUtils.closeDBConnection();

                String value = ClientQueryString;
                String[] valueArr = value.Split('=','&');
                if (valueArr.Count() > 1)
                {
                    int inputId = Int32.Parse(valueArr[1]);
                    bool deleteMode = valueArr[7] == "true" ? true : false;
                    bool viewMode = valueArr[3] == "true" ? true : false;
                    bool editMode = valueArr[5] == "true" ? true : false;
                    
                    if (deleteMode) deleteAttribute(inputId);

                    ViewState["viewMode"] = viewMode;
                    ViewState["editMode"] = editMode;
                    QueAttribute queAttribute = queDaoObj.getAttrbuteByID(inputId);
                    
                    attStmt.Text = queAttribute.attributeStatement;
                    attWeight.Text = queAttribute.attributeWeight.ToString();
                    RadioButtonList1.SelectedValue = queAttribute.attributeTypeID.ToString();
                    CheckBoxRelevance.Checked = queAttribute.isRelevanceApplicable;
                    FirstGridViewRow(inputId);

                    attStmt.Enabled = !viewMode;
                    AttributeOptions.Enabled = !viewMode;
                    Button btnAdd = (Button)AttributeOptions.FooterRow.Cells[1].FindControl("ButtonAdd");
                    btnAdd.Visible = !viewMode;
                    AttributeOptions.Columns[3].Visible = !viewMode;
                    Button1.Visible = !viewMode;
                    attWeight.Enabled = !viewMode;
                    CancelBtnId.Text = viewMode ? "Close" : "Cancel";
                    cautionLbl.Visible = editMode;
                    Button1.Text = editMode ? "Update" : "Submit";
                }
                else
                {
                    FirstGridViewRow();
                    cautionLbl.Visible = false;
                }

            }
        }

        public void deleteAttribute(int attributeID)
        {
            DBUtils dbUtilsObj = new DBUtils();

            String deleteQuery
                = "delete from Q_Attribute_Options"
                    + " where Q_Attribute_attribute_id = " + attributeID;
            dbUtilsObj.CUDOperations(deleteQuery);

            String updateQuery
                        = "delete from Q_Attribute"
                            + " where attribute_id = " + attributeID;
            dbUtilsObj.CUDOperations(updateQuery);

            Response.Redirect("~/KMSPages/QueAttributeList.aspx");
        }

        private void FirstGridViewRow(int inputId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));

            List<QueAttributeOption> queAttrOptionList = queDaoObj.getOptionsListByAttributeID(inputId);
            int rowCount = 0;
            foreach (QueAttributeOption option in queAttrOptionList)
            {
                DataRow dr = null;
                dr = dt.NewRow();
                dr["RowNumber"] = rowCount + 1;
                dr["Col1"] = option.priorityOption;
                dr["Col2"] = option.optionStatement;
                dt.Rows.Add(dr);

                rowCount++;
            }

            ViewState["CurrentTable"] = dt;
            AttributeOptions.DataSource = dt;
            AttributeOptions.DataBind();

            for (int count = 0; count < rowCount; count++)
            {
                TextBox TextBoxName = (TextBox)AttributeOptions.Rows[count].Cells[2].FindControl("txtName");
                TextBoxName.Text = queAttrOptionList[count].optionStatement;
                TextBox PriorityTextBox = (TextBox)AttributeOptions.Rows[count].Cells[1].FindControl("priorityNo");
                PriorityTextBox.Text = queAttrOptionList[count].priorityOption;
            }

            TextBox txn = (TextBox)AttributeOptions.Rows[0].Cells[1].FindControl("priorityNo");
            txn.Focus();
            Button btnAdd = (Button)AttributeOptions.FooterRow.Cells[1].FindControl("ButtonAdd");
            Page.Form.DefaultFocus = btnAdd.ClientID;

        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            AttributeOptions.DataSource = dt;
            AttributeOptions.DataBind();

            TextBox txn = (TextBox)AttributeOptions.Rows[0].Cells[2].FindControl("priorityNo");
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
                        TextBox TextBoxName = (TextBox)AttributeOptions.Rows[rowIndex].Cells[2].FindControl("txtName");
                        TextBox priorityTextBoxName = (TextBox)AttributeOptions.Rows[rowIndex].Cells[2].FindControl("priorityNo"); 
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = priorityTextBoxName.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxName.Text;
                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    AttributeOptions.DataSource = dtCurrentTable;
                    AttributeOptions.DataBind();

                    TextBox txn = (TextBox)AttributeOptions.Rows[rowIndex].Cells[2].FindControl("priorityNo");
                    txn.Focus();
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
                        TextBox priorityTextBox = (TextBox)AttributeOptions.Rows[rowIndex].Cells[2].FindControl("priorityNo");
                        TextBox TextBoxName = (TextBox)AttributeOptions.Rows[rowIndex].Cells[2].FindControl("txtName");
                        // drCurrentRow["RowNumber"] = i + 1;

                        AttributeOptions.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        priorityTextBox.Text = dt.Rows[i]["Col1"].ToString();
                        TextBoxName.Text = dt.Rows[i]["Col2"].ToString();
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
                        TextBox PriorityTextBox = (TextBox)AttributeOptions.Rows[rowIndex].Cells[2].FindControl("priorityNo");
                        TextBox TextBoxName = (TextBox)AttributeOptions.Rows[rowIndex].Cells[2].FindControl("txtName");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Col1"] = PriorityTextBox.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxName.Text;
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

        protected void Button1_Click(object Source, EventArgs e)
        {
            //LogUtils.myLog.Info("OptionID : " + DataBinder.Eval(AttributeOptions.Rows[0].DataItem, "attributeID"));
            bool editMode = (bool) (ViewState["editMode"] == null ? false : ViewState["editMode"]);
            if (editMode)
            {
                int attributeID = Int32.Parse(Request.QueryString["id"]);
                String updateQuery
                    = "update Q_Attribute"
                        + " set attribute_statement = '" + attStmt.Text
                        + "', attribute_type_id = " + RadioButtonList1.SelectedValue
                        + ", WEIGHTAGE = " + attWeight.Text
                        + ", IS_RELEVANCE_APPLICABLE = " + (CheckBoxRelevance.Checked == false ? '0' : '1')
                        + " where attribute_id = " + attributeID; 
                DBUtils dbUtilsObj = new DBUtils();
                dbUtilsObj.CUDOperations(updateQuery);

                String deleteQuery
                    = "delete from Q_Attribute_Options"
                        +" where Q_Attribute_attribute_id = " + attributeID;
                int rowDeleted = dbUtilsObj.CUDOperations(deleteQuery);
                    
                btnSave_Click(attributeID);
            }
            else
            {
                String insertionQuery
                    = "INSERT INTO Q_Attribute (ATTRIBUTE_STATEMENT, ATTRIBUTE_TYPE_ID, WEIGHTAGE, COURSE_ID, IS_RELEVANCE_APPLICABLE)"
                        + " output inserted.attribute_id"
                        + " VALUES ('"
                        + attStmt.Text + "','" + RadioButtonList1.SelectedValue
                        + "','" + attWeight.Text
                        + "','" + Session["courseId"]
                        + "', '" + CheckBoxRelevance.Checked
                        + "')";
                LogUtils.myLog.Info("My Insert Query is: " + insertionQuery);
                DBUtils dbUtilsObj = new DBUtils();
                int insertedID = dbUtilsObj.CUDOperationsScalar(insertionQuery);
                LogUtils.myLog.Info("Inserted ID of Attribute is: " + insertedID);
                btnSave_Click(insertedID);
            }
            Response.Redirect("~/KMSPages/QueAttributeList.aspx");
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
                        string priorityNum = row.ItemArray[1] as string;
                        string txName = row.ItemArray[2] as string;

                        if (txName != null)
                        {
                            // Do whatever is needed with this data, 
                            // Possibily push it in database
                            // I am just printing on the page to demonstrate that it is working.
                            DBUtils dbUtilObj = new DBUtils();
                            String insertStmt = "INSERT INTO Q_Attribute_Options"
                                + " (Q_Attribute_attribute_id, option_statement, PRIORITY_OPTION)"
                                + " VALUES ("
                                + " \'" + attributeID.ToString() + "\', \'"
                                + txName + "\', \'" + priorityNum + "\')";
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
    }
}