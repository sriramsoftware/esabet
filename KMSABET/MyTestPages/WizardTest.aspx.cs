using KMSABET.MyUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class WizardTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = TextBox1.Text; Label2.Text = TextBox2.Text;
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (Wizard1.ActiveStepIndex == 1)
            {
                //Wizard1.ActiveStepIndex = 1;
                Button myPreviousBtn = (Button) Wizard1.FindControl("StepNavigationTemplateContainerID$StepPreviousButton");
                if (myPreviousBtn != null)
                {
                    LogUtils.myLog.Info("My Previous Button is not null");
                    myPreviousBtn.Visible = false;
                }
                LogUtils.myLog.Info("I am inside 1st tab. Input Value is: " + TextBox1.Text);
            }
        }
    }
}