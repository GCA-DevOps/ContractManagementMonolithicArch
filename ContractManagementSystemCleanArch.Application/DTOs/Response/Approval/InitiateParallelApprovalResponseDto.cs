
namespace CMS.Application.DTOs.Response.Approval
{
   public class InitiateParallelApprovalResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<int> ApproverIds { get; set; }
    }
}
