using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.Ajax.Utilities;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
//using System.Web.Mvc;

namespace NBKProject.Helpers
{
    public class Emailing
    {
        public string EmailHostDetail(string toEmail, string fromEmail, string subject, string bodyHtml, string fileName, string filePath)
        {
            try
            {
                //File Path
                //string filePath = "";
                //if (fileName == null)
                //{
                //    fileName = "";
                //}
                //if (fileName.Contains("FinalReport"))
                //{
                //    filePath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\PartyDoc\\";

                //}
                //else if (fileName.Contains("DevChecklists"))
                //{
                //    filePath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\DevChecklistPdfs\\";
                //}
                //else
                //{
                //    filePath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\Files\\Docs\\";
                //}

                //email sending code will appear here.
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();

                message.To.Add(new MailAddress(toEmail));

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = bodyHtml;

                string html = bodyHtml;
                string plain = bodyHtml;
                //Commented 12-7-2016
                //message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, new ContentType("text/html")));
                //message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plain, new ContentType("text/plain")));

                if (!String.IsNullOrEmpty(fileName))
                {

                    //DOING ATTACHMENT:
                    Attachment data =
                        new Attachment(filePath + fileName,
                            MediaTypeNames.Application.Octet);
                    // Add time stamp information for the file.
                    ContentDisposition disposition = data.ContentDisposition;
                    // Add the file attachment to this e-mail message.
                    message.Attachments.Add(data);
                }
                message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                //string hostname = "smtp.sendgrid.net";// "smtp.gmail.com";
                //client.Host = hostname;
                //client.Port = 587;
                //client.EnableSsl = true;
                //string username = "alipk3";
                //string password = "Emailsend94";

                string hostname = "mail.smtp2go.com"; //"smtp.office365.com";// "smtp.gmail.com";
                client.Host = hostname;
                client.Port = 2525;
                //client.EnableSsl = true;
                string username = "db@lehmann.ch";
                string password = "Lehmann1234";
                message.From = new MailAddress(fromEmail, "NBK");
                var basicCredentials = new System.Net.NetworkCredential(username, password);
                client.Credentials = basicCredentials;



                client.Send(message);
                return "sendt";
            }
            catch (SmtpFailedRecipientException ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
