using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace CryptoGambling.Core.Emails
{
    public class EmailSender
    {
        public static bool SendMail(string toAddress, string subject, string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("postmaster@sandbox9d218c52aff94153affcb4f85e6ed43c.mailgun.org");
                message.To.Add(new MailAddress(toAddress));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = email;
                smtp.Port = 587;
                smtp.Host = "smtp.mailgun.org"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("postmaster@sandbox9d218c52aff94153affcb4f85e6ed43c.mailgun.org", "4e1245cfdd3dcb6b623812e4e131b42f-787e6567-0b931f1b");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
