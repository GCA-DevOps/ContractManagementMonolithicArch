using System.ComponentModel.DataAnnotations;

namespace CMS.Application.DTOs.Request.Approval
{
    public class RejectContractRequestDTO
    {
        [Required(ErrorMessage = "ContractId is required")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "ApproverId is required")]
        public int ApproverId { get; set; }

        [Required]
        public string? RejectionReason { get; set; }
    }
}
