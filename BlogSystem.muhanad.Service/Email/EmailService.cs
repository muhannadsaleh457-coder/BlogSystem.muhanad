using BlogSystem.muhanad.Abstractions.Mails;
using BlogSystem.muhanad.Services.Auth;
using BlogSystem.muhanad.Shared.Dtos.Emails;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Services.Email
{
    public class EmailService(IOptions<EmailSettingsOptions> options) : IMailService
    {
        public async Task<bool> SendEmail(EmailDto email)
        {
            try
            {

                var client = new SmtpClient(options.Value.Host, options.Value.Port)
                {
                    Credentials = new NetworkCredential(
                options.Value.Email,
                options.Value.Password
                 ),
                    EnableSsl = true
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(options.Value.Email),
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = true
                };

                mail.To.Add(email.To);

                await client.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex) 
            {

                return false;
            }
 
        }
    }
}
