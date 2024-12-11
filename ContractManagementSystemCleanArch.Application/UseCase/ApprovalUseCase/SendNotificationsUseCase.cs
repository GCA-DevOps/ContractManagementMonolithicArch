using CMS.Application.DTOs.Request.Approval;
using CMS.Application.Interfaces.Notification;

namespace CMS.Application.UseCase.ApprovalUseCase
{
    public class SendNotificationsUseCase
    {
        private readonly INotificationService _notificationService;

        public SendNotificationsUseCase(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Execute(SendNotificationRequestDto request)
        {
            foreach (var recipient in request.Recipients)
            {
                if (!string.IsNullOrEmpty(recipient.Email))
                {
                    await _notificationService.SendEmailNotificationAsync(recipient.Email, request.Subject, request.Message);
                }
                if (!string.IsNullOrEmpty(recipient.PhoneNumber))
                {
                    await _notificationService.SendSmsNotificationAsync(recipient.PhoneNumber, request.Message);
                }
            }
        }
    }
}
