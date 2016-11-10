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
                lblBidFeesPayable.Text = DAL.Bids.BidFeesPayable(userObj.Id).ToString();
                lblBidFeesReceivable.Text = DAL.Bids.BidFeesReceivable(userObj.Id).ToString();
                BLL.CommonFunctions.SetSessionValue("User", userObj);

                if (BLL.CommonFunctions.GetApplicationValue("Block End Time") != null)
                {
                    plcBlockEndTime.Visible = true;
                    lblBlockEnd.Text = BLL.CommonFunctions.GetApplicationValue("Block End Time").ToString();
                    DateTime BlockEndTime = DateTime.Parse(lblBlockEnd.Text);
                    if (BlockEndTime > DateTime.Now && BlockEndTime < DateTime.Now.AddMinutes(5))
                    {
                        lblBlockAdditionalInfo.Text = "Less Than 5 Minutes Left";
                        lblBlockAdditionalInfo.Visible = true;
                    }
                    else if (BlockEndTime < DateTime.Now)
                    {
                        lblBlockAdditionalInfo.Text = "Block Has Ended";
                        lblBlockAdditionalInfo.Visible = true;
                    }
                }
                else
                {
                    lblBlockAdditionalInfo.Text = "Block Has Not Begun";
                    lblBlockAdditionalInfo.Visible = true;
                }

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