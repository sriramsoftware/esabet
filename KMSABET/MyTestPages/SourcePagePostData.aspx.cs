using KMSABET.MyPocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KMSABET.MyTestPages
{
    public partial class SourcePagePostData : System.Web.UI.Page
    {
        public int sourceData
        {
            get
            {
                return 33;
            }
        }

        public QueAttribute attributeData
        {
            get
            {
                QueAttribute obj = new QueAttribute();
                obj.attributeID = 44;
                obj.attributeStatement = "My Statement";
                return obj;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Server.Transfer("TargetPagePostData.aspx", true);
        }
    }
}