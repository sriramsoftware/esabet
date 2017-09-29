using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace KMSABET.MyDaos
{
    public class AppDao
    {
        public List<AppProgram> getProgramList() {
            DBUtils dbUtils1 = new DBUtils();
            SqlDataReader attributeListReader1 = dbUtils1.readOperation("SELECT * FROM App_Program");
            List<AppProgram> programList = new List<AppProgram>();
            while (attributeListReader1.Read())
            {
                AppProgram att2 = new AppProgram();

                att2.programId = (int) attributeListReader1["program_id"];
                att2.programName = attributeListReader1["program_name"].ToString();
                programList.Add(att2);
            }
            dbUtils1.closeDBConnection();
            return programList;
        }

        public List<AppCourse> getCourseList(String programId)
        {
            DBUtils dbUtils1 = new DBUtils();
            SqlDataReader attributeListReader1 = dbUtils1.readOperation("SELECT * FROM App_Course where App_Program_program_id = " + programId);
            List<AppCourse> courseList = new List<AppCourse>();
            while (attributeListReader1.Read())
            {
                AppCourse att2 = new AppCourse();

                att2.courseId = (int)attributeListReader1["course_id"];
                att2.courseName = attributeListReader1["course_name"].ToString();
                courseList.Add(att2);
            }
            dbUtils1.closeDBConnection(); 
            return courseList;
        }

        public List<AppCourse> getCourseList()
        {
            DBUtils dbUtils1 = new DBUtils();
            SqlDataReader attributeListReader1 = dbUtils1.readOperation("SELECT * FROM App_Course");
            List<AppCourse> courseList = new List<AppCourse>();
            while (attributeListReader1.Read())
            {
                AppCourse att2 = new AppCourse();

                att2.courseId = (int)attributeListReader1["course_id"];
                att2.courseName = attributeListReader1["course_name"].ToString();
                String courseNumber = attributeListReader1["COURSE_NUMBER"].ToString();
                att2.courseName = att2.courseName + " (" + courseNumber + ")";
                courseList.Add(att2);
            }
            dbUtils1.closeDBConnection();
            return courseList;
        }
        
        public List<AppCourseTopic> getCourseTopicList(String courseId)
        {
            DBUtils dbUtils1 = new DBUtils();
            String myQuery = "SELECT [TOPIC_ID]"
                        + " ,[TOPIC_STATEMENT]"
                        + " ,[COURSE_ID]"
                        + " ,[LECTURE_HOURS]"
                        + " ,[LAB_HOURS]"
                        + " FROM [APP_COURSE_TOPIC]"
                        + " WHERE COURSE_ID = " + courseId;
            SqlDataReader attributeListReader1 = dbUtils1.readOperation(myQuery);
            List<AppCourseTopic> courseTopicList = new List<AppCourseTopic>();
            while (attributeListReader1 != null && attributeListReader1.Read())
            {
                AppCourseTopic att2 = new AppCourseTopic();

                att2.id = (int)attributeListReader1["TOPIC_ID"];
                att2.topic = attributeListReader1["TOPIC_STATEMENT"].ToString();
                att2.labHours = (int)attributeListReader1["LECTURE_HOURS"];
                att2.lectureHours = (int)attributeListReader1["LAB_HOURS"];
                courseTopicList.Add(att2);
            }
            dbUtils1.closeDBConnection();
            return courseTopicList;
        }

        public List<AppCourseTopic> getCourseTopicListByCloId(String cloId)
        {
            DBUtils dbUtils1 = new DBUtils();
            String myQuery = "SELECT [TOPIC_ID]"
                        + " ,[TOPIC_STATEMENT]"
                        + " ,[COURSE_ID]"
                        + " ,[LECTURE_HOURS]"
                        + " ,[LAB_HOURS]"
                        + " FROM [APP_COURSE_TOPIC] CT "
                        + " JOIN [APP_CLO_COURSE_TOPIC] CCT ON CT.TOPIC_ID = CCT.COURSE_TOPIC_ID "
                        + " WHERE CCT.CLO_ID = " + cloId;
            SqlDataReader attributeListReader1 = dbUtils1.readOperation(myQuery);
            List<AppCourseTopic> courseTopicList = new List<AppCourseTopic>();
            while (attributeListReader1 != null && attributeListReader1.Read())
            {
                AppCourseTopic att2 = new AppCourseTopic();

                att2.id = (int)attributeListReader1["TOPIC_ID"];
                att2.topic = attributeListReader1["TOPIC_STATEMENT"].ToString();
                att2.labHours = (int)attributeListReader1["LECTURE_HOURS"];
                att2.lectureHours = (int)attributeListReader1["LAB_HOURS"];
                courseTopicList.Add(att2);
            }
            dbUtils1.closeDBConnection();
            return courseTopicList;
        }
        
        public List<AppCLO> getCLOList(String courseId)
        {
            DBUtils dbUtils1 = new DBUtils();
            String queryString = "SELECT *"
                  + " FROM [I_CLO] C"
                  + " JOIN App_Course CO ON C.App_Course_course_id = CO.course_id"
                  + " JOIN App_Program AP ON AP.program_id = CO.App_Program_program_id"
                  + " WHERE App_Course_course_id = " + courseId
                  + " ORDER BY 1 DESC";
            //LogUtils.myLog.Info("My Query for CLO List is : " + queryString);
            
            SqlDataReader attributeListReader1 = dbUtils1.readOperation(queryString);
            List<AppCLO> CLOList = new List<AppCLO>();
            while (attributeListReader1.Read())
            {
                AppCLO att2 = new AppCLO();

                att2.cloId = (int)attributeListReader1["clo_id"];
                att2.cloStatement = attributeListReader1["clo_statement"].ToString();
                att2.courseId = (int)attributeListReader1["App_Course_course_id"];
                att2.courseName = attributeListReader1["course_name"].ToString();
                CLOList.Add(att2);
            }
            dbUtils1.closeDBConnection();
            return CLOList;
        }

        public List<AppSO> getSOList(String cloID)
        {
            DBUtils dbUtils1 = new DBUtils();
            String queryString = "SELECT S.[SO_ID]"
                  + " , [SO_STATEMENT]"
                  + " FROM [APP_SO] S"
                  + " JOIN [APP_CLO_SO_MAP] M ON S.SO_ID = M.SO_ID"
                  + " WHERE M.CLO_ID = " + cloID;
            //LogUtils.myLog.Info("My Query for CLO List is : " + queryString);

            SqlDataReader attributeListReader1 = dbUtils1.readOperation(queryString);
            List<AppSO> SOList = new List<AppSO>();
            while (attributeListReader1.Read())
            {
                AppSO att2 = new AppSO();

                att2.id = (int)attributeListReader1["SO_ID"];
                att2.statement = attributeListReader1["SO_STATEMENT"].ToString();
                SOList.Add(att2);
            }
            dbUtils1.closeDBConnection();
            return SOList;
        }

        public List<ListItem> getCodeListByCodeTypeId(int CodeTypeId)
        {
            List<ListItem> listItems = new List<ListItem>();

            DBUtils dbUtils1 = new DBUtils();
            SqlDataReader attributeTypeReader1 = dbUtils1.readOperation(
                "SELECT * FROM App_CODE Where CODE_TYPE_ID=" + CodeTypeId.ToString());
            while (attributeTypeReader1.Read())
            {
                listItems.Add(new ListItem(attributeTypeReader1["CODE_VALUE"].ToString(),
                    attributeTypeReader1["CODE_ID"].ToString()));
            }
            dbUtils1.closeDBConnection();

            return listItems;
        }

        public int insertInstructor(AppUser appUserObj)
        {
            DBUtils dbUtilsObj = new DBUtils();
            String updateQuery = "INSERT INTO APP_INSTRUCTOR"
                        + " (INSTRUCTOR_NAME, EMAIL"
                        + ") output inserted.instructor_id"
                        + " VALUES ('"
                        + appUserObj.fullName
                        + "' , '" + appUserObj.email
                        + "')";
            int instructorId = dbUtilsObj.CUDOperationsScalar(updateQuery);
            return instructorId;
        }

        public AppUser validateUser(String username, String password)
        {
            AppUser user = new AppUser();
            user.username = null;
            DBUtils dbUtils1 = new DBUtils();

            SqlDataReader attributeTypeReader1 = dbUtils1.readOperation(
                "SELECT * FROM Users u join app_course c on c.course_id = u.COURSE_ID Where Username='" + username + "' AND password='" + password + "'");
            while (attributeTypeReader1.Read())
            {
                user.courseId = Int32.Parse(attributeTypeReader1["COURSE_ID"].ToString());
                user.expertSystemTypeId = Int32.Parse(attributeTypeReader1["EXPERT_SYSTEM_TYPE"].ToString());
                user.instructorTypeId = Int32.Parse(attributeTypeReader1["INSRUCTOR_TYPE"].ToString());
                user.username = attributeTypeReader1["Username"].ToString();
                user.course = attributeTypeReader1["course_name"].ToString();
            }
            dbUtils1.closeDBConnection();

            return user;

        }

        public void insertUser(AppUser appUserObj)
        {
            DBUtils dbUtilsObj = new DBUtils();
            String updateQuery = "INSERT INTO USERS"
                        + " VALUES ('"
                        + appUserObj.username
                        + "' , '" + appUserObj.pwd
                        + "' , '" + appUserObj.instructorId
                        + "' , '" + appUserObj.userType
                        + "' , '" + appUserObj.module
                        + "' , '" + appUserObj.course
                        + "' , '1" 
                        + "')";
            dbUtilsObj.CUDOperations(updateQuery);
        }

        public AppEmailConfiguration getEmailConfiguration()
        {
            AppEmailConfiguration emailConfigObj = new AppEmailConfiguration();
            
            DBUtils dbUtils1 = new DBUtils();
            SqlDataReader attributeTypeReader1 = dbUtils1.readOperation(
                "SELECT * FROM APP_EMAIL_CONFIGURATION");
            while (attributeTypeReader1.Read())
            {
                emailConfigObj.smtpHost = attributeTypeReader1["SMTP_HOST"].ToString();
                emailConfigObj.fromAddress = attributeTypeReader1["FROM_EMAIL_ADDRESS"].ToString();
                emailConfigObj.fromPassword = attributeTypeReader1["FROM_PASSWORD"].ToString();
                break;
            }
            dbUtils1.closeDBConnection();
            
            return emailConfigObj;
        }

        public List<AppUser> getListOfUsers()
        {
            List<AppUser> listOfUsers = new List<AppUser>();

            SqlDataReader sdb = new MyUtilities.DBUtils().readOperation(
                "select t2.instructor_name as 'INS', "
	                + " Username,"
                    + " EMAIL as Email," 
                    + " t2.instructor_id as ID," 
                    + " ADM_IJ.CODE_VALUE as ADM_TYPE,"
                    + " ES_IJ.CODE_VALUE AS ES_TYPE," 
                    + " COUR.course_name + ' (' + COUR.COURSE_NUMBER + ')' AS COUR"
                + " from Users t1 inner join App_Instructor t2 on t1.InstructorID = t2.instructor_id"
                + " INNER JOIN APP_CODE ES_IJ ON ES_IJ.CODE_ID = EXPERT_SYSTEM_TYPE"
                + " INNER JOIN APP_CODE ADM_IJ ON ADM_IJ.CODE_ID = INSRUCTOR_TYPE"
                + " INNER JOIN App_Course COUR ON COUR.course_id = T1.COURSE_ID"
                + " ORDER BY ID DESC");
    
            while (sdb.Read())
            {
                AppUser userObj = new AppUser();
                userObj.email = sdb["Email"].ToString();
                userObj.fullName = sdb["INS"].ToString();
                userObj.username = sdb["Username"].ToString();
                userObj.module = sdb["ES_TYPE"].ToString();
                userObj.userType = sdb["ADM_TYPE"].ToString();
                userObj.course = sdb["COUR"].ToString();
                userObj.instructorId = Int32.Parse(sdb["ID"].ToString());
                listOfUsers.Add(userObj);
            }

            return listOfUsers;
        }

        public void updatedPassword(AppUser userObj)
        {
            DBUtils dbUtilsObj = new DBUtils();
            String updateQuery = "UPDATE USERS"
                        + " SET PASSWORD = '" + userObj.pwd
                        + "', FIRST_TIME_LOGIN_FLAG = 0"
                        + " WHERE USERNAME = '" + userObj.username
                        + "'";
            dbUtilsObj.CUDOperations(updateQuery);
        }
    }
}