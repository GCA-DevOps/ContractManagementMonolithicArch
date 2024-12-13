
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ContractManagementSystem.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, byte[] attachmentData, string attachmentName);
        Task SendEmailAsync(string? email, string v, string emailBody, string attachmentPath);
        Task SendEmailAsync(string? email, string subject, string message);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message, byte[] attachmentData, string attachmentName)
        {
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
            var smtpClient = new SmtpClient
            {
                Host = _configuration["EmailSettings:Host"],
                Port = smtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:Username"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            if (attachmentData != null && !string.IsNullOrEmpty(attachmentName))
            {
                using (var stream = new MemoryStream(attachmentData))
                {
                    stream.Position = 0; // Ensure the stream position is set to the beginning
                    var attachment = new Attachment(stream, attachmentName, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                    mailMessage.Attachments.Add(attachment);

                    // Use SendMailAsync to ensure async behavior is maintained
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            else
            {
                // If there's no attachment, just send the email
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MailMessage("from@example.com", to)
            {
                Subject = subject,
                Body = body
            };

            await _smtpClient.SendMailAsync(message);
        }

        public Task SendEmailAsync(string? email, string v, string emailBody, string attachmentPath)
        {
            throw new NotImplementedException();
        }
    }
}