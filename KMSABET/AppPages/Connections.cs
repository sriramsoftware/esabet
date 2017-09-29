using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace KMSABET.AppPages
{
    class Connections
    {
        public static List<string> LIST = new List<string>();
        public static bool isUpdateInstructor = false;

        public static int LandFirstTimeVal = 0;

        public SqlConnection SQLCON()
        {
            try
            {
                SqlConnection con = new SqlConnection(MyConstants.DBConnectionString);
                return con;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int InsertData(string query)
        {
            try
            {
                SqlConnection con = SQLCON();

                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();

                con.Close();
                return 1;
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error while inserting data :", ex);
                return 0;
            }

        }

        public int DeleteDate(string query)
        {
            try
            {
                SqlConnection con = SQLCON();

                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();

                con.Close();

                return 1;
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While deleting Data :", ex);
                return 0;
            }

        }

        public void GetSemester(DropDownList dropSemester)
        {
            try
            {
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select CODE_VALUE as SM from App_CODE_TYPE t1 inner join App_CODE t2 on t1.CODE_TYPE_ID = t2.CODE_TYPE_ID where t1.CODE_TYPE_ID = 1400");

                while (sdb.Read())
                {
                    dropSemester.Items.Add(new ListItem() { Text = sdb["SM"].ToString(), Value = sdb["SM"].ToString() });
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public List<string> GetAssessmentandRawScore(DropDownList Assessment, TextBox t1, DropDownList Semester, DropDownList Year, DropDownList Course)
        {
            try
            {
                List<string> li = new List<string>();

                Assessment.Items.Clear();
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select t2.CODE_VALUE as ASS, t1.SCORE_VALUE as SV from APP_SCORE_DISTRIBUTION t1 inner join App_CODE t2 on t1.ASSESSMENT_ID = t2.CODE_ID where COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semester, Course, Year) + "");

                while (sdb.Read())
                {
                    li.Add(sdb["SV"].ToString());
                    Assessment.Items.Add(sdb["ASS"].ToString());
                }

                if (li.Count != 0)
                {
                    t1.Text = li[Assessment.SelectedIndex];
                    Assessment.Enabled = true;
                }
                else
                {
                    Assessment.Enabled = false;
                    t1.Text = "";
                }

                return li;

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Get Assessment", ex);
                return null;
            }
        }

        public void GetAssessemntsValues(DropDownList semester, TextBox Score, DropDownList course, DropDownList year, DropDownList Assessment, DropDownList AssessmentType)
        {
            Assessment.Items.Clear();
            MyUtilities.DBUtils db = new MyUtilities.DBUtils();
            SqlDataReader sdb = db.readOperation(@"select t1.ASSESSMENT_NAME as AN, t1.RAW_SCORE as RS from APP_SCORE_DESIGN t1 inner join APP_SCORE_DISTRIBUTION t2 on t1.SCORE_DISTRIBUTION_ID = t2.SCORE_DISTRIBUTION_ID where t1.SCORE_DISTRIBUTION_ID = (select t1.SCORE_DISTRIBUTION_ID from APP_SCORE_DISTRIBUTION t1 where t1.ASSESSMENT_ID = (select CODE_ID from App_CODE where CODE_VALUE = '" + AssessmentType.SelectedValue + "') and t1.COURSE_ENR_ID = " + new Students().GetCoureEnrollID(semester, course, year) + " )");
            while (sdb.Read())
            {
                ListItem li = new ListItem();
                li.Text = sdb["AN"].ToString();
                li.Value = sdb["AN"].ToString();

                Assessment.Items.Add(li);
                Score.Text = sdb["RS"].ToString();
            }
        }

        public void GetProgram(DropDownList dp)
        {
            try
            {
                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader sdb = db.readOperation(@"select program_name as PN from App_Program");

                while (sdb.Read())
                {
                    ListItem li = new ListItem();
                    li.Text = sdb["PN"].ToString();
                    li.Value = sdb["PN"].ToString();

                    dp.Items.Add(li);
                }

            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public string GetCourseID(DropDownList course)
        {
            try
            {
                string a = "select course_id as CID from App_Course where course_name = '" + course.SelectedValue + "';";

                MyUtilities.DBUtils db = new MyUtilities.DBUtils();
                SqlDataReader sdb = db.readOperation(a);

                while (sdb.Read())
                {
                    return sdb["CID"].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error While Get Course Enrollment", ex);
                return null;
            }
        }


        public void GetStudent(DropDownList Student, string Instructor)
        {
            try
            {
                Student.Items.Clear();
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select t1.STUDENT_NAME as Name, STUDENT_ID as Id from APP_STUDENT t1 inner join APP_COURSE_ENROLMENT t2 on t1.COURSE_ENR_ID = t2.COURSE_ENROL_ID inner join App_Instructor t3 on t2.INSTRUCTOR_ID = t3.instructor_id where t2.INSTRUCTOR_ID = '" + Instructor + "'");

                while (sdb.Read())
                {
                    Student.Items.Add(new ListItem() { Text = sdb["Name"].ToString(), Value = sdb["Id"].ToString() });
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

        public void GetStudent(DropDownList Student, string Instructor, DropDownList Semester, DropDownList Course, DropDownList Year )
        {
            try
            {
                Student.Items.Clear();
                SqlDataReader sdb = new MyUtilities.DBUtils().readOperation("select t1.STUDENT_NAME as Name from APP_STUDENT t1 inner join APP_COURSE_ENROLMENT t2 on t1.COURSE_ENR_ID = t2.COURSE_ENROL_ID inner join App_Instructor t3 on t2.INSTRUCTOR_ID = t3.instructor_id where t2.INSTRUCTOR_ID = '" + Instructor + "' and t1.COURSE_ENR_ID = " + new Students().GetCoureEnrollID(Semester, Course, Year) + "");

                while (sdb.Read())
                {
                    Student.Items.Add(new ListItem() { Text = sdb["Name"].ToString(), Value = sdb["Name"].ToString() });
                }
            }
            catch (Exception ex)
            {
                MyUtilities.LogUtils.myLog.Error("Error", ex);
            }
        }

    }

    public class Course_Information
    {
        public string COURSE_ENROL_ID { get; set; }
        public string APP_COURSE_ID { get; set; }
        public string ACDEMIC_YEAR { get; set; }
        public string SEMESTER { get; set; }
        public string SECTION_NUMBERS { get; set; }
        public string INSTRUCTOR_ID { get; set; }

    }

    public class App_Instructor
    {
        public string instructor_id { get; set; }
        public string instructor_name { get; set; }
        public string Full_Name { get; set; }        
        public string EMAIL { get; set; }
        public string CELL_PHONE_NUM { get; set; }        
        public string UNI_ID { get; set; }

    }

    public class App_Student
    {
        public string StudentID { get; set; }
        public string StudentRoll { get; set; }
        public string StudentName { get; set; }
        public string CourseID { get; set; }

    }

    public class APP_CourseTopic
    {
        public string TOPIC_ID { get; set; }
        public string TOPIC_STATEMENT { get; set; }
        public string Course_ID { get; set; }
        public string LECTURE_HOURS { get; set; }
        public string LAB_HOURS { get; set; }
        public string CLO { get; set; }
    }

    public class App_Course_Topic_Enroll
    {
        public string CourseTopicEnrID { get; set; }
        public string CourseTopicID { get; set; }
        public string CourseEnrolID { get; set; }
        public string TopicSeqNo { get; set; }

    }

    public class Score_Distribution
    {
        public string ID { get; set; }
        public string Assessment_ID { get; set; }
        public string Score_Value { get; set; }
        public string Course_EnrollID { get; set; }
    }

    public class ScoreDesignDataView
    {
        public string ID { get; set; }
        public string Assessment_type { get; set; }
        public string Assessment { get; set; }
        public string Raw_Score { get; set; }
        public string Total_Score { get; set; }
        public string Score_Value { get; set; }        
        public string CLO_Statement { get; set; }

    }

    public class Program
    {
        public string ID { get; set; }
        public string Program_Name { get; set; }
    }

    public class Courses
    {
        public string ID { get; set; }
        public string PN { get; set; } 
        public string CN { get; set; } 
        public string CNU { get; set; } 
        public string CT { get; set; } 
        public string TCRHOURS { get; set; } 
        public string LCRHOURS { get; set; } 
        public string TCHOURS { get; set; } 
        public string LCHOURS { get; set; }
    }

    public class Universities
    {
        public string ID { get; set; }
        public string UN { get; set; }
        public string UA { get; set; }
        public string V { get; set; }
    }

    public class Scoreinput
    {
        public string ID { get; set; }
        public string Student_Name { get; set; }
        public string Assessment_Name { get; set; }
        public string Marks { get; set; }
    }

    public class insMethod
    {
        public string ID { get; set; }
        public string CID { get; set; }
        public string IID { get; set; }
        public string Clo { get; set; }
    }

    public class ICLO
    {
        public string ID { get; set; }
        public string Course { get; set; }
        public string Clo_statement { get; set; }
    }

}
