using CMS.Application.DTOs.Request.Approval;
using CMS.Application.DTOs.Response.Approval;
using CMS.Application.Interfaces.ApprovalInterfaces;
using CMS.Domain.Entities.Approval;

namespace CMS.Application.UseCase.ApprovalUseCase
{
    public class RejectContractUseCase
    {
        private readonly IApprovalWorkflowService _approvalworkflowService;

        public RejectContractUseCase(IApprovalWorkflowService approvalworkflowService)
        {
            _approvalworkflowService = approvalworkflowService;
        }
        public async Task<UpdateApproverStateResponseDTO> Execute(RejectContractRequestDTO request)
        {
            return await _approvalworkflowService.UpdateApproverStateAsync(new UpdateApproverStateRequestDTO
            {
                ApproverId = request.ApproverId,
                NewState = ApprovalState.Rejected

            });
        }
    }
}
