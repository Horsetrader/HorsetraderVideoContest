using System;
using System.Configuration;
using System.Net.Mail;

namespace BLL
{
    public class EmailHandler
    {
        public static bool Send(string fromAddress, string toAddress, string bccAddress, string subject, string body)
        {
            bool result = false;
            System.Net.Mail.SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["EmailHost"];
            client.Port = string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailPort"].ToString()) ? 0 :
                    Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.Mail.MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
            if (!string.IsNullOrEmpty(bccAddress))
            {
                MailAddress mailBccAddress = new MailAddress(bccAddress);
                message.Bcc.Add(mailBccAddress);
            }
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            //Send Message
            try
            {
                client.Send(message);
                result = true;
            }
            catch
            {
                result = false;

            }
            return result;
        }

        public static bool Send(string fromAddress, string toAddress, string bccAddress, string subject, string body, string attachmentFile)
        {
            bool result = false;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["EmailHost"];
            client.Port = string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailPort"].ToString()) ? 0 :
                    Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
            Attachment data = new Attachment(ConfigurationManager.AppSettings["AttachmentPath"] + attachmentFile);

            message.Attachments.Add(data);

            if (!string.IsNullOrEmpty(bccAddress))
            {
                MailAddress mailBccAddress = new MailAddress(bccAddress);
                message.Bcc.Add(mailBccAddress);
            }
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            //Send Message
            try
            {
                client.Send(message);
                result = true;
            }
            catch
            {
                result = false;

            }
            return result;
        }
    }
}
