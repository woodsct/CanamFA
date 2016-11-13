using System;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Web;

namespace CanamLiveFA
{
    public partial class Commisioner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblFileError.Visible = false;
            plcError.Visible = false;
            lblSigned.Visible = false;
            if (!IsPostBack && BLL.CommonFunctions.GetApplicationValue("Player Reset") != null)
            {
                txtPlayerEndTime.Text = BLL.CommonFunctions.GetApplicationValue("Player Reset").ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPlayerEndTime.Text))
            {
                BLL.CommonFunctions.SetApplicationValue("Player Reset", double.Parse(txtPlayerEndTime.Text));
                DAL.Player.StartFreeAgency();
            }
            else if (BLL.CommonFunctions.GetApplicationValue("Player Reset") != null)
                BLL.CommonFunctions.RemoveApplicationValue("Player Reset");
            Response.Redirect(Request.RawUrl);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PlayerSelect.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            decimal qualifyingOffer;
            if (fupFreeAgents.HasFile && decimal.TryParse(txtQualifyingOffer.Text, out qualifyingOffer))
            {
                string uploadedFileName = fupFreeAgents.PostedFile.FileName;
                string fileExtension = System.IO.Path.GetExtension(uploadedFileName).ToLower();
                if (fileExtension == ".csv")
                {
                    fupFreeAgents.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + uploadedFileName));
                    string filePath = Server.MapPath("~/Uploads/") + uploadedFileName;
                    DAL.Player.UploadFreeAgents(filePath, qualifyingOffer);
                    lblFileError.Text = "File Successfully Uploaded";
                    lblFileError.Visible = true;
                }
                else
                {
                    lblFileError.Visible = true;
                    lblFileError.Text = "FileExtension must be .csv";
                }
            }
            else if (fupFreeAgents.HasFile)
            {
                lblFileError.Visible = true;
                lblFileError.Text = "Error: Enter a qualifying offer";
            }
            else
            {
                lblFileError.Visible = true;
                lblFileError.Text = "Error: Upload A File";
            }
        }

        protected void btnSignPlayers_Click(object sender, EventArgs e)
        {
            /*DAL.Player.SignAllPlayers();
            lblSigned.Visible = true;*/

            DataTable dTable = DAL.Bids.GetFinalBids();

            StringBuilder fileContent = new StringBuilder();
            string path = Server.MapPath("/Uploads/FinalBids.csv");

            foreach (var col in dTable.Columns)
            {
                fileContent.Append(col.ToString() + ",");
            }

            fileContent.Replace(",", Environment.NewLine, fileContent.Length - 1, 1);



            foreach (DataRow dr in dTable.Rows)
            {

                foreach (var column in dr.ItemArray)
                {
                    fileContent.Append("\"" + column.ToString() + "\",");
                }

                fileContent.Replace(",", Environment.NewLine, fileContent.Length - 1, 1);
            }

            File.WriteAllText(path, fileContent.ToString());

            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=FinalBids.csv");
            byte[] data = req.DownloadData(path);
            response.BinaryWrite(data);
            response.End();
        }
    }
}