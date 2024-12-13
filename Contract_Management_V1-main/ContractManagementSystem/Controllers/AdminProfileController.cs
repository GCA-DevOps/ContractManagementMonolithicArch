using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContractManagementSystem.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContractManagementSystem.Data;
using ContractManagementSystem.ViewModels;

namespace ContractManagementSystem.Controllers
{
    public class AdminProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminProfileController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: AdminProfile/Edit




        public IActionResult UpdateProfile()
        {

            return View();
        }


        // POST: AdminProfile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(AdminUpdateVM model)
        {
            if (ModelState.IsValid)
            {


                //Retrieve the current user
                var user = await _userManager.GetUserAsync(User);
                // var userId = user.Email;

                var admin = await _userManager.FindByEmailAsync(user.Email);
                //(v => v.Email == User.Identity!.Name);



                admin.FirstName = model.FirstName;

                admin.LastName = model.LastName;
                admin.EmployeeNumber = model.EmployeeNumber;
                admin.Email = model.Email;
                admin.PhoneNumber = model.PhoneNumber;
                //admin.AuthorizationLevel = model.AuthorizationLevel;
                admin.AlternativePhoneNumber = model.AlternativePhoneNumber;
                //admin.KraPin = model.KraPin;
                //admin.IdentificationNumber = model.IdentificationNumber;

                var result = await _userManager.UpdateAsync(admin);

                if (result.Succeeded)
                {
                    return RedirectToAction("ProfileUpdated");
                }
                else
                {
                    // Update operation failed
                    foreach (var error in result.Errors)
                    {
                        // Add model errors for each error
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);





        }
    }
}
