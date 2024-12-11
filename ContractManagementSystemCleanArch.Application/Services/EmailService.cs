
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace C.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message, byte[] attachmentData, string attachmentName);
    }
}