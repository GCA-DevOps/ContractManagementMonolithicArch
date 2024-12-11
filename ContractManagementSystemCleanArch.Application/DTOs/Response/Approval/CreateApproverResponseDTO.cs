using CMS.Domain.Entities.Approval;
using CMS.Domain.Entities.Authentication;

namespace CMS.Application.DTOs.Response.Approval
{
    public class CreateApproverResponseDTO
    {
        public int Id { get; set; }
        public AppUser? UserId { get; set; }
        public int? OrderNumber { get; set; } 
        public ApprovalState ApprovalState { get; set; }

    }
}
