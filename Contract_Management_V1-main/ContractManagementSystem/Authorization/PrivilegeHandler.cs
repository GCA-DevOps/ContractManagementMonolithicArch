using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContractManagementSystem.Models;
using ContractManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ContractManagementSystem.Authorization
{
    public class PrivilegeHandler : AuthorizationHandler<PrivilegeRequirement>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPrivilegeService _privilegeService;
        private readonly ILogger<PrivilegeHandler> _logger;

        public PrivilegeHandler(
            UserManager<AppUser> userManager,
            IPrivilegeService privilegeService,
            ILogger<PrivilegeHandler> logger)
        {
            _userManager = userManager;
            _privilegeService = privilegeService;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PrivilegeRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);
            if (user == null)
            {
                _logger.LogWarning("User is not authenticated.");
                return;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Any())
            {
                _logger.LogWarning("User has no roles assigned.");
                return;
            }

            var privileges = await _privilegeService.GetPrivilegesByRolesAsync(userRoles);
            if (privileges.Contains(requirement.PrivilegeName))
            {
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning($"User does not have the required privilege: {requirement.PrivilegeName}");
            }
        }
    }
}
