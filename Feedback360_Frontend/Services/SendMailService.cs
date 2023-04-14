using Feedback360_Frontend.IServices;
using Feedback360_Frontend.Models;
using System.Net.Mail;

namespace Feedback360_Frontend.Services
{
    public class SendMailService:ISendMailService
    {
        public void SendMail(EmailEntity emailEntity)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(emailEntity.Email);
            mail.From = new System.Net.Mail.MailAddress("rishida619@gmail.com");
            mail.Subject = emailEntity.Subject;
            mail.Body = emailEntity.Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("rishida619@gmail.com", "nfsnrngmhxiwxbod");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
