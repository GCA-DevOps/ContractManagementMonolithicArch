using CMS.Application.DTOs.Request.Approval;
using CMS.Application.DTOs.Response.Approval;
using CMS.Application.Interfaces.ApprovalInterfaces;
using CMS.Domain.Entities.Approval;
using CMS.Domain.Exceptions;
using CMS.Infrastructure.Repositories;
using CMS.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalWorkflowService _approvalworkflowService;
        private readonly ILogger<ApprovalController> _logger;
        private readonly IApproverRepository _approverRepository;

        public ApprovalController(IApprovalWorkflowService approvalworkflowService, ILogger<ApprovalController> logger, IApproverRepository approverRepository)
        {
            _approvalworkflowService = approvalworkflowService;
            _logger = logger;
            _approverRepository = approverRepository;
        }

        [HttpGet("approver/{approverId}")]
        public async Task<IActionResult> GetApproverDetails(int approverId)
        {
            try
            {
                var approverDetails = await _approvalworkflowService.GetApproverDetailsAsync(approverId);
                return Ok(approverDetails);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("createapprover")]
        public async Task<IActionResult> CreateApprover([FromBody] CreateApproverRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state: {@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            try
            {
                var newApproverId = await _approvalworkflowService.CreateApproverAsync(request);

                var location = Url.Link(nameof(GetApproverDetails), new { id = newApproverId });

                return Created(location, new { id = newApproverId });
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex, "Failed to create approver due to missing user.");
                return NotFound($"User with email '{request.Email}' does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating an approver.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating an approver: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApprover(int id)
        {
            bool isDeleted = await _approverRepository.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound($"Approver with ID {id} not found.");
            }

            return Ok("Approver successfully deleted");
        }

        [HttpGet("approverslist")]
        public async Task<ActionResult<IEnumerable<ApproverDTO>>> GetAllApprovers()
        {
            var approvers = await _approvalworkflowService.GetApproversAsync();
            return Ok(approvers);
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveContract([FromBody] ApproveContractRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _approvalworkflowService.ApproveContractAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to approve contract.");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("reject")]
        public async Task<IActionResult> RejectContract([FromBody] RejectContractRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _approvalworkflowService.RejectContractAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reject contract");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("sequentialapproval")]
        public async Task<IActionResult> InitiateSequentialApproval([FromBody] InitiateSequentialApprovalRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _approvalworkflowService.InitiateSequentialApprovalAsync(request);
                return Ok(response);
            }
            catch
            {
                return StatusCode(500, "An error occurred while initiating sequential approval.");
            }
        }

        [HttpPost("parallelapproval")]
        public async Task<IActionResult> InitiateParallelApproval([FromBody] InitiateParallelApprovalRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _approvalworkflowService.InitiateParallelApprovalAsync(request);
                return Ok(response);
            }
            catch
            {
                return StatusCode(500, "An error occurred while initiating parallel approval.");
            }
        }

        [HttpPost("approvalstate")]
        public async Task<IActionResult> UpdateApproverState([FromBody] UpdateApproverStateRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updateRequest = new UpdateApproverStateRequestDTO
                {
                    ApproverId = request.ApproverId,
                    NewState = request.NewState
                };

                var result = await _approvalworkflowService.UpdateApproverStateAsync(updateRequest);

                var response = new UpdateApproverStateResponseDTO
                {
                    Success = result.Success,
                    Message = result.Message
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = "An unexpected error occurred.", error =  ex.Message});
            }
        }

       
    }
}
