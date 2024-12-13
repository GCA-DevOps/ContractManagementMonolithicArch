using ContractManagementSystem.Services;
using ContractManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ContractManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollaborationController : ControllerBase
    {
        private readonly ICollaborationService _collaborationService;
        private readonly IEmailService _emailService;
        private readonly ILogger<CollaborationController> _logger;

        public CollaborationController(
            ICollaborationService collaborationService, 
            IEmailService emailService,
            ILogger<CollaborationController> logger)
        {
            _collaborationService = collaborationService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCollaborator([FromBody] AddCollaboratorRequest request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for AddCollaborator request");
                return BadRequest(ModelState);
            }

            try
            {
                await _collaborationService.AddCollaboratorAsync(request.ContractId, request.Email, request.PermissionLevel);

                if (request.NotifyByEmail)
                {
                    var subject = "Contract Collaboration";
                    var body = $"You have been added as a collaborator for contract ID {request.ContractId}. {request.Message}";
                    await _emailService.SendEmailAsync(request.Email, subject, body);
                }

                _logger.LogInformation("Collaborator added successfully for contract ID {ContractId}", request.ContractId);
                return Ok(new { message = "Collaborator added successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a collaborator");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
