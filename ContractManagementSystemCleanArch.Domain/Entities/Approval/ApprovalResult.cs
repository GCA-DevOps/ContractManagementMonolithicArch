
namespace CMS.Domain.Entities.Approval
{
    public enum ApprovalState
    {
        Pending,
        Approved,
        Rejected
    }

    public class ApprovalResult
    {
        public int ApproverId { get; set; }
        public ApprovalState State { get; set; }
    }
}
