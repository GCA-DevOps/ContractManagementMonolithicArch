using CMS.Application.DTOs.Request.Approval;
using CMS.Application.DTOs.Response.Approval;
using CMS.Domain.Entities.Approval;

namespace CMS.Application.Interfaces.ApprovalInterfaces
{
    public interface IApprovalWorkflowService
    {
        Task<RejectContractResponseDTO> RejectContractAsync(RejectContractRequestDTO request);
        Task<ApproveContractResponseDTO> ApproveContractAsync(ApproveContractRequestDTO request);
        Task<int> CreateApproverAsync(CreateApproverRequestDTO request);
        Task<ApproverDTO> GetApproverDetailsAsync(int approverId);
        Task<InitiateSequentialApprovalResponseDto> InitiateSequentialApprovalAsync(InitiateSequentialApprovalRequestDTO request);
        Task<InitiateParallelApprovalResponseDto> InitiateParallelApprovalAsync(InitiateParallelApprovalRequestDTO request);
        Task<UpdateApproverStateResponseDTO> UpdateApproverStateAsync(UpdateApproverStateRequestDTO request);
        Task<IEnumerable<ApproverDTO>> GetApproversAsync();
    }
}
