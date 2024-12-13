using System;
using System.Linq;
using System.Threading.Tasks;
using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractManagementSystem.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ApplicationDbContext _context;

        public RolesController(IRoleService roleService, ApplicationDbContext context)
        {
            _roleService = roleService;
            _context = context;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "Role name cannot be empty.");
                return View();
            }

            try
            {
                var role = await _roleService.CreateRoleAsync(roleName);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult AssignPrivilege()
        {
            ViewBag.Roles = _context.Roles.Select(r => new SelectListItem
            {
                Value = r.Id,
                Text = r.Name
            }).ToList();

            ViewBag.Privileges = _context.Privileges.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignPrivilege(string roleId, int privilegeId)
        {
            if (string.IsNullOrEmpty(roleId) || privilegeId <= 0)
            {
                ModelState.AddModelError("", "Invalid role ID or privilege ID.");
                ViewBag.Roles = _context.Roles.Select(r => new SelectListItem
                {
                    Value = r.Id,
                    Text = r.Name
                }).ToList();

                ViewBag.Privileges = _context.Privileges.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList();

                return View();
            }

            try
            {
                await _roleService.AssignPrivilegeAsync(roleId, privilegeId);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Roles = _context.Roles.Select(r => new SelectListItem
                {
                    Value = r.Id,
                    Text = r.Name
                }).ToList();

                ViewBag.Privileges = _context.Privileges.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList();

                return View();
            }
        }
    }
}
