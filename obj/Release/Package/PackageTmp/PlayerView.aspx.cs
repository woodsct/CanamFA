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
            if (BLL.CommonFunctions.GetApplicationValue("Block End Time") != null && (DateTime.Parse(BLL.CommonFunctions.GetApplicationValue("Block End Time").ToString()) < DateTime.Now.AddMinutes(5)))
            {
                plcBidChart.Visible = false;
            }
            if (Request.QueryString != null && Request.QueryString.Get("Id") != null)
            {
                Player currentPlayer = DAL.Player.GetPlayer(int.Parse(Request.QueryString.Get("Id")));
                lblPlayerName.Text = currentPlayer.PlayerName;
                grdPlayerBids.DataSource = DAL.Player.GetBidsByPlayer(currentPlayer.Id);
                grdPlayerBids.DataBind();

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
            if (BLL.CommonFunctions.GetApplicationValue("Block End Time") == null || (DateTime.Parse(BLL.CommonFunctions.GetApplicationValue("Block End Time").ToString()) > DateTime.Now))
            {
                DO.User userObj = (DO.User)BLL.CommonFunctions.GetSessionValue("User");
                lblAmountError.Visible = false;
                plcError.Visible = false;
                int amountPerYear = 0;
                if (int.TryParse(txtAmountPerYear.Text, out amountPerYear))
                {
                    Player currentPlayer = DAL.Player.GetPlayer(int.Parse(Request.QueryString.Get("Id")));
                    string errorStr = string.Empty;
                    Contracts.freeAgentValueCalc(currentPlayer, int.Parse(ddlNumOfYears.SelectedValue), amountPerYear, chkNoTrade.Checked, ref errorStr, userObj, plcBidChart.Visible);
                    grdPlayerBids.DataSource = DAL.Player.GetBidsByPlayer(currentPlayer.Id);
                    grdPlayerBids.DataBind();

                    if (string.IsNullOrWhiteSpace(errorStr))
                    {
                        DAL.Bids.UpdateBidFees(userObj, currentPlayer.CurrentTeam);
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
                lblError.Text = "Unable to place bid as the block has ended";
                plcError.Visible = true;
            }
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
    }
}