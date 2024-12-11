using CMS.Application.DTOs.Request.Approval;
using CMS.Application.DTOs.Response.Approval;
using CMS.Application.Interfaces.AccountInterfaces;
using CMS.Application.Interfaces.ApprovalInterfaces;
using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Interfaces.Notification;
using CMS.Domain.Entities.Approval;
using CMS.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace CMS.Infrastructure.Services
{
    public class ApprovalWorkflowService : IApprovalWorkflowService
    {
        private readonly IApproverRepository _approverRepository;
        private readonly IContractRepository _contractRepository;
        private readonly INotificationService _notificationService;
        private readonly ICommentRepository _commentRepository;
        private readonly ILogger<ApprovalWorkflowService> _logger;
        private readonly IAccount _accountRepository;

        private static ApprovalState DetermineDecisionBasedOnComments(List<Comment> comments)
        {
            // if there are any comments, approve; otherwise, reject
            return comments.Any() ? ApprovalState.Approved : ApprovalState.Rejected;
        }

        private async Task<ApprovalResult> ProcessApprovalForApproverAsync(int approverId, int contractId)
        {
            // Simulate the approver reviewing the contract and commenting
            await Task.Delay(1000); 

            // Fetch comments for the current contract
            var comments = await _commentRepository.GetCommentsByContractIdAsync(contractId);

            // Determine the decision based on comments
            var decision = DetermineDecisionBasedOnComments(comments);

            // Return the result
            return new ApprovalResult
            {
                ApproverId = approverId,
                State = decision == ApprovalState.Approved ? ApprovalState.Approved : ApprovalState.Rejected
            };
        }

        public ApprovalWorkflowService(IApproverRepository approverRepository, IContractRepository contractRepository, ICommentRepository commentRepository, INotificationService notificationService, ILogger<ApprovalWorkflowService> logger, IAccount accountRepository)
        {
            _approverRepository = approverRepository;
            _commentRepository = commentRepository;
            _contractRepository = contractRepository;
            _notificationService = notificationService;
            _commentRepository = commentRepository;
            _logger = logger;
            _accountRepository = accountRepository;
        }

        public async Task<ApproverDTO> GetApproverDetailsAsync(int approverId)
        {
            var approver = await _approverRepository.GetByIdAsync(approverId);

            if (approver != null)
            {
                return new ApproverDTO
                {
                    Id = approver.ApproverId,
                    Email = approver.Email!,
                    ApprovalState = approver.ApprovalState
                };
            }
            return null!;
        }

        public async Task<IEnumerable<ApproverDTO>> GetApproversAsync()
        {
            return await _approverRepository.GetAllApproversAsync();
        }

        public async Task<InitiateParallelApprovalResponseDto> InitiateParallelApprovalAsync(InitiateParallelApprovalRequestDTO request)
        {
            var approvers = await _approverRepository.GetAllApproversAsync();
            var approverIds = request.ApproverIds;
            var tasks = new List<Task<ApprovalResult>>();

            // create tasks for each approver to process approval in parallel
            foreach (var approverId in approverIds)
            {
                var approver = approvers.FirstOrDefault(a => a.Id == approverId);
                if (approver != null)
                {
                    var task = ProcessApprovalForApproverAsync(approverId, request.ContractId);
                    tasks.Add(task);
                }
            }

            // Waiting for all tasks to complete
            var results = await Task.WhenAll(tasks);

            // Determine the overall success based on individual results
            var success = results.All(r => r.State == ApprovalState.Approved);
            var message = success ? "All contracts were approved." : "Some contracts were rejected.";

            return new InitiateParallelApprovalResponseDto
            {
                Success = success,
                Message = message,
                ApproverIds = approverIds
            };
        }

        public async Task<InitiateSequentialApprovalResponseDto> InitiateSequentialApprovalAsync(InitiateSequentialApprovalRequestDTO request)
        {
            var approvers = await _approverRepository.GetAllApproversAsync();
            var approverIdsInOrder = request.ApproverIdsInOrder;
            var approvedContracts = new List<int>();
            var contractComments = new Dictionary<int, List<Comment>>();

            foreach (var approverId in approverIdsInOrder)
            {
                var approver = approvers.FirstOrDefault(a => a.Id == approverId);
                if (approver != null)
                {
                    var comments = await _commentRepository.GetCommentsByContractIdAsync(request.ContractId);
                    contractComments[approverId] = comments;

                    await Task.Delay(1000);

                    var decision = DetermineDecisionBasedOnComments(comments);

                    if (decision == ApprovalState.Approved)
                    {
                        if (approvedContracts.Contains(approverId)) continue;

                        approvedContracts.Add(approverId);

                        //await _notificationService.SendEmailNotificationAsync(approver.User.Email, "Contract Approved", $"Contract ID: {request.ContractId} has been approved.");
                    }
                    else
                    {
                        await _notificationService.SendEmailNotificationAsync("contractcreator@example.com", "Contract Rejected", $"Contract ID: {request.ContractId} was rejected due to: {decision}.");
                    }
                }
            }

            return new InitiateSequentialApprovalResponseDto
            {
                Success = true,
                Message = "Sequential approval initiated.",
                ApproverIdsInOrder = approverIdsInOrder
            };
        }

        public async Task<UpdateApproverStateResponseDTO> UpdateApproverStateAsync(UpdateApproverStateRequestDTO request)
        {
            var approver = await _approverRepository.GetByIdAsync(request.ApproverId);
            
            if (approver != null)
            {
                approver.ApprovalState = request.NewState;
                await _approverRepository.UpdateAsync(approver);
                return new UpdateApproverStateResponseDTO
                {
                    Success = true,
                    Message = "Approver state updated successfully."
                };
            }
            else
            {
                return new UpdateApproverStateResponseDTO
                {
                    Success = false,
                    Message = "Approver not found."
                };
            }
        }

        public async Task<int> CreateApproverAsync(CreateApproverRequestDTO request)
        {
            var user = await _accountRepository.FindUserByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogError("Attempted to create approver for non-existent user: {Email}", request.Email);
                throw new UserNotFoundException("User does not exist.");
            }

            var approver = new Approver
            {
                Email = request.Email
            };

            var addedApprover = await _approverRepository.AddAsync(approver);

            return addedApprover.ApproverId;
        }

        public async Task<ApproveContractResponseDTO> ApproveContractAsync(ApproveContractRequestDTO request)
        {
            var contract = await _contractRepository.GetById(request.ContractId);
            var approver = await _approverRepository.GetByIdAsync(request.ApproverId);

            if (contract == null || approver == null)
            {
                _logger.LogError("Either Contract or Approver does not exist.");
                throw new Exception("Contract or Approver not found.");
            }

            await _contractRepository.UpdateContract(contract);

            await _notificationService.SendEmailNotificationAsync(approver.User.Email, "Contract Approved", $"The contract with ID {request.ContractId} has been approved.");

            return new ApproveContractResponseDTO
            {
                Message = "Contract approved successfully.",
                ContractId = request.ContractId
            };
        }

        public async Task<RejectContractResponseDTO> RejectContractAsync(RejectContractRequestDTO request)
        {
            var contract = await _contractRepository.GetById(request.ContractId);
            var approver = await _approverRepository.GetByIdAsync(request.ApproverId);

            if (contract == null || approver == null)
            {
                throw new Exception("Contract or Approver not found.");
            }

            await _contractRepository.UpdateContract(contract);


            await _notificationService.SendEmailNotificationAsync(approver.User.Email, "Contract Rejected", $"The contract with ID {request.ContractId} has been rejected.");

            return new RejectContractResponseDTO
            {
                Message = "Contract rejected successfully.",
                ContractId = request.ContractId
            };
        }

    }
}
