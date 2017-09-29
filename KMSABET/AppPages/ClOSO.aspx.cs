using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    public partial class ClOSO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                new Connections().GetProgram(programs);
                new Students().SelectCourse(Course, programs);
                new Students().GetAcadmicYear(programs, Course, Year);
                new Students().GetSamester(Sem, Year, Course);
                GetSOs(SO, programs);
                GETCLOs(CLO, Course);
            }


            try
            {

                Table tb = new Table();

                TableRow tbr = new TableRow();

                TableCell tbc1 = new TableCell();

                tbc1.Width = 50;
                tbc1.Text = "CLOSO ID";
                tbc1.BackColor = Color.Black;
                tbc1.ForeColor = Color.White;

                TableCell tbc2 = new TableCell();

                tbc2.Width = 20;
                tbc2.Text = "a";
                tbc2.BackColor = Color.Black;
                tbc2.ForeColor = Color.White;

                TableCell tbc3 = new TableCell();

                tbc3.Width = 20;
                tbc3.Text = "b";
                tbc3.BackColor = Color.Black;
                tbc3.ForeColor = Color.White;

                TableCell tbc4 = new TableCell();

                tbc4.Width = 20;
                tbc4.Text = "c";
                tbc4.BackColor = Color.Black;
                tbc4.ForeColor = Color.White;

                TableCell tbc5 = new TableCell();

                tbc5.Width = 20;
                tbc5.Text = "d";
                tbc5.BackColor = Color.Black;
                tbc5.ForeColor = Color.White;

                TableCell tbc6 = new TableCell();

                tbc6.Width = 20;
                tbc6.Text = "e";
                tbc6.BackColor = Color.Black;
                tbc6.ForeColor = Color.White;

                TableCell tbc7 = new TableCell();

                tbc7.Width = 20;
                tbc7.Text = "f";
                tbc7.BackColor = Color.Black;
                tbc7.ForeColor = Color.White;

                TableCell tbc8 = new TableCell();

                tbc8.Width = 20;
                tbc8.Text = "g";
                tbc8.BackColor = Color.Black;
                tbc8.ForeColor = Color.White;

                TableCell tbc9 = new TableCell();

                tbc9.Width = 20;
                tbc9.Text = "h";
                tbc9.BackColor = Color.Black;
                tbc9.ForeColor = Color.White;

                TableCell tbc10 = new TableCell();

                tbc10.Width = 20;
                tbc10.Text = "i";
                tbc10.BackColor = Color.Black;
                tbc10.ForeColor = Color.White;

                TableCell tbc11 = new TableCell();

                tbc11.Width = 20;
                tbc11.Text = "j";
                tbc11.BackColor = Color.Black;
                tbc11.ForeColor = Color.White;

                TableCell tbc12 = new TableCell();

                tbc12.Width = 20;
                tbc12.Text = "k";
                tbc12.BackColor = Color.Black;
                tbc12.ForeColor = Color.White;



                tbr.Cells.Add(tbc1);
                tbr.Cells.Add(tbc2);
                tbr.Cells.Add(tbc3);
                tbr.Cells.Add(tbc4);
                tbr.Cells.Add(tbc5);
                tbr.Cells.Add(tbc6);
                tbr.Cells.Add(tbc7);
                tbr.Cells.Add(tbc8);
                tbr.Cells.Add(tbc9);
                tbr.Cells.Add(tbc10);
                tbr.Cells.Add(tbc11);
                tbr.Cells.Add(tbc12);

                tb.Rows.Add(tbr);

                //dynamic Code Start Hear FOR CLOSO


                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader sdr = db.readOperation("Select distinct CLO_ID from APP_CLO_SO_MAP");

                while (sdr.Read())
                {
                    TableRow tbr1 = new TableRow();
                    // Get CLO ID
                    string CLOID = sdr["CLO_ID"].ToString();

                    TableCell tc1 = new TableCell();
                    tc1.Width = 20;
                    tc1.Text = CLOID.ToString();



                    // Get SO_ID
                    MyUtilities.DBUtils db1 = new MyUtilities.DBUtils();
                    SqlDataReader sdr1 = db1.readOperation("select * from APP_CLO_SO_MAP where CLO_ID = " + Int32.Parse(CLOID) + " order by SO_ID ASC");

                    TableCell[] tcarr = new TableCell[12];


                    int a = 1;
                    while (sdr1.Read())
                    {
                        //  Response.Write(sdr1["SO_ID"]);

                        string SO_ID = sdr1["SEQUENCE_ID"].ToString();


                        TableCell tbcs = new TableCell();
                        tbcs.Width = 20;

                        tbcs.Text = "1";

                        tcarr[Int32.Parse(SO_ID)] = tbcs;

                        a++;

                    }


                    tcarr[0] = tc1;

                    for (int i = 1; i < 12; i++)
                    {
                        if (tcarr[i] == null)
                        {
                            TableCell tbcs = new TableCell();
                            tbcs.Width = 20;

                            tbcs.Text = "0";

                            tcarr[i] = tbcs;
                        }
                    }

                    tbr1.Cells.AddRange(tcarr);
                    tb.Rows.Add(tbr1);

                }

                div.Controls.Add(tb);


                MyUtilities.DBUtils dbs = new MyUtilities.DBUtils();
                SqlDataReader sdrs = dbs.readOperation("select max(CLOSO_ID) as CLOSO_ID from APP_CLO_SO_MAP");

                while (sdrs.Read())
                {
                    string closoid = sdrs["CLOSO_ID"].ToString();

                    CLOSOID.Text = (Int32.Parse(closoid) + 1).ToString();

                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While CLOSO MAPING", ex);
            }


        }

        protected void programs_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().SelectCourse(Course, programs);
            new Students().GetAcadmicYear(programs, Course, Year);
            new Students().GetSamester(Sem, Year, Course);
            GetSOs(SO, programs);
            GETCLOs(CLO, Course);
        }

        protected void SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                int res = new Connections().InsertData("insert into APP_CLO_SO_MAP (CLO_ID, SO_ID,COURSE_ENR_ID) values (" + CLO.SelectedValue + "," + SO.SelectedValue + "," + new Students().GetCoureEnrollID(Sem, Course, Year) + ");");
                
                if (res == 1)
                {
                    Response.Redirect("~/AppPages/UniversityPortalMenu.aspx");
                }
                else
                {
                    Response.Write("Error Occur");
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error : ", ex);
            }
        }

        protected void course_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetAcadmicYear(programs, Course, Year);
            new Students().GetSamester(Sem, Year, Course);
            GETCLOs(CLO, Course);
        }

        protected void Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            new Students().GetSamester(Sem, Year, Course);
            
        }

        protected void Sem_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        public void GetSOs(DropDownList SO, DropDownList Program)
        {
            try
            {
                SO.Items.Clear();
                string Query = "select SO_STATEMENT as ST, SO_ID as ID from APP_SO where APP_PROGRAM_ID = (select program_id from App_Program where App_Program.program_name = '" + Program.SelectedValue + "');";
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(Query);

                while (sdb.Read())
                {
                    SO.Items.Add(new ListItem() { Text = sdb["ST"].ToString(), Value = sdb["ID"].ToString() });
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While GET SO STATEMENT", ex);
            }
        }

        public void GETCLOs(DropDownList CLO, DropDownList Course)
        {
            try
            {
                CLO.Items.Clear();
                string Query = "select clo_statement As ST, clo_id as ID from I_CLO where App_Course_course_id = (select course_id from App_Course where course_name = '" + Course.SelectedValue + "');";

                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(Query);

                while (sdb.Read())
                {
                    CLO.Items.Add(new ListItem() { Text = sdb["ST"].ToString(), Value = sdb["ID"].ToString() });   
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While GETING CLO", ex);
            }
        }


    }
}