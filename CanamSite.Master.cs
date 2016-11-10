using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CanamLiveFA
{
    public partial class CanamSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BLL.CommonFunctions.GetSessionValue("User") == null && !Request.Path.ToString().ToLower().Contains("login"))
                Response.Redirect("~/Login.aspx");

            if (Request.Path.ToString().ToLower().Contains("login"))
                plcUserInfo.Visible = false;
        }
    }
}