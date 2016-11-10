using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CanamLiveFA
{
    public partial class Commisioner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblFileError.Visible = false;
            plcError.Visible = false;
            lblSigned.Visible = false;
            if (!IsPostBack && BLL.CommonFunctions.GetApplicationValue("Block End Time") != null)
            {
                txtBlockEndTime.Text = BLL.CommonFunctions.GetApplicationValue("Block End Time").ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBlockEndTime.Text))
                BLL.CommonFunctions.SetApplicationValue("Block End Time", txtBlockEndTime.Text);
            else if (BLL.CommonFunctions.GetApplicationValue("Block End Time") != null)
                BLL.CommonFunctions.RemoveApplicationValue("Block End Time");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PlayerSelect.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fupFreeAgents.HasFile)
            {
                string uploadedFileName = fupFreeAgents.PostedFile.FileName;
                string fileExtension = System.IO.Path.GetExtension(uploadedFileName).ToLower();
                if (fileExtension == ".csv")
                {
                    fupFreeAgents.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + uploadedFileName));
                    string filePath = Server.MapPath("~/Uploads/") + uploadedFileName;
                    DAL.Player.UploadFreeAgents(filePath);
                    lblFileError.Text = "File Successfully Uploaded";
                    lblFileError.Visible = true;
                }
                else
                {
                    lblFileError.Visible = true;
                    lblFileError.Text = "FileExtension must be .csv";
                }
            }
            else
            {
                lblFileError.Visible = true;
                lblFileError.Text = "Error: Upload A File";
            }
        }

        protected void btnSignPlayers_Click(object sender, EventArgs e)
        {
            DAL.Player.SignHighPlayers();
            lblSigned.Visible = true;
        }
    }
}