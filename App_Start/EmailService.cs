using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace demomvc.App_Start
{
	public class EmailService
	{
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("YOUR_EMAIL@gmail.com", "APP_PASSWORD"),
                EnableSsl = true
            };

            var mail = new MailMessage("YOUR_EMAIL@gmail.com", toEmail, subject, body);
            mail.IsBodyHtml = true;

            await smtp.SendMailAsync(mail);
        }
    }
}