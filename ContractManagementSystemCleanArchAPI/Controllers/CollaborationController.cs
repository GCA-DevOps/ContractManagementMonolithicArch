using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.CollaborationInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaborationController : ControllerBase
    {
        private readonly ICollaborationService _collaborationService;
        private readonly ILogger<CollaborationController> _logger;

        public CollaborationController(ICollaborationService collaborationService, ILogger<CollaborationController> logger)
        {
            _collaborationService = collaborationService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollaborator(int id)
        {
            var collaboration = await _collaborationService.GetCollaboratorByIdAsync(id);
            if (collaboration == null)
            {
                return NotFound();
            }
            return Ok(collaboration);
        }

        [HttpPost]
        public async Task<IActionResult> AddCollaborator([FromForm] CollaborationDto collaborationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _collaborationService.AddCollaboratorAsync(collaborationDto);
                if (result)
                {
                    return Ok("Collaborator added successfully");
                }
                else
                {
                    return BadRequest("Error adding Collaborator.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding Collaborator: {Message}", ex.Message);
                return BadRequest(new { message = "Error adding Collaborator.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollaborator(int id)
        {
            var collaborator = await _collaborationService.GetCollaboratorByIdAsync(id);
            if (collaborator == null)
            {
                return NotFound();
            }

            await _collaborationService.RemoveCollaboratorAsync(collaborator);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollaborator(int id, [FromBody] CollaborationDto collaborationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var collaborator = await _collaborationService.GetCollaboratorByIdAsync(id);
            if (collaborator == null)
            {
                return NotFound();
            }

            try
            {
                await _collaborationService.UpdateCollaborationAsync(collaborationDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating collaborator");
                return BadRequest("Error updating collaborator");
            }
        }

        [HttpPost("{contractId}/share")]
        public async Task<IActionResult> ShareContract(int contractId, [FromForm] string documentPath)
        {
            _logger.LogInformation("Sharing contract {ContractId} document located at {DocumentPath}", contractId, documentPath);
            try
            {
                await _collaborationService.ShareContractAsync(contractId, documentPath);
                _logger.LogInformation("Successfully shared contract {ContractId} document", contractId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sharing contract {ContractId} document", contractId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while sharing contract document.");
            }
        }
    }
}
