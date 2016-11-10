using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CanamLiveFA
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                lblError.Text = "Error: Enter UserName";
                plcError.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Error: Enter Password";
                plcError.Visible = true;
            }
            else if (BLL.User.LoginUser(txtUserName.Text, txtPassword.Text))
                Response.Redirect("~/PlayerSelect.aspx");
            else
            {
                lblError.Text = "Your username and password do not match";
                plcError.Visible = true;
            }
            
        }
    }
}