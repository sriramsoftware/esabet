using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KMSABET.MyUtilities;
using System.Data.SqlClient;
using KMSABET.MyPocos;

namespace KMSABET.MyDaos
{
    public class QueDao
    {
        public List<KMSABET.MyPocos.QueAttribute> getAttrbuteList(String courseId)
        {
            List<KMSABET.MyPocos.QueAttribute> attributeList = new List<KMSABET.MyPocos.QueAttribute>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation("SELECT * FROM Q_Attribute A"
                + " JOIN App_CODE C ON C.CODE_ID = A.attribute_type_id"
                + " WHERE A.COURSE_ID = " + courseId
                + " order by 1 desc");
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueAttribute att1 = new KMSABET.MyPocos.QueAttribute();

                att1.attributeID = (int)attributeListReader["attribute_id"];
                att1.attributeStatement = attributeListReader["attribute_statement"].ToString();
                att1.attributeTypeID = (int)attributeListReader["attribute_type_id"];
                att1.AttributeType = attributeListReader["CODE_VALUE"].ToString();
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public List<QueAttributeOption> getAttributeOptionListByAttrID(String attributeID)
        {
            List<QueAttributeOption> optionsList = new List<QueAttributeOption>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation("SELECT * FROM Q_Attribute_Options"
                + " JOIN Q_Attribute ON attribute_id = Q_Attribute_attribute_id"
                + " WHERE Q_Attribute_attribute_id = " + attributeID.ToString()
                + " ORDER BY PRIORITY_OPTION");
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueAttributeOption optionTemp = new KMSABET.MyPocos.QueAttributeOption();

                optionTemp.attributeOptionId = (int)attributeListReader["attribute_option_id"];
                optionTemp.optionStatement = attributeListReader["PRIORITY_OPTION"].ToString()
                    + " - " + attributeListReader["option_statement"].ToString();
                //optionTemp.attributeId = (int)attributeListReader["Q_Attribute_attribute_id"];
                optionsList.Add(optionTemp);
            }
            return optionsList;
        }

