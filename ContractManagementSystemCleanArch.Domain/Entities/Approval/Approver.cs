using CMS.Domain.Entities.Authentication;

namespace CMS.Domain.Entities.Approval
{
    public class Approver
    {
        public int ApproverId { get; set; }
        public AppUser? User { get; set; }
        public string? Email { get; set; }
        public int OrderNumber { get; set; }  
        public ApprovalState ApprovalState { get; set; }
        public string ApprovalStatus { get; set; } = string.Empty;
    }

}
