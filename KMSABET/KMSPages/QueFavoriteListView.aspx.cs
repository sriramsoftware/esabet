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
    public partial class QueFavoriteListView : System.Web.UI.Page
    {
        public List<MyPocos.QueQuestion> questionList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int favQueListID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
                if(favQueListID != 0) 
                {
                    bool deleteMode = Request.QueryString["delete"] == null ? false : bool.Parse(Request.QueryString["delete"]);
                    if (deleteMode)
                    {
                        DBUtils dbUtilsObj = new DBUtils();

                        String deleteQuery
                            = "delete from Q_Favorite_List_has_Q_Question "
                                + " where Q_Favorite_List_favorite_list_id =" + favQueListID;
                        dbUtilsObj.CUDOperations(deleteQuery);

                        deleteQuery
                            = "DELETE FROM Q_FAVORITE_LIST_QUESTION_SCORE WHERE FAVORITE_LIST_ID = " + favQueListID;
                        dbUtilsObj.CUDOperations(deleteQuery);

                        String updateQuery
                                    = "delete from Q_Favorite_List "
                                        + " where favorite_list_id = " + favQueListID;
                        dbUtilsObj.CUDOperations(updateQuery);

                        Response.Redirect("~/KMSPages/QueFavQuestionList.aspx");
                    }

                    QueDao queDaoObj = new QueDao();
                    MyPocos.QueFavQuestionList favList = queDaoObj.getFavQuestionListByID(favQueListID);
                    favListName1.Text = favList.favQuestionName;

                    questionList = queDaoObj.getQuestionListByFavID(favQueListID);
                    questionsListSelectedTag.DataSource = questionList;
                    questionsListSelectedTag.DataBind();

                }
                
            }
        }

        public void MyBtnHandler(Object sender, EventArgs e)
        {

            selectedQuestionPanel.Visible = true;
            int favQueListID = Request.QueryString["id"] == null ? 0 : Int32.Parse(Request.QueryString["id"]);
            Button btn = (Button)sender;
            selectedQuestionPanel.Visible = true;
            selectedQuestionStatment.Text = btn.CommandName.ToString();
            int quesID = Int32.Parse(btn.CommandArgument);
            QueDao queDaoObj = new QueDao();
            List<FavListQueScore> favQueDetailList = queDaoObj.getQuestionDetailsByQuesIDAndAssessmentID(favQueListID, quesID);
            int sum = 0;
            foreach (FavListQueScore queScore in favQueDetailList)
            {
                sum += queScore.sumScoreValue;
            }

            Label4.Text = sum.ToString();
            selectedQuestionDetailsView.DataSource = favQueDetailList;
            selectedQuestionDetailsView.DataBind();

            AjaxControlToolkit.ModalPopupExtender ModalPopupExtender =
               this.Page.FindControl("ctl00$MainContent$MsgBoxModalPopupExtender") as AjaxControlToolkit.ModalPopupExtender;
            UpdatePanel UpdatePanel = this.Page.FindControl("ctl00$MainContent$UpdatePanelMsgBox") as UpdatePanel;
            UpdatePanel.Update();
            ModalPopupExtender.Show();
        }
    }
}