        public List<QueQuestion> getQuesScoreListInferenceEngine(List<QuestionRangedValue> rangeValueList, List<int> selectedItemsIds, String countSelect, QueAssessmentSearchCriteria criteria)
        {
            String rangeValueStr = "";
            String rangeValueBrackets = "";
            String checkBoxStr = "";
            String checkBoxBrackets = "";
            foreach(QuestionRangedValue rangeValue in rangeValueList) 
            {
                if (rangeValue != null && !rangeValue.fromValue.Equals("") && !rangeValue.toValue.Equals(""))
                {
                    rangeValueStr += " SELECT QA1.Q_Question_question_id "
                     + " FROM Q_Question_Attribute_Option QA1"
                     + " JOIN q_attribute_options AO1 "
                     + "     ON AO1.attribute_option_id = "
                     + "     QA1.q_attribute_options_attribute_option_id "
                     + " JOIN Q_Attribute A1 ON A1.attribute_id = AO1.Q_Attribute_attribute_id "
                     + " WHERE ( A1.attribute_type_id = 1002 "
                     + " AND " + rangeValue.fromValue + " <= CASE "
                     + " WHEN Isnumeric(AO1.option_statement) = 1 THEN "
                     + " CONVERT(INT, AO1.option_statement) END "
                     + " AND " + rangeValue.toValue + " >= CASE "
                     + " WHEN Isnumeric(AO1.option_statement) = 1 THEN "
                     + " CONVERT(INT, AO1.option_statement) END ) "
                     + " AND QA1.Q_Question_question_id IN (";
                    rangeValueBrackets += ")";
                }
            }
            foreach (int selectedItemId in selectedItemsIds)
            {
                checkBoxStr += "SELECT   qa2.q_question_question_id AS t1_ques_id "
		            + " FROM     q_question_attribute_option qa2 "
		            + " WHERE    q_attribute_options_attribute_option_id = " + selectedItemId
			        + " AND Q_Question_question_id IN (";
                checkBoxBrackets += ")";
            }
            List<QueQuestion> quesList = new List<QueQuestion>();
            DBUtils dbUtils = new DBUtils();
            String sqlStmt = "SELECT TOP " + countSelect
                + " Q.question_statement, QS.QUESTION_ID, SUM(QS.SCORE * QW.WEIGHTAGE) T2_SUM_SCORE, T1_SUM_SCORE, (T1_SUM_SCORE + SUM(QS.SCORE * QW.WEIGHTAGE)) T1_T2_SUM_SCORE"
                + " FROM Q_QUESTION_SCORE QS"
                + " JOIN Q_Question Q ON Q.question_id = QS.QUESTION_ID"
                    + " AND Q.App_Course_course_id = " + criteria.courseId
                    + " AND Q.App_Clo_id = " + criteria.cloId
                    + " AND Q.App_So_id = " + criteria.soId
                    + " AND Q.App_Course_Topic_id = " + criteria.courseTopicId
                + " JOIN QUE_ATTRIBUTE_WEIGHTAGE QW ON QS.ATTRIBUTE_ID = QW.ATTRIBUTE_ID"
                + " JOIN (SELECT QA.Q_Question_question_id AS T1_QUES_ID, SUM(QA.SCORE * A.WEIGHTAGE) T1_SUM_SCORE "
                + " FROM Q_Question_Attribute_Option QA "
                + " JOIN Q_Attribute_Options AO ON AO.attribute_option_id = QA.Q_Attribute_Options_attribute_option_id "
                + " JOIN Q_Attribute A ON A.attribute_id = AO.Q_Attribute_attribute_id "
                + " WHERE QA.Q_QUESTION_QUESTION_ID IN ( "
                + rangeValueStr
                + checkBoxStr
                + " 			SELECT q_question_question_id  "
                + "             FROM q_question_attribute_option qao123  "
                + "             GROUP  BY qao123.q_question_question_id  "
                + " 			HAVING Count(qao123.q_attribute_options_attribute_option_id) = " + criteria.selectedAttributeCount + ")  "
                + " 	GROUP  BY QA2.q_question_question_id"
                + rangeValueBrackets
                + checkBoxBrackets
                + " GROUP BY QA.Q_Question_question_id"
                + " ) TABLE1 ON TABLE1.T1_QUES_ID = QS.QUESTION_ID "
                + " GROUP BY QS.QUESTION_ID, Q.question_statement, T1_SUM_SCORE"
                + " ORDER BY T1_T2_SUM_SCORE DESC";
            //LogUtils.myLog.Info(sqlStmt);
            SqlDataReader attributeListReader = dbUtils.readOperation(sqlStmt);

            while (attributeListReader.Read())
            {
                QueQuestion att1 = new QueQuestion();

                att1.questionId = (int)attributeListReader["QUESTION_ID"];
                att1.questionStatement = attributeListReader["question_statement"].ToString();
                att1.sumScore = (int)attributeListReader["T1_T2_SUM_SCORE"];
                quesList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return quesList;
        }

        public KMSABET.MyPocos.QueAttribute getAttrbuteByID(int id)
        {
            KMSABET.MyPocos.QueAttribute att1 = new KMSABET.MyPocos.QueAttribute();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation("SELECT * FROM Q_Attribute A"
                + " JOIN App_CODE C ON C.CODE_ID = A.attribute_type_id"
                + " where A.attribute_id = " + id
                + " order by 1 desc");
            while (attributeListReader.Read())
            {
                att1.attributeID = (int)attributeListReader["attribute_id"];
                att1.attributeStatement = attributeListReader["attribute_statement"].ToString();
                att1.attributeTypeID = (int)attributeListReader["attribute_type_id"];
                att1.attributeWeight = (int)attributeListReader["WEIGHTAGE"];
                att1.AttributeType = attributeListReader["CODE_VALUE"].ToString();
                att1.isRelevanceApplicable = (Boolean)attributeListReader["IS_RELEVANCE_APPLICABLE"];
                break;
            }
            dbUtils.closeDBConnection();
            return att1;
        }

        public List<KMSABET.MyPocos.QueAttributeOption> getOptionsListByAttributeID(int attributeId)
        {
            List<KMSABET.MyPocos.QueAttributeOption> attributeList = new List<KMSABET.MyPocos.QueAttributeOption>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation("SELECT * FROM Q_Attribute_Options A"
                + " where A.Q_Attribute_attribute_id = " + attributeId);
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueAttributeOption att1 = new KMSABET.MyPocos.QueAttributeOption();

                att1.attributeId = (int)attributeListReader["Q_Attribute_attribute_id"];
                att1.optionStatement = attributeListReader["option_statement"].ToString();
                att1.priorityOption = attributeListReader["PRIORITY_OPTION"].ToString();
                att1.attributeOptionId = (int)attributeListReader["attribute_option_id"];
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public List<KMSABET.MyPocos.QueQuestion> getQuestionList(String courseID)
        {
            List<KMSABET.MyPocos.QueQuestion> attributeList = new List<KMSABET.MyPocos.QueQuestion>();
            DBUtils dbUtils = new DBUtils();
            String query = "SELECT * FROM Q_Question Q"
                 + " LEFT JOIN App_Course AC ON Q.App_Course_course_id = AC.course_id"
                 + " LEFT JOIN App_Instructor AI ON Q.App_Instructor_instructor_id = AI.instructor_id"
                 + " LEFT JOIN I_CLO CL ON CL.clo_id = Q.App_Clo_id"
                 + " LEFT JOIN APP_SO SO ON SO.SO_ID = Q.App_So_id"
                 + " LEFT JOIN APP_COURSE_TOPIC CT ON CT.TOPIC_ID = Q.App_Course_Topic_id"
                 + " WHERE Q.App_Course_course_id = " + courseID
                 + " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(query);
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueQuestion att1 = new KMSABET.MyPocos.QueQuestion();

                att1.questionId = (int)attributeListReader["question_id"];
                att1.questionStatement = attributeListReader["question_statement"].ToString();
                att1.courseId = (int)attributeListReader["course_id"];
                att1.courseName = attributeListReader["course_name"].ToString();
                att1.instructorId = (int)attributeListReader["instructor_id"];
                att1.instructorName = attributeListReader["instructor_name"].ToString();
                att1.courseTopicName = attributeListReader["TOPIC_STATEMENT"].ToString();
                att1.soStatement = attributeListReader["SO_STATEMENT"].ToString();
                att1.cloStatement = attributeListReader["clo_statement"].ToString();

                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public List<KMSABET.MyPocos.QueQuestion> getQuestionList()
        {
            List<KMSABET.MyPocos.QueQuestion> attributeList = new List<KMSABET.MyPocos.QueQuestion>();
            DBUtils dbUtils = new DBUtils();
            String query = "SELECT * FROM Q_Question Q"
                 + " LEFT JOIN App_Course AC ON Q.App_Course_course_id = AC.course_id"
                 + " LEFT JOIN App_Instructor AI ON Q.App_Instructor_instructor_id = AI.instructor_id"
                 + " LEFT JOIN I_CLO CL ON CL.clo_id = Q.App_Clo_id"
                 + " LEFT JOIN APP_SO SO ON SO.SO_ID = Q.App_So_id"
                 + " LEFT JOIN APP_COURSE_TOPIC CT ON CT.TOPIC_ID = Q.App_Course_Topic_id"
                 + " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(query);
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueQuestion att1 = new KMSABET.MyPocos.QueQuestion();

                att1.questionId = (int)attributeListReader["question_id"];
                att1.questionStatement = attributeListReader["question_statement"].ToString();
                att1.courseId = (int)attributeListReader["course_id"];
                att1.courseName = attributeListReader["course_name"].ToString();
                att1.instructorId = (int)attributeListReader["instructor_id"];
                att1.instructorName = attributeListReader["instructor_name"].ToString();
                att1.courseTopicName = attributeListReader["TOPIC_STATEMENT"].ToString();
                att1.soStatement = attributeListReader["SO_STATEMENT"].ToString();
                att1.cloStatement = attributeListReader["clo_statement"].ToString();

                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public List<KMSABET.MyPocos.QueFavQuestionList> getFavQuestionList()
        {
            List<KMSABET.MyPocos.QueFavQuestionList> favList = new List<KMSABET.MyPocos.QueFavQuestionList>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation("SELECT *"
                + " FROM Q_Favorite_List Q"
                + " LEFT JOIN App_Course AC ON Q.App_Course_course_id = AC.course_id"
                + " LEFT JOIN App_Instructor AI ON Q.App_Instructor_instructor_id = AI.instructor_id"
                + " order by 1 desc" );
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueFavQuestionList att1 = new KMSABET.MyPocos.QueFavQuestionList();

                att1.favQuestionId = (int)attributeListReader["favorite_list_id"];
                att1.favQuestionName = attributeListReader["Favorite_list_name"].ToString();
                att1.courseId = (int)attributeListReader["course_id"];
                att1.courseName = attributeListReader["course_name"].ToString();
                att1.instructorId = (int)attributeListReader["instructor_id"];
                att1.instructorName = attributeListReader["instructor_name"].ToString();
                favList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return favList;
        }

        public List<KMSABET.MyPocos.QueFavQuestionList> getFavQuestionList(String courseId, String instructorId = "")
        {
            List<KMSABET.MyPocos.QueFavQuestionList> favList = new List<KMSABET.MyPocos.QueFavQuestionList>();
            DBUtils dbUtils = new DBUtils();
            String myQuery = "SELECT *"
                + " FROM Q_Favorite_List Q"
                + " LEFT JOIN App_Course AC ON Q.App_Course_course_id = AC.course_id"
                + " LEFT JOIN App_Instructor AI ON Q.App_Instructor_instructor_id = AI.instructor_id"
                + " WHERE Q.App_Course_course_id = " + courseId;
            if (instructorId != "") myQuery += " AND App_Instructor_instructor_id = " + instructorId;
            myQuery += " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(myQuery);
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueFavQuestionList att1 = new KMSABET.MyPocos.QueFavQuestionList();

                att1.favQuestionId = (int)attributeListReader["favorite_list_id"];
                att1.favQuestionName = attributeListReader["Favorite_list_name"].ToString();
                att1.courseId = (int)attributeListReader["course_id"];
                att1.courseName = attributeListReader["course_name"].ToString();
                att1.instructorId = (int)attributeListReader["instructor_id"];
                att1.instructorName = attributeListReader["instructor_name"].ToString();
                favList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return favList;
        }

        public KMSABET.MyPocos.QueFavQuestionList getFavQuestionListByID(int favListID)
        {
            KMSABET.MyPocos.QueFavQuestionList favList = new KMSABET.MyPocos.QueFavQuestionList();
            DBUtils dbUtils = new DBUtils();
            String selectQuery = "SELECT *"
                + " FROM Q_Favorite_List Q"
                + " LEFT JOIN App_Course AC ON Q.App_Course_course_id = AC.course_id"
                + " LEFT JOIN App_Instructor AI ON Q.App_Instructor_instructor_id = AI.instructor_id"
                + " WHERE favorite_list_id = " + favListID
                + " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(selectQuery);
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueFavQuestionList att1 = new KMSABET.MyPocos.QueFavQuestionList();

                att1.favQuestionId = (int)attributeListReader["favorite_list_id"];
                att1.favQuestionName = attributeListReader["Favorite_list_name"].ToString();
                att1.courseId = (int)attributeListReader["course_id"];
                att1.courseName = attributeListReader["course_name"].ToString();
                att1.instructorId = (int)attributeListReader["instructor_id"];
                att1.instructorName = attributeListReader["instructor_name"].ToString();
                favList = att1;
                
                break;
            }
            dbUtils.closeDBConnection();
            return favList;
        }

        public List<KMSABET.MyPocos.QueAttributeOption> getOptionsListByAttributeIDQuestionID(int attributeID, int questionID)
        {
            List<KMSABET.MyPocos.QueAttributeOption> attributeList = new List<KMSABET.MyPocos.QueAttributeOption>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation(
                "SELECT *"
                    + " FROM [Q_Question] Q"
                    + " JOIN Q_Question_Attribute_Option QA ON Q.question_id = QA.Q_Question_question_id"
                    + " JOIN Q_Attribute_Options AO ON AO.attribute_option_id = QA.Q_Attribute_Options_attribute_option_id"
                    + " JOIN Q_Attribute A ON A.attribute_id = AO.Q_Attribute_attribute_id"
                    + " WHERE Q.question_id = " + questionID
                    + " AND A.attribute_id = "+ attributeID);
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueAttributeOption att1 = new KMSABET.MyPocos.QueAttributeOption();

                att1.attributeId = (int)attributeListReader["Q_Attribute_attribute_id"];
                att1.optionStatement = attributeListReader["option_statement"].ToString();
                att1.attributeOptionId = (int)attributeListReader["attribute_option_id"];
                att1.score = (int)attributeListReader["SCORE"];
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }


        public List<KMSABET.MyPocos.QueQuestion> questionList(List<int> ids)
        {
            List<KMSABET.MyPocos.QueQuestion> attributeList = new List<KMSABET.MyPocos.QueQuestion>();
                
            if (ids.Count > 0)
            {

                String selectedItems = "";
                foreach (int item in ids)
                {
                    selectedItems += item + ",";
                }

                if (selectedItems.Length > 0)
                    selectedItems = selectedItems.Substring(0, selectedItems.Length - 1);

                DBUtils dbUtils = new DBUtils();
                String generatedQuery = "SELECT * FROM [Q_Question] Q"
                      + " JOIN App_Instructor I ON I.instructor_id = Q.App_Instructor_instructor_id"
                      + " JOIN App_Course C ON C.course_id = Q.App_Course_course_id"
                      + " JOIN APP_COURSE_TOPIC CT ON CT.TOPIC_ID = Q.App_Course_Topic_id"
                      + " JOIN I_CLO IC ON IC.clo_id = Q.App_Clo_id"
                      + " JOIN APP_SO S ON S.SO_ID = Q.App_So_id"
                      + " where question_id in (" + selectedItems + ") order by 1 desc";
                SqlDataReader attributeListReader = dbUtils.readOperation(generatedQuery);
                while (attributeListReader.Read())
                {
                    KMSABET.MyPocos.QueQuestion att1 = new KMSABET.MyPocos.QueQuestion();

                    att1.questionId = (int)attributeListReader["question_id"];
                    att1.questionStatement = attributeListReader["question_statement"].ToString();
                    att1.questionType = (int)attributeListReader["question_type"];
                    att1.instructorId = (int)attributeListReader["instructor_id"];
                    att1.courseId = (int)attributeListReader["course_id"];
                    att1.courseName = attributeListReader["course_name"].ToString();
                    att1.instructorName = attributeListReader["instructor_name"].ToString();
                    att1.courseTopicId = (int)attributeListReader["TOPIC_ID"];
                    att1.courseTopicName = attributeListReader["TOPIC_STATEMENT"].ToString();
                    att1.cloId = (int)attributeListReader["clo_id"];
                    att1.cloStatement = attributeListReader["clo_statement"].ToString();
                    att1.soId = (int)attributeListReader["SO_ID"];
                    att1.soStatement = attributeListReader["SO_STATEMENT"].ToString();
                    
                    attributeList.Add(att1);
                }
                dbUtils.closeDBConnection();

            }
            return attributeList;
        }

        public List<KMSABET.MyPocos.QueQuestion> getQuestionListByFavID(int favID)
        {
            List<KMSABET.MyPocos.QueQuestion> attributeList = new List<KMSABET.MyPocos.QueQuestion>();

            DBUtils dbUtils = new DBUtils();
            String selectQuery = "SELECT * FROM "
                        + " Q_Favorite_List F"
                        + " JOIN Q_Favorite_List_has_Q_Question FL ON FL.Q_Favorite_List_favorite_list_id = F.favorite_list_id"
                        + " JOIN Q_Question Q on Q.question_id = FL.Q_Question_question_id "
                        + " JOIN App_Instructor I ON I.instructor_id = Q.App_Instructor_instructor_id"
                        + " JOIN App_Course C ON C.course_id = Q.App_Course_course_id"
                        + " where F.favorite_list_id = " + favID
                        + " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(selectQuery);
            while (attributeListReader.Read())
            {
                KMSABET.MyPocos.QueQuestion att1 = new KMSABET.MyPocos.QueQuestion();

                att1.questionId = (int)attributeListReader["question_id"];
                att1.questionStatement = attributeListReader["question_statement"].ToString();
                att1.questionType = (int)attributeListReader["question_type"];
                att1.instructorId = (int)attributeListReader["instructor_id"];
                att1.courseId = (int)attributeListReader["course_id"];
                att1.courseName = attributeListReader["course_name"].ToString();
                att1.instructorName = attributeListReader["instructor_name"].ToString();

                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();

            return attributeList;
        }

        public List<QueAttributeScore> getQuestionScoreListByID(int questionID)
        {
            List<QueAttributeScore> attributeList = new List<QueAttributeScore>();

            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation(
                "SELECT QUESTION_SCORE_ID, SCORE, ATTRIBUTE_ID, QUESTION_ID FROM "
                    + " Q_QUESTION_SCORE"
                    + " where QUESTION_ID = " + questionID
                    + " order by 1 desc");
            while (attributeListReader.Read())
            {
                QueAttributeScore att1 = new QueAttributeScore();

                att1.attributeID = (int)attributeListReader["ATTRIBUTE_ID"];
                att1.questionID = (int)attributeListReader["QUESTION_ID"];
                att1.questionScoreID = (int)attributeListReader["QUESTION_SCORE_ID"];
                att1.scoreValue = (int)attributeListReader["SCORE"];

                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();

            return attributeList;
        }

        public List<QueQuestionScore> getQuestionScoreDetailsByID(int questionID, 
            List<QuestionRangedValue> rangedValue, String selectedAttrOptIds)
        {
            List<QueQuestionScore> attributeList = new List<QueQuestionScore>();

            String rangedValueStr = "";
            String rangeValueScoreStr = "";
            bool anyRangeValue = false;
            if(rangedValue.Count > 0) {
                rangedValueStr = " WHEN A.attribute_type_id = 1002 THEN "
                    + " CASE ";
                foreach (QuestionRangedValue rang in rangedValue)
                {
                    if (rang.fromValue != "")
                    {
                        anyRangeValue = true;
                        rangedValueStr += " WHEN A.attribute_id = " + rang.attributeID
                            + " THEN '" + rang.fromValue + "-" + rang.toValue + "' ";
                        rangeValueScoreStr += " OR ( "
                            + rang.fromValue + " <= CASE WHEN Isnumeric(AO.option_statement) = 1 "
                                + " THEN CONVERT(INT, AO.option_statement) END "
                            + " AND " + rang.toValue + " >= CASE WHEN Isnumeric(AO.option_statement) = 1 "
                                + " THEN CONVERT(INT, AO.option_statement) END "
                            + " AND AO.Q_Attribute_attribute_id = " + rang.attributeID + ") ";
                    }
                }
                rangedValueStr += " END ";
            }

            if (anyRangeValue == false)
            {
                rangedValueStr = "";
                rangeValueScoreStr = "";
            }

            DBUtils dbUtils = new DBUtils();
            
            String myQuery = " SELECT ISNULL(S.SCORE, 0) SCORE_VALUE, C.CODE_VALUE ATTRIBUTE_STATEMENT"
                    + " , W.WEIGHTAGE WEIGHT_VALUE, (ISNULL(S.SCORE, 0) * W.WEIGHTAGE) TOTAL_SUM,"
                    + " 0 ATTRIBUTE_OPTION_ID,"
                    + " CASE WHEN S.ATTRIBUTE_ID = 1301 THEN IC.clo_statement "
		                + " WHEN S.ATTRIBUTE_ID = 1302 THEN ASO.SO_STATEMENT"
		                + " WHEN S.ATTRIBUTE_ID = 1303 THEN ACT.TOPIC_STATEMENT"
	                + " END AS ATTRIBUTE_OPTION_STATEMENT,"
                    + " CASE "
                        + " WHEN S.attribute_id = 1301 THEN IC.clo_statement "
                        + " WHEN S.attribute_id = 1302 THEN ASO.so_statement "
                        + " WHEN S.attribute_id = 1303 THEN ACT.topic_statement "
                       + " END                                  AS QUESTION_ATTRIBUTE," 
                    + "CASE "
                        + " WHEN S.attribute_id = 1301 THEN IC.clo_id "
                        + " WHEN S.attribute_id = 1302 THEN ASO.SO_ID "
                        + " WHEN S.attribute_id = 1303 THEN ACT.TOPIC_ID "
                    + " END                                  AS QUESTION_ATTRIBUTE_OPT_ID, "
	                + " S.attribute_id                       ATTRIBUTE_ID, "
                    + " S.ATTRIBUTE_ID ATTRIBUTE_ID, " + questionID.ToString() + " AS QUESTION_ID,"
                    + " NULL						AS 'ATTRIBUTE_TYPE_ID'"
                + " FROM Q_QUESTION_SCORE S"
                + " JOIN Q_Question Q ON Q.question_id = S.QUESTION_ID"
                + " JOIN I_CLO IC ON IC.clo_id = Q.App_Clo_id"
                + " JOIN APP_SO ASO ON ASO.SO_ID = Q.App_So_id"
                + " LEFT JOIN APP_COURSE_TOPIC ACT ON ACT.TOPIC_ID = Q.App_Course_Topic_id"
                + " JOIN QUE_ATTRIBUTE_WEIGHTAGE W ON S.ATTRIBUTE_ID = W.ATTRIBUTE_ID"
                + " JOIN App_CODE C ON C.CODE_ID = W.ATTRIBUTE_ID"
                + " WHERE S.QUESTION_ID = " + questionID.ToString()
                + " UNION"
                + " SELECT ISNULL(QA.SCORE, 0) SCORE_VALUE, A.attribute_statement ATTRIBUTE_STATEMENT, "
                    + " A.WEIGHTAGE WEIGHT_VALUE, (ISNULL(QA.SCORE, 0) * A.WEIGHTAGE) TOTAL_SUM,"
                    + " ISNULL(QA.Q_Attribute_Options_attribute_option_id, 0) ATTRIBUTE_OPTION_ID,"
                    + "  ISNULL(CASE WHEN A.attribute_type_id = 1001"
	                    + " THEN "
                    + " (SELECT QAO.option_statement FROM Q_Attribute_Options QAO WHERE A.attribute_id = "
                        + " QAO.Q_Attribute_attribute_id AND QAO.attribute_option_id IN ( " + selectedAttrOptIds + " )) "
                        + rangedValueStr
                        + " END, 'No Input/Selection') AS ATTRIBUTE_OPTION_STATEMENT,"
                    + " ISNULL((SELECT  TOP 1 AO.option_statement"
		                + " FROM Q_Question Q "
		                + " JOIN Q_Question_Attribute_Option QA1 ON QA1.Q_Question_question_id = Q.question_id "
		                + " JOIN Q_Attribute_Options AO ON AO.attribute_option_id = QA1.Q_Attribute_Options_attribute_option_id "
                        + " AND Q_Question_question_id = "
                        + questionID.ToString()
                        + " AND AO.Q_Attribute_attribute_id = A.attribute_id), 'No Input/Selection') QUESTION_ATTRIBUTE, "
                    + " (SELECT TOP 1 AO.attribute_option_id "
                        + " FROM   q_question Q "
                        + " JOIN q_question_attribute_option QA1 "
                        + " ON QA1.q_question_question_id = Q.question_id "
                        + " JOIN q_attribute_options AO "
                        + " ON AO.attribute_option_id = "
                        + " QA1.q_attribute_options_attribute_option_id "
                        + " AND q_question_question_id = " + questionID.ToString() 
                        + " AND AO.q_attribute_attribute_id = A.attribute_id) "
                                                             + " AS QUESTION_ATTRIBUTE_OPT_ID, "
                    + " A.attribute_id                                        ATTRIBUTE_ID, "
                    + " A.attribute_id ATTRIBUTE_ID, " + questionID.ToString() + " AS QUESTION_ID," 
	                + " A.attribute_type_id AS 'ATTRIBUTE_TYPE_ID'"
                + " FROM Q_Question_Attribute_Option QA"
                + " JOIN Q_Attribute_Options AO ON AO.attribute_option_id = QA.Q_Attribute_Options_attribute_option_id "
                    + " AND QA.Q_Question_question_id = " + questionID.ToString()
                    + " AND ( "
                    + " QA.Q_Attribute_Options_attribute_option_id IN (" + selectedAttrOptIds + " )"
                    + rangeValueScoreStr
                    + ") "
                + " RIGHT JOIN Q_Attribute A ON A.attribute_id = AO.Q_Attribute_attribute_id"
                + " WHERE a.COURSE_ID = 4"
                + " ORDER BY WEIGHT_VALUE DESC";

            SqlDataReader attributeListReader = dbUtils.readOperation(myQuery);

            while (attributeListReader.Read())
            {
                QueQuestionScore att1 = new QueQuestionScore();

                att1.attributeID = (int)attributeListReader["ATTRIBUTE_ID"];
                att1.scoreValue = (int)attributeListReader["SCORE_VALUE"];
                att1.weightValue = (int)attributeListReader["WEIGHT_VALUE"];
                att1.sumScoreValue = (int)attributeListReader["TOTAL_SUM"];
                att1.attributOptionId = (int)attributeListReader["ATTRIBUTE_OPTION_ID"];
                att1.attributeStatement = attributeListReader["ATTRIBUTE_STATEMENT"].ToString();
                att1.userSelectionStatement = attributeListReader["ATTRIBUTE_OPTION_STATEMENT"].ToString();
                att1.userSelectionStatement = att1.userSelectionStatement == "" ? "No Input/Selection" : att1.userSelectionStatement;
                att1.questionAttributeOptStatement = attributeListReader["QUESTION_ATTRIBUTE"].ToString();
                int quesIdIndex = attributeListReader.GetOrdinal("QUESTION_ID");
                if (!attributeListReader.IsDBNull(quesIdIndex))
                {
                    att1.questionID = (int)attributeListReader["QUESTION_ID"];
                }
                else
                {
                    att1.questionID = 0;
                }
                int quesAttOptIdIndex = attributeListReader.GetOrdinal("QUESTION_ATTRIBUTE_OPT_ID");
                if (!attributeListReader.IsDBNull(quesAttOptIdIndex))
                    att1.questionAttributeOptId = (int)attributeListReader["QUESTION_ATTRIBUTE_OPT_ID"];

                int attributeTypeIdInd = attributeListReader.GetOrdinal("ATTRIBUTE_TYPE_ID");
                if (!attributeListReader.IsDBNull(attributeTypeIdInd))
                {
                    att1.attributeTypeId = (int)attributeListReader["ATTRIBUTE_TYPE_ID"];
                }
                
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();

            return attributeList;
        }

        public List<AppCourseTopic> getCourseTopicList()
        {
            List<AppCourseTopic> attributeList = new List<AppCourseTopic>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation(
                "SELECT T.TOPIC_ID, T.TOPIC_STATEMENT, C.course_id, C.course_name, P.program_id, P.program_name"
                + " FROM APP_COURSE_TOPIC T"
                + " JOIN App_Course C ON C.course_id = T.COURSE_ID"
                + " JOIN App_Program P ON P.program_id = C.App_Program_program_id"
                + " ORDER BY COURSE_ID DESC");
            while (attributeListReader.Read())
            {
                AppCourseTopic att1 = new AppCourseTopic();

                att1.id = (int)attributeListReader["TOPIC_ID"];
                att1.topic = attributeListReader["TOPIC_STATEMENT"].ToString();
                att1.course = new AppCourse();
                att1.course.courseId = (int)attributeListReader["course_id"];
                att1.course.courseName = attributeListReader["course_name"].ToString();
                att1.course.program = new AppProgram();
                att1.course.program.programId = (int)attributeListReader["program_id"];
                att1.course.program.programName = attributeListReader["program_name"].ToString();
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public List<AppCLO> getCLOList()
        {
            List<AppCLO> attributeList = new List<AppCLO>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation(
                "SELECT T.clo_id, T.clo_statement, C.course_id, C.course_name"
                + " FROM I_CLO T"
                + " JOIN App_Course C ON C.course_id = T.App_Course_course_id"
                + " ORDER BY COURSE_ID DESC");

            while (attributeListReader.Read())
            {
                AppCLO att1 = new AppCLO();

                att1.cloId = (int)attributeListReader["clo_id"];
                att1.cloStatement = attributeListReader["clo_statement"].ToString();
                att1.courseId = (int)attributeListReader["course_id"];
                att1.courseName = attributeListReader["course_name"].ToString();
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public List<AppSO> getSOList()
        {
            List<AppSO> attributeList = new List<AppSO>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation(
                "SELECT T.SO_ID, T.SO_STATEMENT, P.program_id, P.program_name"
                + " FROM APP_SO T"
                + " JOIN App_Program P ON P.program_id = T.APP_PROGRAM_ID"
                + " ORDER BY PROGRAM_ID");

            while (attributeListReader.Read())
            {
                AppSO att1 = new AppSO();

                att1.id = (int)attributeListReader["SO_ID"];
                att1.statement = attributeListReader["SO_STATEMENT"].ToString();
                att1.program = new AppProgram();
                att1.program.programId = (int)attributeListReader["program_id"];
                att1.program.programName = attributeListReader["program_name"].ToString();
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public List<QueAttributeOption> getAttributeOptionsList()
        {
            List<QueAttributeOption> attributeList = new List<QueAttributeOption>();
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation(
                "SELECT AO.attribute_option_id, AO.option_statement, A.attribute_id, A.attribute_statement"
                + " FROM Q_Attribute A"
                + " JOIN Q_Attribute_Options AO ON A.attribute_id = AO.Q_Attribute_attribute_id"
                + " ORDER BY attribute_id DESC");

            while (attributeListReader.Read())
            {
                QueAttributeOption att1 = new QueAttributeOption();

                att1.attributeOptionId = (int)attributeListReader["attribute_option_id"];
                att1.optionStatement = attributeListReader["option_statement"].ToString();
                att1.attributeId = (int)attributeListReader["attribute_id"];
                att1.attributeStatement = attributeListReader["attribute_statement"].ToString();
                attributeList.Add(att1);
            }
            dbUtils.closeDBConnection();
            return attributeList;
        }

        public String insertQuestion(QueQuestion ques)
        {
            String query = "";

            query = @"Declare @a int; Declare @b int;" +
                "insert into Q_Question (App_Instructor_instructor_id,App_Course_course_id,App_Program_program_id, "
                    + " question_statement,question_type,App_Course_Topic_id,App_Clo_id,App_So_id) values (1,"
                    + ques.courseId + "," + "null" + ",'" + ques.questionStatement + "',1,"
                    + ques.courseTopicId + "," + ques.cloId + "," + ques.soId + ");" +
                "select @a= MAX(question_id) from Q_Question;" +
                "INSERT INTO Q_QUESTION_SCORE (SCORE, ATTRIBUTE_ID, QUESTION_ID) values (3,1301,@a)" +
                "INSERT INTO Q_QUESTION_SCORE (SCORE, ATTRIBUTE_ID, QUESTION_ID) values (4,1302,@a)" +
                "INSERT INTO Q_QUESTION_SCORE (SCORE, ATTRIBUTE_ID, QUESTION_ID) values (5,1303,@a)";
            //LogUtils.myLog.Info("Query : " + query);
            for (int j = 0; j < ques.attrOptionIds.Count; j++)
            {
                query += "insert into Q_Question_Attribute_Option "
                    + " (Q_Attribute_Options_attribute_option_id,Q_Question_question_id,SCORE) values ('" 
                    + ques.attrOptionIds[j] + "',@a,1);";
            }

            return query;
        }

        public List<FavListQueScore> getQuestionDetailsByQuesIDAndAssessmentID(int faveListID, int queID = 0)
        {
            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = queryAssessmentQuestionScoreList(faveListID, dbUtils, queID);
            List<FavListQueScore> toReturn = new List<FavListQueScore>();

            while (listReader.Read())
            {
                FavListQueScore queScore = new FavListQueScore();
                queScore.favListScoreID = Int32.Parse(listReader["Q_FAVORITE_LIST_QUESTION_SCORE_ID"].ToString());
                queScore.favListID = faveListID;
                queScore.quesID = queID;
                queScore.attrTypeID = Int32.Parse(listReader["ATTR_TYPE_ID"].ToString());
                queScore.attributeStatement = listReader["ATTRIBUTE_NAME"].ToString();
                int attrIdIndex = listReader.GetOrdinal("ATTR_ID");
                if (!listReader.IsDBNull(attrIdIndex))
                {
                    queScore.attributeID = (int)listReader["ATTR_ID"];
                }
                queScore.userSelectionStatement = listReader["USER_SELECTION"].ToString();
                queScore.userSelectionStatement = queScore.userSelectionStatement == "" ? "No Input/Selection" : queScore.userSelectionStatement;
                queScore.questionAttributeOptStatement = listReader["QUESTION_ATTRIBUTE"].ToString();

                int selectedAttrIdIndex = listReader.GetOrdinal("SELECTED_ATTR_OPT_ID");
                if (!listReader.IsDBNull(selectedAttrIdIndex))
                {
                    queScore.userSelectedID = (int)listReader["SELECTED_ATTR_OPT_ID"];
                }
                int queAttrOptIdIndex = listReader.GetOrdinal("QUES_ATTR_OPT_ID");
                if (!listReader.IsDBNull(queAttrOptIdIndex))
                {
                    queScore.quesAttributeID = (int)listReader["QUES_ATTR_OPT_ID"];
                }
                queScore.scoreValue = Int32.Parse(listReader["SCORE"].ToString());
                queScore.weightValue = Int32.Parse(listReader["WEIGHTAGE"].ToString());
                queScore.sumScoreValue = queScore.scoreValue * queScore.weightValue;
                toReturn.Add(queScore);
            }
            dbUtils.closeDBConnection();

            return toReturn;

        }

        private SqlDataReader queryAssessmentQuestionScoreList(int faveListID, DBUtils dbUtils, int queID = 0)
        {
            String myQuery =
                "SELECT [ATTR_ID]"
                + "       ,[ATTR_TYPE_ID]"
                + "       ,[SELECTED_ATTR_OPT_ID]"
                + "       ,[QUES_ATTR_OPT_ID]"
                + "       ,[QUESTION_ID]"
                + "       ,[SCORE]"
                + "       ,QFLQS.WEIGHTAGE"
                + "       ,QFLQS.[CLO_ID]"
                + "       ,QFLQS.[SO_ID]"
                + "       ,ACT.[TOPIC_ID]"
                + " 	  ,[Q_FAVORITE_LIST_QUESTION_SCORE_ID]"
                + " 	, CASE WHEN ATTR_TYPE_ID NOT IN (1301, 1302, 1303) THEN QA.attribute_statement "
                + " 		WHEN ATTR_TYPE_ID IN (1301, 1302, 1303) THEN AC.CODE_VALUE"
                + " 		END 'ATTRIBUTE_NAME'"
                + " 	, CASE WHEN ATTR_TYPE_ID NOT IN (1301, 1302, 1303, 1002) THEN QAO.option_statement "
                + " 		WHEN ATTR_TYPE_ID = 1301 THEN IC.clo_statement"
                + " 		WHEN ATTR_TYPE_ID = 1302 THEN ASO.SO_STATEMENT"
                + " 		WHEN ATTR_TYPE_ID = 1303 THEN ACT.TOPIC_STATEMENT"
                + "         WHEN ATTR_TYPE_ID = 1002 THEN QFLQS.RANGED_VALUE"
                + " 		END AS 'USER_SELECTION'"
                + " 	, CASE WHEN ATTR_TYPE_ID NOT IN (1301, 1302, 1303) THEN QAO1.option_statement "
                + " 		WHEN ATTR_TYPE_ID = 1301 THEN IC.clo_statement"
                + " 		WHEN ATTR_TYPE_ID = 1302 THEN ASO.SO_STATEMENT"
                + " 		WHEN ATTR_TYPE_ID = 1303 THEN ACT.TOPIC_STATEMENT"
                + " 		END AS 'QUESTION_ATTRIBUTE'"
                + " FROM Q_FAVORITE_LIST_QUESTION_SCORE QFLQS"
                + " LEFT JOIN Q_Attribute QA ON QFLQS.ATTR_ID = QA.attribute_id"
                + " LEFT JOIN Q_Attribute_Options QAO ON QAO.attribute_option_id = QFLQS.SELECTED_ATTR_OPT_ID"
                + " LEFT JOIN Q_Attribute_Options QAO1 ON QAO1.attribute_option_id = QFLQS.QUES_ATTR_OPT_ID"
                + " LEFT JOIN APP_CODE AC ON AC.CODE_ID = QFLQS.ATTR_TYPE_ID"
                + " LEFT JOIN I_CLO IC ON IC.clo_id = QFLQS.CLO_ID"
                + " LEFT JOIN APP_SO ASO ON ASO.SO_ID = QFLQS.SO_ID"
                + " LEFT JOIN APP_COURSE_TOPIC ACT ON ACT.TOPIC_ID = QFLQS.TOPIC_ID"
                + " WHERE FAVORITE_LIST_ID = " + faveListID;

            if (queID != 0)
                myQuery += " AND QUESTION_ID = " + queID;

            myQuery += " ORDER BY QFLQS.WEIGHTAGE DESC, QFLQS.SCORE DESC";

            SqlDataReader listReader = dbUtils.readOperation(myQuery);
            return listReader;
        }

        public void insertAssessmentQuesScoreList(int assessmentId, List<QueQuestionScore> queScoreList)
        {
            foreach (QueQuestionScore queScore in queScoreList) 
            {
                String insertStmt = " INSERT INTO [dbo].[Q_FAVORITE_LIST_QUESTION_SCORE]"
                + "           ([FAVORITE_LIST_ID]"
                + "           ,[QUESTION_ID]"
                + "           ,[ATTR_ID]"
                + "           ,[ATTR_TYPE_ID]"
                + "           ,[SELECTED_ATTR_OPT_ID]"
                + "           ,[QUES_ATTR_OPT_ID]"
                + "           ,[SCORE]"
                + "           ,[WEIGHTAGE]"
                + "           ,[CLO_ID]"
                + "           ,[SO_ID]"
                +"            ,[TOPIC_ID]"
                + "           ,[RANGED_VALUE])"
                + "     VALUES"
                + "           (" + assessmentId
                + "           ," + queScore.questionID
                + "           ," + ((queScore.attributeID == 1301 || queScore.attributeID == 1302 || queScore.attributeID == 1303 || queScore.attributeID == 0) ? "null" : queScore.attributeID.ToString())
                + "           ," + ((queScore.attributeID == 1301 || queScore.attributeID == 1302 || queScore.attributeID == 1303) ? queScore.attributeID.ToString() : queScore.attributeTypeId.ToString())
                + "           ," + ((queScore.attributOptionId == 0) ? "null" : queScore.attributOptionId.ToString())
                + "           ," + ((queScore.attributeID == 1301 || queScore.attributeID == 1302 || queScore.attributeID == 1303 || queScore.attributeID == 0) ? "null" : queScore.questionAttributeOptId.ToString())
                + "           ," + queScore.scoreValue
                + "           ," + queScore.weightValue
                + "           ," + (queScore.attributeID == 1301 ? queScore.questionAttributeOptId.ToString() : "NULL")
                + "           ," + (queScore.attributeID == 1302 ? queScore.questionAttributeOptId.ToString() : "NULL")
                + "           ," + (queScore.attributeID == 1303 ? queScore.questionAttributeOptId.ToString() : "NULL")
                + "           ," + (queScore.attributeTypeId == 1002 ? "'" + queScore.userSelectionStatement.ToString() + "'" : "NULL")
                + ")";

                DBUtils dbUtilsObj2 = new DBUtils();
                dbUtilsObj2.CUDOperations(insertStmt);
            }
        }
    }
}