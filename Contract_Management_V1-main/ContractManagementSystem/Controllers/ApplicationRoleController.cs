using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ContractManagementSystem.Models;
using ContractManagementSystem.Services;
using Microsoft.Extensions.Logging;
using ContractManagementSystem.Data;

namespace ContractManagementSystem.Controllers
{
    public class ApplicationRoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRoleService _roleService;
        private readonly IPrivilegeService _privilegeService;
        private readonly ILogger<ApplicationRoleController> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationRoleController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<AppUser> userManager,
            IRoleService roleService,
            IPrivilegeService privilegeService,
            ILogger<ApplicationRoleController> logger,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleService = roleService;
            _privilegeService = privilegeService;
            _logger = logger;
            _context = context;
        }

        // GET: ApplicationRole
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // GET: ApplicationRole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(role);
        }

        // GET: ApplicationRole/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: ApplicationRole/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] ApplicationRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var roleToUpdate = await _roleManager.FindByIdAsync(id);
                if (roleToUpdate == null)
                {
                    return NotFound();
                }

                roleToUpdate.Name = role.Name;
                var result = await _roleManager.UpdateAsync(roleToUpdate);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(role);
        }

        // GET: ApplicationRole/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: ApplicationRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ApplicationRole/ManagePrivileges/5
        public async Task<IActionResult> ManagePrivileges(string id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Role ID is null.");
                    return NotFound("Role ID is null.");
                }

                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    _logger.LogError($"Role not found for ID: {id}");
                    return NotFound($"Role not found for ID: {id}");
                }

                var allPrivileges = await _privilegeService.GetAllPrivilegesAsync();
                var rolePrivileges = _context.RolePrivileges
                                             .Where(rp => rp.RoleId == id)
                                             .Select(rp => rp.PrivilegeId)
                                             .ToList();

                var model = new ManagePrivilegesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Privileges = allPrivileges.Select(p => new PrivilegeViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Assigned = rolePrivileges.Contains(p.Id)
                    }).ToList()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while managing privileges for Role ID: {RoleId}", id);
                ModelState.AddModelError(string.Empty, $"An error occurred while managing privileges: {ex.Message}"); // Include the exception message temporarily for debugging
                return View(new ManagePrivilegesViewModel());
            }
        }


        // POST: ApplicationRole/ManagePrivileges/5
        [HttpPost]
        public async Task<IActionResult> ManagePrivileges(ManagePrivilegesViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Model state is invalid");
                    return View(model);
                }

                _logger.LogInformation("Received data: RoleId={RoleId}, Privileges={Privileges}", model.RoleId, string.Join(", ", model.Privileges.Select(p => $"{p.Id}:{p.Assigned}")));

                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    return NotFound();
                }

                // Remove existing role privileges
                var existingRolePrivileges = _context.RolePrivileges.Where(rp => rp.RoleId == model.RoleId);
                _context.RolePrivileges.RemoveRange(existingRolePrivileges);

                // Assign new privileges
                var selectedPrivileges = model.Privileges.Where(p => p.Assigned).Select(p => p.Id).ToList();
                foreach (var privilegeId in selectedPrivileges)
                {
                    var rolePrivilege = new RolePrivilege
                    {
                        RoleId = model.RoleId,
                        PrivilegeId = privilegeId
                    };
                    _context.RolePrivileges.Add(rolePrivilege);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(RoleDetails), new { id = model.RoleId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while managing privileges for Role ID: {RoleId}", model.RoleId);
                ModelState.AddModelError(string.Empty, "An error occurred while managing privileges. Please try again later.");
                return View(model);
            }
        }


        // GET: ApplicationRole/RoleDetails/5
        public async Task<IActionResult> RoleDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var privileges = _context.RolePrivileges
                                     .Where(rp => rp.RoleId == id)
                                     .Select(rp => rp.Privilege)
                                     .ToList();

            var model = new RoleDetailsViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Privileges = privileges
            };

            return View(model);
        }
    }
}
