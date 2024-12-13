using Microsoft.AspNetCore.Mvc;
using ContractManagementSystem.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ContractManagementSystem.Controllers
{
    public class VendorIndividual : Controller
    {
        public IActionResult Index()
        {
            var model = new VendorIndividualProfile
            {
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProfile(VendorIndividualProfile model)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, return the view with the current model to display validation errors
                return View("Index", model);
            }

            // Logic to update the profile in the database or perform other actions

            // Optionally, redirect to a success page or back to the form
            return RedirectToAction("ProfileUpdated");
        }

        public IActionResult LandingPage()
        { 
            return View();
        }
    }
}
