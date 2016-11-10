using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CanamLiveFA
{
    public partial class PlayerSelect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DO.User userObj = (DO.User)BLL.CommonFunctions.GetSessionValue("User");

            if (BLL.CommonFunctions.GetApplicationValue("Block End Time") == null)
            {
                grdUnsignedPlayers.DataSource = DAL.Player.GetAllPlayersByTeam(userObj);
                grdUnsignedPlayers.DataBind();
            }
            else
            {
                grdUnsignedPlayers.DataSource = DAL.Player.GetAllUnsignedPlayers();
                grdUnsignedPlayers.DataBind();
                DataTable signedPlayers = DAL.Player.GetAllSignedPlayers();
                if (signedPlayers.Rows.Count > 0)
                {
                    plcSignedPlayers.Visible = true;
                    grdSignedPlayers.DataSource = signedPlayers;
                    grdSignedPlayers.DataBind();
                }
            }
        }

        protected void grdUnsignedPlayers_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = DAL.Player.GetAllUnsignedPlayers();

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdUnsignedPlayers.DataSource = dt;
                grdUnsignedPlayers.DataBind();
            }
        }

        protected void grdSignedPlayers_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = DAL.Player.GetAllSignedPlayers();

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                grdSignedPlayers.DataSource = dt;
                grdSignedPlayers.DataBind();
            }
        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void grdSignedPlayers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSignedPlayers.PageIndex = e.NewPageIndex;
            grdUnsignedPlayers.DataBind();
        }

        protected void grdUnsignedPlayers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUnsignedPlayers.PageIndex = e.NewPageIndex;
            grdUnsignedPlayers.DataBind();
        }
    }
}