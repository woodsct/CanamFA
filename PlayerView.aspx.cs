using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CanamLiveFA.DO;
using CanamLiveFA.BLL;

namespace CanamLiveFA
{
    public partial class PlayerView : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString != null && Request.QueryString.Get("Id") != null)
            {
                DO.User userObj = (DO.User)BLL.CommonFunctions.GetSessionValue("User");
                Player currentPlayer = DAL.Player.GetPlayer(int.Parse(Request.QueryString.Get("Id")));
                lblPlayerName.Text = currentPlayer.PlayerName;
                
                plcBidChart.Visible = true;
                grdPlayerBids.DataSource = DAL.Player.GetUserBidsByPlayer(currentPlayer.Id, (int)userObj.Team);
                grdPlayerBids.DataBind();

                if (currentPlayer.CurrentTeam == userObj.Team)
                    btnMatch.Visible = true;

                if (BLL.CommonFunctions.GetApplicationValue("Player Reset") != null && !userObj.Commissioner)
                    grdPlayerBids.Columns[5].Visible = false;
                else if (BLL.CommonFunctions.GetApplicationValue("Player Reset") != null && userObj.Commissioner)
                    grdPlayerBids.Columns[5].Visible = false;

                if (currentPlayer.Signed)
                {
                    plcBidding.Visible = false;
                    btnPlaceBid.Visible = false;
                }

                double amount;
                if (double.TryParse(txtAmountPerYear.Text, out amount))
                {
                    if (chkNoTrade.Checked)
                        lblTotalContractValue.Text = ((int.Parse(ddlNumOfYears.SelectedValue) + 1) * amount * 1.1).ToString();
                    else
                        lblTotalContractValue.Text = ((int.Parse(ddlNumOfYears.SelectedValue) + 1) * amount).ToString();
                }
            }
            else
                Response.Redirect("~/Login.aspx");
        }

        protected void btnPlaceBid_Click(object sender, EventArgs e)
        {
            if (BLL.CommonFunctions.GetApplicationValue("Player Reset") != null)
            {
                DO.User userObj = (DO.User)BLL.CommonFunctions.GetSessionValue("User");
                lblAmountError.Visible = false;
                plcError.Visible = false;
                int amountPerYear = 0;
                if (int.TryParse(txtAmountPerYear.Text, out amountPerYear))
                {
                    Player currentPlayer = DAL.Player.GetPlayer(int.Parse(Request.QueryString.Get("Id")));
                    string errorStr = string.Empty;
                    Contracts.freeAgentValueCalc(currentPlayer, int.Parse(ddlNumOfYears.SelectedValue), amountPerYear, chkNoTrade.Checked, ref errorStr, userObj);
                    grdPlayerBids.DataSource = DAL.Player.GetUserBidsByPlayer(currentPlayer.Id, (int)userObj.Team);
                    grdPlayerBids.DataBind();

                    if (string.IsNullOrWhiteSpace(errorStr))
                    {
                        lblError.Text = "Bid Successfully Entered!!";
                        plcError.Visible = true;
                    }
                    else
                    {
                        lblError.Text = errorStr;
                        plcError.Visible = true;
                    }
                }
                else
                    lblAmountError.Visible = true;
            }
            else
            {
                lblError.Text = "Unable to place bid as free agency has not started";
                plcError.Visible = true;
            }
        }

        protected void lnkRemove_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            DAL.Bids.RemoveBid(int.Parse(Request.QueryString.Get("Id")), int.Parse(lb.CommandArgument));
            Response.Redirect(Request.RawUrl);
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PlayerSelect.aspx");
        }

        protected void grdPlayerBids_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPlayerBids.PageIndex = e.NewPageIndex;
            grdPlayerBids.DataBind();
        }

        protected void btnMatch_Click(object sender, EventArgs e)
        {
            Contracts.match(int.Parse(Request.QueryString.Get("Id")));
        }
    }
}