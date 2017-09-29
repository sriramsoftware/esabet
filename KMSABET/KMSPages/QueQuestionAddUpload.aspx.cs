using Excel_Data;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.KMSPages
{
    public partial class QueQuestionAddUpload : System.Web.UI.Page
    {

        OleDbConnection conn;
        string conns = MyConstants.DBConnectionString;
        SqlConnection con = new SqlConnection(MyConstants.DBConnectionString);
        public List<ExcelQuestions> QuestionList = new List<ExcelQuestions>();
        public List<Query> qus;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                GetPrograms();
            }
        }

        public void GetPrograms()
        {
            SqlDataReader sdb = new Data().readOperation("select program_id as ID, program_name as Name from App_Program", con);

            while (sdb.Read())
            {
                program.Items.Add(new ListItem() { Text = sdb["Name"].ToString(), Value = sdb["ID"].ToString() });
            }
            GetCourse();
        }

        public void GetCourse()
        {
            SqlDataReader sdb1 = new Data().readOperation("select course_id as ID, course_name as Name from App_Course where App_Program_program_id = " + program.SelectedItem.Value + "", new SqlConnection(conns));

            course.Items.Clear();
            //Response.Write("select course_id as ID, course_name as Name from App_Course where App_Program_program_id = " + program.SelectedItem.Value + "");
            while (sdb1.Read())
            {
                course.Items.Add(new ListItem() { Text = sdb1["Name"].ToString(), Value = sdb1["ID"].ToString() });
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        public void UploadFile()
        {
            try
            {
                string path = "";
                string ConStr = "";
                string ext = Path.GetExtension(FileUpload1.FileName).ToLower();

                path = Server.MapPath("~/MyFolder/" + FileUpload1.FileName);
                //string host = HttpContext.Current.Request.Url.;
                LogUtils.myLog.Info("Path of file to save : " + path);
                
                FileUpload1.SaveAs(path);

                if (FileUpload1.FileName != null)
                    Session["path"] = path;

                if (FileUpload1.FileName == null)
                    path = Session["path"].ToString();

                if (ext.Trim() == ".xls")
                {

                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (ext.Trim() == ".xlsx")
                {

                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                conn = new OleDbConnection(ConStr);

                conn.Open();
                DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);


                foreach (DataRow dr in Sheets.Rows)
                {
                    string sht = dr[2].ToString().Replace("'", "");
                    ListItem li = new ListItem();
                    li.Text = sht.Remove(sht.Length - 1);
                    li.Value = sht;

                    Sheetss.Items.Add(li);

                }

                GetData(conn, Sheetss.SelectedItem.Text);
            }
            catch (Exception ex)
            {
                LogUtils.myLog.Info("Exception occurred in UploadFile : " + ex.Message);
            }
        }

        public void GetData(OleDbConnection conns, string sheet)
        {
            string query = "SELECT * FROM [" + sheet + "$]";

            OleDbCommand cmd = new OleDbCommand(query, conns);

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();

            da.Fill(ds);

            gvExcelFile.DataSource = ds.Tables[0];

            gvExcelFile.DataBind();

            conn.Close();
            //Response.Write(gvExcelFile.Rows[0].Cells.Count + "");
            Qcol.Items.Clear();
            QCLO.Items.Clear();
            QSO.Items.Clear();
            Coursetopic.Items.Clear();

            String attributeColNum = "";
            for (int i = 0; i < gvExcelFile.Rows[0].Cells.Count; i++)
            {
                ListItem li = new ListItem();
                li.Value = i + "";
                li.Text = (i+1) + "";
                
                ListItem li1 = new ListItem();
                li1.Value = i + "";
                li1.Text = (i+1) + "";

                ListItem li2 = new ListItem();
                li2.Value = i + "";
                li2.Text = (i+1) + "";

                ListItem li3 = new ListItem();
                li3.Value = i + "";
                li3.Text = (i+1) + "";

                Qcol.Items.Add(li);
                Coursetopic.Items.Add(li1);
                QCLO.Items.Add(li2);
                QSO.Items.Add(li3);

                if (i > 3)
                    attributeColNum += (i + 1) + ",";
            }
            Qcol.Items.FindByValue("0").Selected = true;
            Coursetopic.Items.FindByValue("1").Selected = true;
            QCLO.Items.FindByValue("2").Selected = true;
            QSO.Items.FindByValue("3").Selected = true;

            if (attributeColNum.Length > 1) attributeColNum = attributeColNum.Substring(0, attributeColNum.Length - 1);
            QATT.Text = attributeColNum;
        }

        public void CreateList()
        {
            string[] ListNo = QATT.Text.Split(',');

            for (int rows = 0; rows < gvExcelFile.Rows.Count; rows++)
            {
                ExcelQuestions q = new ExcelQuestions();
                for (int col = 0; col < gvExcelFile.Rows[rows].Cells.Count; col++)
                {
                    string value = gvExcelFile.Rows[rows].Cells[col].Text.ToString();

                    if (col == Convert.ToInt32(Qcol.SelectedItem.Value))
                    {
                        q.Question = value;
                    }
                    else if (col == Convert.ToInt32(QCLO.SelectedItem.Value))
                    {
                        q.CLO = value;
                    }
                    else if (col == Convert.ToInt32(QSO.SelectedItem.Value))
                    {
                        q.SO = value;
                    }
                    else if (col == Convert.ToInt32(Coursetopic.SelectedItem.Value))
                    {
                        q.CoursetopicID = value;
                    }
                    else
                    {
                        for (int i = 0; i < ListNo.Length; i++)
                        {
                            if (col == (Convert.ToInt32(ListNo[i])-1))
                            {
                                q.Attribute.Add(value);
                            }
                        }
                    }

                    q.ProgramID = program.SelectedItem.Value;
                    q.CourseID = course.SelectedItem.Value;

                }
                QuestionList.Add(q);
            }
        }

        protected void b_Click(object sender, EventArgs e)
        {
            CreateList();
            //Views();
            CreateQuery();
            Response.Redirect("~/KMSPages/QueQuestionList.aspx");
        }

        private void CreateQuery()
        {
            qus = new List<Query>();

            for (int i = 0; i < QuestionList.Count; i++)
            {
                Query q = new Query();
                //Declare @a int; Declare @b int;
                //insert into Q_Question (App_Instructor_instructor_id,App_Course_course_id,App_Program_program_id,question_statement,question_type,App_Course_Topic_id,App_Clo_id,App_So_id) values (1,4,3,'Why are the pipes connected in parallel?',1,1016,7,13);   
                //select @a= MAX(question_id) from Q_Question  select @b = attribute_option_id from Q_Attribute_Options where option_statement = 'SA';
                //insert into Q_Question_Attribute_Option (Q_Attribute_Options_attribute_option_id,Q_Question_question_id,SCORE) values (@b,@a,1);
                //select @b = attribute_option_id from Q_Attribute_Options where option_statement = '400';
                //insert into Q_Question_Attribute_Option (Q_Attribute_Options_attribute_option_id,Q_Question_question_id,SCORE) values (@b,@a,1);
                //insert into Q_Question_Attribute_Option (Q_Attribute_Options_attribute_option_id,Q_Question_question_id,SCORE) values (4087,@a,1);
                //select @b = attribute_option_id from Q_Attribute_Options where option_statement = 'No';
                //insert into Q_Question_Attribute_Option (Q_Attribute_Options_attribute_option_id,Q_Question_question_id,SCORE) values (@b,@a,1);

                q.query = @"Declare @a int; Declare @b int;" +
                    "insert into Q_Question (App_Instructor_instructor_id,App_Course_course_id,App_Program_program_id,question_statement,question_type,App_Course_Topic_id,App_Clo_id,App_So_id) values (1," + QuestionList[i].CourseID + "," + QuestionList[i].ProgramID + ",'" + QuestionList[i].Question + "',1," + QuestionList[i].CoursetopicID + "," + QuestionList[i].CLO + "," + QuestionList[i].SO + ");" +
                    "select @a= MAX(question_id) from Q_Question;"+
                    "INSERT INTO Q_QUESTION_SCORE (SCORE, ATTRIBUTE_ID, QUESTION_ID) values (3,1301,@a)"+
                    "INSERT INTO Q_QUESTION_SCORE (SCORE, ATTRIBUTE_ID, QUESTION_ID) values (4,1302,@a)"+
                    "INSERT INTO Q_QUESTION_SCORE (SCORE, ATTRIBUTE_ID, QUESTION_ID) values (5,1303,@a)";

                for (int j = 0; j < QuestionList[i].Attribute.Count; j++)
                {
                    q.query += "insert into Q_Question_Attribute_Option (Q_Attribute_Options_attribute_option_id,Q_Question_question_id,SCORE) values ('" + QuestionList[i].Attribute[j] + "',@a,1);";
                }

                //Response.Write(q.query);

                qus.Add(q);

                DBUtils dbUtils = new DBUtils();
                dbUtils.CUDOperations(q.query);
            }





        }

        public void Views()
        {
            for (int i = 0; i < QuestionList.Count; i++)
            {
                Response.Write(QuestionList[i].Question + " , ");
                Response.Write(QuestionList[i].CLO + " , ");
                Response.Write(QuestionList[i].ProgramID + " , ");
                Response.Write(QuestionList[i].CourseID + " , ");
                Response.Write(QuestionList[i].CoursetopicID + " , ");
                Response.Write(QuestionList[i].SO + " ! ");

                for (int j = 0; j < QuestionList[i].Attribute.Count; j++)
                {
                    Response.Write(QuestionList[i].Attribute[j] + " , ");
                }

            }

        }

        protected void Sheetss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string path = Session["path"].ToString();
                string ConStr = "";
                string ext = Path.GetExtension(FileUpload1.FileName).ToLower();


                FileUpload1.SaveAs(path);


                if (ext.Trim() == ".xls")
                {

                    ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (ext.Trim() == ".xlsx")
                {

                    ConStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                conn = new OleDbConnection(ConStr);

                conn.Open();
                DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);


                foreach (DataRow dr in Sheets.Rows)
                {
                    string sht = dr[2].ToString().Replace("'", "");
                    ListItem li = new ListItem();
                    li.Text = sht.Remove(sht.Length - 1);
                    li.Value = sht;

                    Sheetss.Items.Add(li);

                }

                GetData(conn, Sheetss.SelectedItem.Text);
            }
            catch (Exception ex)
            {
                LogUtils.myLog.Info("Exception occurred in Sheetss_SelectedIndexChanged : " + ex.Message);
            }
        }

        protected void program_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCourse();
        }

    }
}