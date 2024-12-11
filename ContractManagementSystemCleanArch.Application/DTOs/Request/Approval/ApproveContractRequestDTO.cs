

using System.ComponentModel.DataAnnotations;

namespace CMS.Application.DTOs.Request.Approval
{
    public class ApproveContractRequestDTO
    {
        [Required(ErrorMessage ="ContractId is required.")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "ApproverId is required.")]
        public int ApproverId { get; set; }

        public string? Comments { get; set; }
    }
}
