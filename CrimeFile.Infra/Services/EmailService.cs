using CrimeFile.Core.Common;
using CrimeFile.Core.Services;
using System;
using System.Threading.Tasks;
using CrimeFile.Core.DTOs;
using System.Net.Mail;
using System.Net;

namespace CrimeFile.Infra.Services
{
    public class EmailService: BaseService, IEmailService
    {
        private readonly IConfigManager _configManager;
        public EmailService(IUnitOfWork unitOfWork, IConfigManager configManager) : base(unitOfWork)
        {
            _configManager = configManager;
        }

        public async Task<string> SendVerificationCode(string userEmail, int code)
        {
            try
            {
                EmailDTO email = new EmailDTO
                {
                    FromEmail = _configManager.CompanyEmail,
                    EmailPassword = _configManager.CompanyEmailPassword,
                    ToEmail = userEmail,
                    Subject = "Verification Email from Crime File Management System Website",
                    Body = @"<div style = 'text-align:center;'>
                             <h1>Welcome to our Crime File Management System</h1>
                             <h3>your verification code is: " + code.ToString() + "</h3> </div>",
                    IsBodyHtml = true,
                    CC = ""
                };
                using(MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(email.FromEmail);
                    mail.To.Add(email.ToEmail);
                    mail.Subject = email.Subject;
                    mail.Body = email.Body;
                    mail.IsBodyHtml = email.IsBodyHtml;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(email.FromEmail, email.EmailPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return ("Email has been sent successfully");
                    }

                }
            }
            catch(Exception ex)
            {
                return (ex.Message);
            }
        }
    }
}
