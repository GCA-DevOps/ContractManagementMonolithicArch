
using CMS.Domain.Entities.Approval;

namespace CMS.Application.DTOs.Request.Approval
{
    public class UpdateApproverStateRequestDTO
    {
        public int ApproverId { get; set; }
        public ApprovalState NewState { get; set; }
    }
}
