using CMS.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace CMS.Infrastructure
{
    // EmailService provides functionality to send emails using SMTP
    public class EmailService : IEmailService
    {
        // Configuration object to access app settings
        private readonly IConfiguration _configuration;

        // Constructor to inject IConfiguration dependency
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Method to send an email with an optional attachment
        public async Task SendEmailAsync(string email, string subject, string message, byte[] attachmentData, string attachmentName)
        {
            // Retrieve SMTP settings from configuration
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
            var smtpClient = new SmtpClient
            {
                Host = _configuration["EmailSettings:Host"],
                Port = smtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"])
            };

            // Create a new MailMessage object
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:Username"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            // If there is an attachment, add it to the email
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

        public Task SendEmailAsync(string? email, string v, string message)
        {
            throw new NotImplementedException();
        }

       
    }
}
