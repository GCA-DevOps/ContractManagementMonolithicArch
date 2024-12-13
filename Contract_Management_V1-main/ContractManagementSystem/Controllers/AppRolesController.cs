/*using ContractManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContractManagementSystem.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ContractManagementSystem.Controllers
{
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }

        //LIST ALL ROLES
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();

            roles = roles.OrderBy(r => r.Name != "Admin").ThenBy(r => r.Name).ToList();

            // Project the roles into RoleViewModel using LINQ to Objects
            var roleViewModels = roles.Select((role, index) => new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                CustomId = $"Role {index + 1}"
            }).ToList();

            // Debugging statement
            System.Diagnostics.Debug.WriteLine($"Retrieved {roles.Count()} roles.");
            return View(roleViewModels);
        }

        //create role
        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromForm] string Name)
        {
            // Avoid duplicate
            if (!_roleManager.RoleExistsAsync(Name).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Name));
                return Ok(); // Return a success status code
            }
            return BadRequest("Role Already exists"); // Return a bad request status code if the role already exists
        }


        // Action method to handle the form submission for deleting an existing role.
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var permanentRoles = new string[] { "Admin", "Approver", "User Company", "User Individual" };
                if (permanentRoles.Contains(role.Name))
                {
                    return BadRequest("Permanent roles cannot be deleted.");
                }

                await _roleManager.DeleteAsync(role);
                return Ok(); // Return a success status code
            }
            else
            {
                return NotFound(); // Return a 404 status code if the role is not found
            }
        }

        //Action method to handle editing a role
        [HttpPost]
        public async Task<IActionResult> EditRole([FromForm] string Id, [FromForm] string Name)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                var permanentRoles = new string[] { "Admin", "Approver", "User Company", "User Individual" };
                if (permanentRoles.Contains(role.Name))
                {
                    return BadRequest("Permanent roles cannot be edited.");
                }

                role.Name = Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(); // Return a success status code
                }
                else
                {
                    return BadRequest("Failed to update role"); // Return a bad request status code if the update fails
                }
            }
            else
            {
                return NotFound(); // Return a 404 status code if the role is not found
            }
        }

        public IActionResult ManageRoles()
        {

            return RedirectToAction("Index"); //redirects to the index page where you can view the roles//
        }

    }
}
*/