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
    public partial class QueGenerateQuestions : System.Web.UI.Page
    {

        private class QueDetails
        {
            public int cloId { get; set; }
            public int soId { get; set; }
            public int topicId { get; set; }
        }

        List<int> optionIds = new List<int>();
        List<QueAttributeOption> optionList = new List<QueAttributeOption>();
        int totalQuesCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GenerateQuestions(object sender, EventArgs e)
        {
            LogUtils.myLog.Info(numberQuestions.Text);
            String courseId = Session["CourseID"].ToString();
            LogUtils.myLog.Info("Session[\"CourseID\"] : " + Session["CourseID"]);
            LogUtils.myLog.Info("Before courseId : " + courseId);
            if (courseId == null || courseId == "")
            {
                courseId = "4";
            }

            LogUtils.myLog.Info("After courseId : " + courseId);

            LogUtils.myLog.Info("Button Generate Questions clicked." + DateTime.Now.ToString());
            AppDao appDaoObj = new AppDao();

            List<AppCourseTopic> topicList = appDaoObj.getCourseTopicList(courseId);
            List<AppCLO> CLOList = appDaoObj.getCLOList(courseId);

            QueDao queDaoObj = new QueDao();

            String courseIdLocal = Session["CourseID"].ToString();
            List<QueAttribute> attrList = queDaoObj.getAttrbuteList(courseIdLocal);
            foreach (QueAttribute attr in attrList)
            {
                attr.optionsList = queDaoObj.getAttributeOptionListByAttrID(attr.attributeID.ToString());
            }

            foreach (AppCLO clo in CLOList)
            {
                List<AppSO> SOList = appDaoObj.getSOList(clo.cloId.ToString());
                clo.soList = SOList;
            }

            foreach (AppCourseTopic courseTopic in topicList)
            {
                LogUtils.myLog.Info("Course Topic : " + courseTopic.topic);
                foreach (AppCLO clo in CLOList)
                {
                    LogUtils.myLog.Info("CLO : " + clo.cloStatement);

                    foreach (AppSO so in clo.soList)
                    {
                        LogUtils.myLog.Info("SO : " + so.statement);
                        QueQuestion queDetails = new QueQuestion();
                        queDetails.cloId = clo.cloId;
                        queDetails.soId = so.id;
                        queDetails.courseTopicId = courseTopic.id;
                        queDetails.courseId = Int32.Parse(courseId);
                        queDetails.attrOptionIds = new List<int>();
                        try
                        {
                            SqlConnection myConnection = new SqlConnection(MyConstants.DBConnectionString);
                            //LogUtils.myLog.Info(MyConstants.DBConnectionString);
                            myConnection.Open();

                            display(attrList, attrList.Count, queDetails, myConnection);

                            myConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            LogUtils.myLog.Debug(e.ToString(), ex);
                        }

                        LogUtils.myLog.Info("After inserting questions. Total Questions : " + totalQuesCount);
                    }
                }

            }

            /*List<QueAttribute> attrList = new List<QueAttribute>();
            QueAttribute attr1 = new QueAttribute();
            attr1.attributeID = 1;
            attr1.attributeStatement = "Time";
            attr1.optionsList = new List<QueAttributeOption>();
            QueAttributeOption o1 = new QueAttributeOption();
            o1.attributeId = 1;
            o1.optionStatement = "5 mins";
            o1.attributeOptionId = 1;
            attr1.optionsList.Add(o1);

            QueAttributeOption o2 = new QueAttributeOption();
            o2.attributeId = 1;
            o2.optionStatement = "10 mins";
            o2.attributeOptionId = 2;
            attr1.optionsList.Add(o2);

            QueAttributeOption o5 = new QueAttributeOption();
            o5.attributeId = 1;
            o5.optionStatement = "15 mins";
            o5.attributeOptionId = 5;
            attr1.optionsList.Add(o5);

            QueAttribute attr2 = new QueAttribute();
            attr2.attributeID = 1;
            attr2.attributeStatement = "Type";
            attr2.optionsList = new List<QueAttributeOption>();
            QueAttributeOption o3 = new QueAttributeOption();
            o3.attributeId = 2;
            o3.optionStatement = "MCQ";
            o3.attributeOptionId = 1;
            attr2.optionsList.Add(o3);

            QueAttributeOption o4 = new QueAttributeOption();
            o4.attributeId = 2;
            o4.optionStatement = "FB";
            o4.attributeOptionId = 2;
            attr2.optionsList.Add(o4);

            attrList.Add(attr1);
            attrList.Add(attr2);
            display(attrList, attrList.Count);
            */
        }

        void display(List<QueAttribute> attr, int size, QueQuestion que, SqlConnection myConnection)
        {
		    int count=0, i=0 , j=0, max=1;
		    for(i=0 ; i<size ; i++)
			    max =max * attr[i].optionsList.Count;
		    List<int> attr_decsn = new List<int>();
            for (int l = 0; l < size; l++)
            {
                attr_decsn.Add(0);
            }

            while (max > count++)
                {
                    for (int z = 0; z <= size - 1; z++)
                    {
                        if (attr_decsn[z] == attr[z].optionsList.Count)
                        {
                            attr_decsn[z] = 0;
                            attr_decsn[z + 1]++;
                        }
                        que.attrOptionIds.Add(attr[z].optionsList[attr_decsn[z]].attributeOptionId);
                        //LogUtils.myLog.Info(attr[z].optionsList[attr_decsn[z]].optionStatement);
                    }
                    attr_decsn[0]++;
                    QueDao queDaoObj = new QueDao();

                    //LogUtils.myLog.Info("Going to print new questions");
                    for (int iter = 0; iter < Int32.Parse(numberQuestions.Text); iter++)
                    {
                        
                        totalQuesCount++;
                        que.questionStatement = "Dummy Question " + totalQuesCount + " " + DateTime.Now.ToString()
                            + " System Generated.";
                        String query = queDaoObj.insertQuestion(que);
                        
                        try
                        {
                            SqlCommand myCommand = new SqlCommand(query, myConnection);
                            //LogUtils.myLog.Info(query);
                            myCommand.ExecuteNonQuery();
                            //LogUtils.myLog.Info("After inserting into DB. Total Questions : " + totalQuesCount);
                        }
                        catch (Exception e)
                        {
                            LogUtils.myLog.Debug(e.ToString(), e);
                        }

                    }
                    que.attrOptionIds = new List<int>();

                }
        }


    }
}