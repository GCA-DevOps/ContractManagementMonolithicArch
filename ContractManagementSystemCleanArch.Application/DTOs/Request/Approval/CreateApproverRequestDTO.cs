
using CMS.Domain.Entities.Authentication;

namespace CMS.Application.DTOs.Request.Approval
{
    public class CreateApproverRequestDTO 
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}
