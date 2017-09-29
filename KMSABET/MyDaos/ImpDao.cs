using KMSABET.MyPocos;
using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KMSABET.MyDaos
{
    public class ImpDao
    {
        public List<ImpRuleQuestion> getRuleQuestionList(int cloID = 0, int quesTypeID = 0)
        {
            List<ImpRuleQuestion> quesList = new List<ImpRuleQuestion>();
            DBUtils dbUtils = new DBUtils();
            String myQuery = "SELECT *"
                + " FROM I_Rule_Question q"
                + " JOIN I_CLO C ON Q.I_CLO_clo_id = C.clo_id"
                + " JOIN App_Course AC ON AC.course_id = C.App_Course_course_id"
                + " JOIN App_Program AP ON AP.program_id = AC.App_Program_program_id"
                + " JOIN App_CODE CD on CD.CODE_ID = Q.rule_question_type_id"
                + " WHERE 1=1";
            myQuery += cloID != 0 ? " AND q.I_CLO_clo_id = " + cloID : "";
            myQuery += quesTypeID != 0 ? " AND q.rule_question_type_id = " + quesTypeID : "";
            myQuery += " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(myQuery);
            while (attributeListReader.Read())
            {
                ImpRuleQuestion ques1 = new ImpRuleQuestion();

                ques1.ruleQuesId = (int)attributeListReader["rule_question_id"];
                ques1.ruleQuestionStatemet = attributeListReader["rule_ques_statement"].ToString();
                ques1.dBQueryStatement = attributeListReader["DATABASE_QUERY_STATEMENT"].ToString();
                ques1.ruleQuesTypeID = (int)attributeListReader["rule_question_type_id"];
                ques1.ruleQuesType = attributeListReader["CODE_VALUE"].ToString();
                AppCLO myClo = new AppCLO();
                myClo.cloId = (int)attributeListReader["clo_id"];
                myClo.cloStatement = attributeListReader["clo_statement"].ToString();
                myClo.courseId = (int)attributeListReader["course_id"];
                myClo.courseName = attributeListReader["course_name"].ToString();
                myClo.programId = (int)attributeListReader["program_id"];
                myClo.programName = attributeListReader["program_name"].ToString();
                ques1.cloData = myClo;
                quesList.Add(ques1);
            }
            dbUtils.closeDBConnection();
            return quesList;
        }

        public ImpRuleQuestion getRuleQuestionByID(int ruleQeustionID)
        {
            ImpRuleQuestion ques1 = new ImpRuleQuestion();
            DBUtils dbUtils = new DBUtils();
            String myQuery = "SELECT *"
                + " FROM I_Rule_Question q"
                + " JOIN I_CLO C ON Q.I_CLO_clo_id = C.clo_id"
                + " JOIN App_Course AC ON AC.course_id = C.App_Course_course_id"
                + " JOIN App_Program AP ON AP.program_id = AC.App_Program_program_id"
                + " JOIN App_CODE CD on CD.CODE_ID = Q.rule_question_type_id"
                + " WHERE rule_question_id = " + ruleQeustionID
                + " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(myQuery);
            while (attributeListReader.Read())
            {
                ques1.ruleQuesId = (int)attributeListReader["rule_question_id"];
                ques1.ruleQuestionStatemet = attributeListReader["rule_ques_statement"].ToString();
                ques1.dBQueryStatement = attributeListReader["DATABASE_QUERY_STATEMENT"].ToString();
                ques1.ruleQuesTypeID = (int)attributeListReader["rule_question_type_id"];
                ques1.ruleQuesType = attributeListReader["CODE_VALUE"].ToString();
                AppCLO myClo = new AppCLO();
                myClo.cloId = (int)attributeListReader["clo_id"];
                myClo.cloStatement = attributeListReader["clo_statement"].ToString();
                myClo.courseId = (int)attributeListReader["course_id"];
                myClo.courseName = attributeListReader["course_name"].ToString();
                myClo.programId = (int)attributeListReader["program_id"];
                myClo.programName = attributeListReader["program_name"].ToString();
                ques1.cloData = myClo;
            }
            dbUtils.closeDBConnection();
            return ques1;
        }

        public List<ImpRuleQuesAnswer> getRuleAnswerList(int ruleQuestionID = 0, int quesTypeID = 0)
        {
            List<ImpRuleQuesAnswer> quesAnsList = new List<ImpRuleQuesAnswer>();
            String myQuery = "SELECT *"
                + " FROM I_Rule_Ques_Answer QA"
                + " JOIN I_Rule_Question Q ON Q.rule_question_id = QA.I_Rule_Question_rule_question_id"
                + " WHERE 1=1";

            myQuery += ruleQuestionID !=0 ? " AND QA.I_Rule_Question_rule_question_id = " + ruleQuestionID : "";
            myQuery += quesTypeID != 0 ? " AND Q.rule_question_type_id = " + quesTypeID : "";
            
            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);
            while (listReader.Read())
            {
                ImpRuleQuesAnswer obj1 = new ImpRuleQuesAnswer();

                obj1.answerId = (int)listReader["answer_id"];
                obj1.answerStatemet = listReader["answer_statement"].ToString();
                obj1.questionId = (int)listReader["I_Rule_Question_rule_question_id"];
                quesAnsList.Add(obj1);
            }
            dbUtils.closeDBConnection();
            return quesAnsList;
        }

        public ImpRuleQuesAnswer getRuleAnswerByRuleIDQuestionID(int ruleQuestionID, int ruleID)
        {
            ImpRuleQuesAnswer obj1 = new ImpRuleQuesAnswer();
            String myQuery = "SELECT *"
                  + " FROM I_Rule_Case RC"
                  + " JOIN I_Rule_Ques_Answer QA ON QA.answer_id = RC.I_Rule_Ques_Answer_answer_id"
                  + " JOIN I_Rule_Question RQ ON RQ.rule_question_id = QA.I_Rule_Question_rule_question_id"
                  + " WHERE RQ.rule_question_id = " + ruleQuestionID
                  + " AND RC.I_Rule_rule_id = " + ruleID;

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);
            while (listReader.Read())
            {

                obj1.answerId = (int)listReader["answer_id"];
                obj1.questionId = (int)listReader["rule_question_id"];
                
            }
            dbUtils.closeDBConnection();
            return obj1;
        }

        public ImpRuleQuesAnswer getRuleDBValueByRuleIDQuestionID(int ruleQuestionID, int ruleID)
        {
            ImpRuleQuesAnswer obj1 = new ImpRuleQuesAnswer();
            String myQuery = "SELECT *"
                  + " FROM I_Rule_Case RC"
                  + " WHERE RC.I_RULE_QUESTION_ID = " + ruleQuestionID
                  + " AND RC.I_Rule_rule_id = " + ruleID;

            DBUtils dbUtils = new DBUtils();
            SqlDataReader listReader = dbUtils.readOperation(myQuery);
            while (listReader.Read())
            {

                obj1.questionId = (int)listReader["I_RULE_QUESTION_ID"];
                obj1.comparisonValue = (int)listReader["COMPARISON_VALUE"];
                obj1.comparisonTypeID = (int)listReader["COMPARISON_TYPE"];
            }
            dbUtils.closeDBConnection();
            return obj1;
        }

        public List<ImpRule> getRuleList(int cloID = 0)
        {
            List<ImpRule> quesList = new List<ImpRule>();
            DBUtils dbUtils = new DBUtils();
            String myQuery = " select * from I_Rule R"
                + " JOIN I_CLO C ON R.I_CLO_clo_id = C.clo_id"
                + " JOIN App_Course AC ON AC.course_id = C.App_Course_course_id"
                + " JOIN App_Program AP ON AP.program_id = AC.App_Program_program_id"
                + " WHERE 1=1";
            myQuery += cloID != 0 ? " AND q.I_CLO_clo_id = " + cloID : "";
            myQuery += " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(myQuery);
            while (attributeListReader.Read())
            {
                ImpRule ques1 = new ImpRule();

                ques1.ruleId = (int)attributeListReader["rule_id"];
                ques1.ruleStatemet = attributeListReader["rule_statement"].ToString();
                AppCLO myClo = new AppCLO();
                myClo.cloId = (int)attributeListReader["clo_id"];
                myClo.cloStatement = attributeListReader["clo_statement"].ToString();
                myClo.courseId = (int)attributeListReader["course_id"];
                myClo.courseName = attributeListReader["course_name"].ToString();
                myClo.programId = (int)attributeListReader["program_id"];
                myClo.programName = attributeListReader["program_name"].ToString();
                ques1.cloData = myClo;
                quesList.Add(ques1);
            }
            dbUtils.closeDBConnection();
            return quesList;
        }

        public ImpRule getRuleByID(int ruleID)
        {
            ImpRule ques1 = new ImpRule();
            DBUtils dbUtils = new DBUtils();
            String myQuery = " select * from I_Rule R"
                + " JOIN I_CLO C ON R.I_CLO_clo_id = C.clo_id"
                + " JOIN App_Course AC ON AC.course_id = C.App_Course_course_id"
                + " JOIN App_Program AP ON AP.program_id = AC.App_Program_program_id"
                + " WHERE rule_id="+ruleID;
            myQuery += " order by 1 desc";
            SqlDataReader attributeListReader = dbUtils.readOperation(myQuery);
            while (attributeListReader.Read())
            {
                
                ques1.ruleId = (int)attributeListReader["rule_id"];
                ques1.ruleStatemet = attributeListReader["rule_statement"].ToString();
                AppCLO myClo = new AppCLO();
                myClo.cloId = (int)attributeListReader["clo_id"];
                myClo.cloStatement = attributeListReader["clo_statement"].ToString();
                myClo.courseId = (int)attributeListReader["course_id"];
                myClo.courseName = attributeListReader["course_name"].ToString();
                myClo.programId = (int)attributeListReader["program_id"];
                myClo.programName = attributeListReader["program_name"].ToString();
                ques1.cloData = myClo;
                
            }
            dbUtils.closeDBConnection();
            return ques1;
        }

        public AppCLO getCloByCloID(int cloID)
        {
            AppCLO myClo = new AppCLO();
            DBUtils dbUtils = new DBUtils();
            String myQuery = "select * from I_CLO C"
                + " JOIN App_Course AC ON AC.course_id = C.App_Course_course_id"
                + " JOIN App_Program AP ON AP.program_id = AC.App_Program_program_id"
                + " WHERE clo_id=" + cloID;
            SqlDataReader attributeListReader = dbUtils.readOperation(myQuery);
            while (attributeListReader.Read())
            {

                myClo.cloId = (int)attributeListReader["clo_id"];
                myClo.cloStatement = attributeListReader["clo_statement"].ToString();
                myClo.courseId = (int)attributeListReader["course_id"];
                myClo.courseName = attributeListReader["course_name"].ToString();
                myClo.programId = (int)attributeListReader["program_id"];
                myClo.programName = attributeListReader["program_name"].ToString();
                
            }
            dbUtils.closeDBConnection();
            return myClo;
        }

        public int executeSavedDBQuery(String dbQueryStatement)
        {
            DBUtils dbUtils = new DBUtils();
            SqlDataReader attributeListReader = dbUtils.readOperation(dbQueryStatement);
            int returnResultIntValue = 0;
            while (attributeListReader.Read())
            {
                returnResultIntValue = (int)attributeListReader["QUERY_RESULT"];
            }
            dbUtils.closeDBConnection();
            return returnResultIntValue;
        }
    }
}