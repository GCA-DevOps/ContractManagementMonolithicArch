using ContractManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class CollaborationController : Controller
{
    private readonly ICollaborationService _collaborationService;
    private readonly IEmailService _emailService;

    public CollaborationController(ICollaborationService collaborationService, IEmailService emailService)
    {
        _collaborationService = collaborationService;
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCollaborator(int contractId, string email, int permissionLevel, bool notifyByEmail, string message)
    {
        // Add collaborator logic
        await _collaborationService.AddCollaboratorAsync(contractId, email, permissionLevel);

        if (notifyByEmail)
        {
            var subject = "Contract Collaboration";
            var body = $"You have been added as a collaborator for contract ID {contractId}. {message}";
            await _emailService.SendEmailAsync(email, subject, body);
        }

        return RedirectToAction("Index", "Contract");
    }
}
