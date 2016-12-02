using System.Net.Mail;
using System.Web;
using System.Collections.Generic;
using CanamLiveFA.Enums;

namespace CanamLiveFA.BLL
{
    public class CommonFunctions
    {
        public static string[] GetTeamEmails(int[] notifiedTeams)
        {
            Dictionary<int, string> teamEmail = new Dictionary<int, string>() {
                { (int)Team.BAN, "adamfell89@hotmail.com" },
                { (int)Team.BEA, "adamfell89@hotmail.com" },
                { (int)Team.BLA, "adamfell89@hotmail.com" },
                { (int)Team.BLU, "adamfell89@hotmail.com" },
                { (int)Team.BUL, "adamfell89@hotmail.com" },
                { (int)Team.HIG, "adamfell89@hotmail.com" },
                { (int)Team.MAD, "adamfell89@hotmail.com" },
                { (int)Team.MAR, "adamfell89@hotmail.com" },
                { (int)Team.OIL, "adamfell89@hotmail.com" },
                { (int)Team.OUT, "woodsct@hotmail.com" },
                { (int)Team.RAI, "adamfell89@hotmail.com" },
                { (int)Team.REB, "adamfell89@hotmail.com" },
                { (int)Team.SCH, "adamfell89@hotmail.com" },
                { (int)Team.SHA, "adamfell89@hotmail.com" },
                { (int)Team.SPI, "adamfell89@hotmail.com" },
                { (int)Team.STA, "alanfell2015@gmail.com" },
                { (int)Team.TON, "adamfell89@hotmail.com" },
                { (int)Team.TOR, "adamfell89@hotmail.com" }
            };
            List<string> emails = new List<string>();

            foreach (int team in notifiedTeams)
            {
                emails.Add(teamEmail[team]);
            }

            return emails.ToArray();
        }
        public static void SetSessionValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static object GetSessionValue(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static object RemoveSessionValue(string key)
        {
            return HttpContext.Current.Session[key] = null;
        }

        public static void SetApplicationValue(string key, object value)
        {
            HttpContext.Current.Application[key] = value;
        }

        public static object GetApplicationValue(string key)
        {
            return HttpContext.Current.Application[key];
        }

        public static object RemoveApplicationValue(string key)
        {
            return HttpContext.Current.Application[key] = null;
        }

        public static void MailMessage(string message, string subject, string[] toEmails)
        {
            SmtpClient emailClient = new SmtpClient();
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailClient.EnableSsl = true;
            emailClient.Host = "smtp.gmail.com";
            emailClient.Port = 587;

            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("CANAMFreeAgencySite@gmail.com", "torosoutlaws");
            emailClient.UseDefaultCredentials = false;
            emailClient.Credentials = credentials;

            MailMessage email = new MailMessage();
            email.From = new MailAddress("CANAMFreeAgencySite@gmail.com");
            foreach (string toEmail in toEmails) {
                email.Bcc.Add(new MailAddress(toEmail));
            }
            email.Subject = subject;
            email.Body = message;

            emailClient.Send(email);
        }
    }
}