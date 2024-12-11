
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.DTOs.Request.Approval
{
   public class InitiateSequentialApprovalRequestDTO
    {
        [Required(ErrorMessage = "ContractId is required.")]
        public int ContractId { get; set; }

        [Required]
        public List<int> ApproverIdsInOrder { get; set; }
    }
}
