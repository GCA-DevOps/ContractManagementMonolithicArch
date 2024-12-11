using CMS.Application.Interfaces.Notification;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace CMS.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;
        
        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailNotificationAsync(string recipient, string subject, string message)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:Host"], Convert.ToInt32(_configuration["EmailSettings:Port"]))
            {
                Credentials = new NetworkCredential(
                    userName: _configuration["EmailSettings:UserName"],
                    password: _configuration["EmailSettings:Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage("CMS@gmail.com", recipient)
            {
                Subject = subject,
                Body = message
            };

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }

        public Task SendSmsNotificationAsync(string recipient, string message)
        {
            throw new NotImplementedException();
        }
    }
}
