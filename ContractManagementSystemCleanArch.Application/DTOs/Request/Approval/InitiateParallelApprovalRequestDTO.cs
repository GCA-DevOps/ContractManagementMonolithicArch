

namespace CMS.Application.DTOs.Request.Approval
{
    public class InitiateParallelApprovalRequestDTO
    {
        public int ContractId { get; set; }
        public List<int> ApproverIds { get; set; }
    }
}
