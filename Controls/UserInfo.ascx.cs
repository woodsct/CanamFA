using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CanamLiveFA.Controls
{
    public partial class UserInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (BLL.CommonFunctions.GetSessionValue("User") != null)
            {
                DO.User userObj = (DO.User)BLL.CommonFunctions.GetSessionValue("User");
                userObj = DAL.User.GetUserByUserName(userObj.UserName);
                lblUser.Text = userObj.UserName;
                lblTeam.Text = userObj.Team.ToString();
                BLL.CommonFunctions.SetSessionValue("User", userObj);

                lblFreeAgencyStarted.Text = (BLL.CommonFunctions.GetApplicationValue("Player Reset") != null)
                    ? "Free Agency Has Begun" : "Free Agency Has Not Begun";

                if (userObj.Commissioner)
                    plcCommissioner.Visible = true;
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            BLL.CommonFunctions.RemoveSessionValue("User");
            Response.Redirect("~/Login.aspx");
        }
    }
}