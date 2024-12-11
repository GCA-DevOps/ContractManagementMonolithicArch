namespace CMS.Application.Interfaces.Notification
{
    public interface INotificationService
    {
        Task SendEmailNotificationAsync(string recipient, string subject, string message);
        Task SendSmsNotificationAsync(string recipient, string message);
    }
}
