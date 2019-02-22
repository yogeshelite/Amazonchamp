using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Amazonweb.Models
{
    public class SendMail
    {
        public static int SendMailContact(string receiverEmailId, string subject, string userName, string userPassword)
        {
            try
            {
                string Host = string.Empty;
                string Port = string.Empty; ;
                string EnableSsl = "false";
                string UseDefaultCredentials = "false";
                string FromMailAddress = string.Empty; ;
                string FromMailerPWD = string.Empty;
                bool IsGmailSmptServer = Convert.ToBoolean(ConfigurationManager.AppSettings["IsGmailSmpt"]);
                if (IsGmailSmptServer)
                {

                    #region Gmail Setting For Mail
                    Host = ConfigurationManager.AppSettings["Host"];
                    Port = ConfigurationManager.AppSettings["Port"];
                    EnableSsl = ConfigurationManager.AppSettings["EnableSsl"];
                    UseDefaultCredentials = ConfigurationManager.AppSettings["UseDefaultCredentials"];
                    FromMailAddress = ConfigurationManager.AppSettings["FromMailAddress"];
                    FromMailerPWD = ConfigurationManager.AppSettings["FromMailerPWD"];
                    #endregion
                }
                else
                {
                    #region Godaddy Setting For Mail
                    Host = ConfigurationManager.AppSettings["HostGoD"];
                    Port = ConfigurationManager.AppSettings["PortGoD"];
                    FromMailAddress = ConfigurationManager.AppSettings["FromMailAddressGoD"];
                    FromMailerPWD = ConfigurationManager.AppSettings["FromMailerPWDGoD"];
                    #endregion
                }
                var senderEmail = new MailAddress(FromMailAddress, userName);
                var receiverEmail = new MailAddress(receiverEmailId, "Receiver");
                var password = FromMailerPWD;
                var body = "<b>Thanks For Registration</b><p> User Name Is=" + userName + "</p><p> Password Is= " + userPassword + "</p>";
                var smtp = new SmtpClient
                {
                    Host = Host,
                    Port = Convert.ToInt16(Port),
                    EnableSsl = Convert.ToBoolean(EnableSsl),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = Convert.ToBoolean(UseDefaultCredentials),
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,

                })
                {
                    smtp.Send(mess);
                }
                return 1;
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
                return 0;
                //ViewBag.Error = "Some Error";
            }
        }

    }
}