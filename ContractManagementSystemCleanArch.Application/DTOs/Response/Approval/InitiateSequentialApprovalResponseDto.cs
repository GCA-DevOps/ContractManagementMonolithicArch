

namespace CMS.Application.DTOs.Response.Approval
{
   public class InitiateSequentialApprovalResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<int> ApproverIdsInOrder { get; set; }
    }
}
