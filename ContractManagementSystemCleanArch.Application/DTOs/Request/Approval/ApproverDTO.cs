using CMS.Domain.Entities.Approval;
using CMS.Domain.Entities.Authentication;

namespace CMS.Application.DTOs.Request.Approval
{
    public class ApproverDTO 
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public required string Email { get; set; }
        public ApprovalState ApprovalState { get; set; }
    }
}
