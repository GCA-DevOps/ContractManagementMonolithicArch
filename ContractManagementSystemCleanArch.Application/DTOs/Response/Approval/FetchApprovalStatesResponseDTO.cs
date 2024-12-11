

using CMS.Domain.Entities.Approval;

namespace CMS.Application.DTOs.Response.Approval
{
    public class FetchApprovalStatesResponseDTO
    {
        public List<ApprovalResult> ApprovalResults { get; set; }
    }
}
