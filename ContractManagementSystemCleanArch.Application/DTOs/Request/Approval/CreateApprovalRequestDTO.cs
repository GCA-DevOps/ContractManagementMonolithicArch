
namespace CMS.Application.DTOs.Request.Approval
{
   public class CreateApprovalRequestDTO
    {
        public List<int> ApproverIdsInOrder { get; set; }
        public bool IsSequential { get; set; }
        public string? Comment { get; set; }
    }
}
