using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContractManagementSystem.Models;
using ContractManagementSystem.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContractManagementSystem.Data;

namespace ContractManagementSystem.Controllers
{

    public class VendorCompanyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public VendorCompanyController(UserManager<AppUser> userManager, ApplicationDbContext context)
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
        public async Task<IActionResult> UpdateProfile(VendorCompanyProfileVM model)
        {
            if (ModelState.IsValid)
            {


                //Retrieve the current user
                var user = await _userManager.GetUserAsync(User);
                // var userId = user.Email;

                var company = await _userManager.FindByEmailAsync(user.Email);
                //(v => v.Email == User.Identity!.Name);



                company.CompanyName = model.CompanyName;

                company.RegistrationNumber = model.RegistrationNumber;
                company.PhysicalAddress = model.PhysicalAddress;
                company.Email = model.Email;
                company.PhoneNumber = model.PhoneNumber;
                company.PostalAddress = model.PostalAddress;

                company.KraPin = model.KraPin;


                var result = await _userManager.UpdateAsync(company);

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
