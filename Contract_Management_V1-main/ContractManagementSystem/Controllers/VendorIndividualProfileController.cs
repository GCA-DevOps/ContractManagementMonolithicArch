using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ContractManagementSystem.Models;
using ContractManagementSystem.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ContractManagementSystem.Data;

namespace ContractManagementSystem.Controllers
{
    public class VendorIndividualProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public VendorIndividualProfileController(UserManager<AppUser> userManager, ApplicationDbContext context)
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
        public async Task<IActionResult> UpdateProfile(VendorIndividualProfileVM model)
        {
            if (ModelState.IsValid)
            {


                //Retrieve the current user
                var user = await _userManager.GetUserAsync(User);
                // var userId = user.Email;

                var vendor = await _userManager.FindByEmailAsync(user.Email);
                //(v => v.Email == User.Identity!.Name);



                vendor.FirstName = model.FirstName;

                vendor.LastName = model.LastName;
                vendor.OtherName = model.OtherName;
                vendor.Email = model.Email;
                vendor.PhoneNumber = model.PhoneNumber;
                vendor.NhifNumber = model.NhifNumber;
                vendor.NssfNumber = model.NssfNumber;
                vendor.KraPin = model.KraPin;
                vendor.IdentificationNumber = model.IdentificationNumber;

                var result = await _userManager.UpdateAsync(vendor);

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
