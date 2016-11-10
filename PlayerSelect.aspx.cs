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

            grdUserPlayers.DataSource = DAL.Player.GetAllPlayersByTeam(userObj);
            grdUserPlayers.DataBind();

            grdUserPlayers.Columns[3].Visible = false;
            grdUserPlayers.Columns[4].Visible = false;

            if (BLL.CommonFunctions.GetApplicationValue("Player Reset") != null)
            {
                grdUserPlayers.Columns[3].Visible = true;
                grdUserPlayers.Columns[4].Visible = true;
                plcUnsignedPlayers.Visible = true;
                grdUnsignedPlayers.DataSource = DAL.Player.GetAllPlayers(userObj);
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
            DO.User userObj = (DO.User)BLL.CommonFunctions.GetSessionValue("User");
            DataTable dt = DAL.Player.GetAllPlayers(userObj);
            dt = (BLL.CommonFunctions.GetApplicationValue("Player Reset") == null) ?
                 DAL.Player.GetAllPlayersByTeam(userObj) : dt = DAL.Player.GetAllPlayers(userObj);

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetUnsignedSortDirection(e.SortExpression);
                grdUnsignedPlayers.DataSource = dt;
                grdUnsignedPlayers.DataBind();
            }
        }

        protected void grdSignedPlayers_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = DAL.Player.GetAllSignedPlayers();

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSignedSortDirection(e.SortExpression);
                grdSignedPlayers.DataSource = dt;
                grdSignedPlayers.DataBind();
            }
        }

        protected void grdUserPlayers_Sorting(object sender, GridViewSortEventArgs e)
        {
            DO.User userObj = (DO.User)BLL.CommonFunctions.GetSessionValue("User");
            DataTable dt = DAL.Player.GetAllPlayersByTeam(userObj);

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetUserSortDirection(e.SortExpression);
                grdUserPlayers.DataSource = dt;
                grdUserPlayers.DataBind();
            }
        }

        private string GetUnsignedSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["UnsignedSortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["UnsignedSortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            if (BLL.CommonFunctions.GetSessionValue("IndexChange") != null && bool.Parse(BLL.CommonFunctions.GetSessionValue("IndexChange").ToString()))
            {
                sortDirection = ViewState["UnsignedSortDirection"] as string;
                BLL.CommonFunctions.RemoveSessionValue("IndexChange");
            }

            ViewState["UnsignedSortDirection"] = sortDirection;
            ViewState["UnsignedSortExpression"] = column;
            return sortDirection;
        }

        private string GetSignedSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SignedSortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SignedSortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC") && BLL.CommonFunctions.GetSessionValue("IndexChange") == null)
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            if (BLL.CommonFunctions.GetSessionValue("IndexChange")!=null && bool.Parse(BLL.CommonFunctions.GetSessionValue("IndexChange").ToString()))
            {
                sortDirection = ViewState["SignedSortDirection"] as string;
                BLL.CommonFunctions.RemoveSessionValue("IndexChange");
            }

            ViewState["SignedSortDirection"] = sortDirection;
            ViewState["SignedSortExpression"] = column;
            return sortDirection;
        }

        private string GetUserSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["UserSortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["UserSortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC") && BLL.CommonFunctions.GetSessionValue("IndexChange") == null)
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            if (BLL.CommonFunctions.GetSessionValue("IndexChange") != null && bool.Parse(BLL.CommonFunctions.GetSessionValue("IndexChange").ToString()))
            {
                sortDirection = ViewState["UserSortDirection"] as string;
                BLL.CommonFunctions.RemoveSessionValue("IndexChange");
            }

            ViewState["UserSortDirection"] = sortDirection;
            ViewState["UserSortExpression"] = column;
            return sortDirection;
        }

        protected void grdSignedPlayers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSignedPlayers.PageIndex = e.NewPageIndex;
            if (ViewState["SignedSortDirection"] != null)
            {
                BLL.CommonFunctions.SetSessionValue("IndexChange", true);
                if (ViewState["SignedSortDirection"].ToString() == "ASC")
                    grdSignedPlayers.Sort(ViewState["SignedSortExpression"].ToString(), SortDirection.Ascending);
                else
                    grdSignedPlayers.Sort(ViewState["SignedSortExpression"].ToString(), SortDirection.Descending);
            }
            grdUnsignedPlayers.DataBind();
        }

        protected void grdUnsignedPlayers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUnsignedPlayers.PageIndex = e.NewPageIndex;
            if (ViewState["UnsignedSortDirection"] != null)
            {
                BLL.CommonFunctions.SetSessionValue("IndexChange", true);
                if (ViewState["UnsignedSortDirection"].ToString() == "ASC")
                    grdUnsignedPlayers.Sort(ViewState["UnsignedSortExpression"].ToString(), SortDirection.Ascending);
                else
                    grdUnsignedPlayers.Sort(ViewState["UnsignedSortExpression"].ToString(), SortDirection.Descending);
            }
            grdUnsignedPlayers.DataBind();
        }

        protected void grdUserPlayers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUserPlayers.PageIndex = e.NewPageIndex;
            if (ViewState["UserSortDirection"] != null)
            {
                BLL.CommonFunctions.SetSessionValue("IndexChange", true);
                if (ViewState["UserSortDirection"].ToString() == "ASC")
                    grdUserPlayers.Sort(ViewState["UserSortExpression"].ToString(), SortDirection.Ascending);
                else
                    grdUserPlayers.Sort(ViewState["UserSortExpression"].ToString(), SortDirection.Descending);
            }
            grdUserPlayers.DataBind();
        }
    }
}