using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class ButtonArgsTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void MyBtnHandler(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.CommandName)
            {
                case "ThisBtnClick":
                    LogUtils.myLog.Info(btn.CommandArgument.ToString());
                    break;
                case "ThatBtnClick":
                    LogUtils.myLog.Info(btn.CommandArgument.ToString());
                    break;
            }
        }
    }
}