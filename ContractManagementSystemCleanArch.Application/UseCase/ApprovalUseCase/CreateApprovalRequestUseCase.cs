using CMS.Application.DTOs.Request.Approval;
using CMS.Application.DTOs.Response.Approval;
using CMS.Application.Interfaces.ApprovalInterfaces;

namespace CMS.Application.UseCase.ApprovalUseCase
{
    public class CreateApprovalRequestUseCase
    {
        private readonly IApprovalWorkflowService _approvalWorkflowService;

        public CreateApprovalRequestUseCase(IApprovalWorkflowService approvalWorkflowService)
        {
            _approvalWorkflowService = approvalWorkflowService;
        }

        public async Task<CreateApprovalResponseDTO> Execute(CreateApprovalRequestDTO request)
        {
            try
            {
                if (request.IsSequential)
                {
                    var sequentialRequest = new InitiateSequentialApprovalRequestDTO
                    {
                        //ContractId = /* Your logic to determine the contract ID */,
                        ApproverIdsInOrder = request.ApproverIdsInOrder
                    };

                    var response = await _approvalWorkflowService.InitiateSequentialApprovalAsync(sequentialRequest);
                    return new CreateApprovalResponseDTO { Success = true, Message = "Sequential approval initiated successfully."};
                }
                else
                {
                    var parallelRequest = new InitiateParallelApprovalRequestDTO
                    {
                        ApproverIds = request.ApproverIdsInOrder
                    };
                    var response = await _approvalWorkflowService.InitiateParallelApprovalAsync(parallelRequest);
                    return new CreateApprovalResponseDTO { Success = true, Message = "Parallel approval initiated successfully."};
                }
            }
            catch (Exception ex)
            {
                return new CreateApprovalResponseDTO { Success = false, Message = $"Failed to initiate approval process: {ex.Message}" };
            }
        }
    }
}
