

namespace CMS.Application.DTOs.Request.Approval
{
    public class SendNotificationRequestDto
    {
        public List<RecipientInfo> Recipients { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }

    public class RecipientInfo
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }

}
