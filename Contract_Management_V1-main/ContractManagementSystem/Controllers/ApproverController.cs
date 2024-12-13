using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagementSystem.Controllers
{
    public class ApprovalController : Controller
    {
        // You can use dependency injection to inject services for interacting with the database
        private readonly ApplicationDbContext _context;

        public ApprovalController(ApplicationDbContext context)
        { 
        }

        // GET: /Approval/RegisterApprovers
        public IActionResult RegisterApprovers()
        {
            return View();
        }

        // POST: /Approval/RegisterApprovers
        [HttpPost]
        public IActionResult RegisterApprovers(AddApproversVM model)
        {
            if (ModelState.IsValid)
            {
                // Process and save the submitted data to the database
                var approvers = model.Approver.Select(approverName => new Approval
                {
                   // Name = approverName
                }).ToList();

                _context.Approval.AddRange(approvers);
                _context.SaveChanges();

                // Optionally, redirect the user to a success page
                return RedirectToAction("Success");
            }
            // If the model state is not valid, return the same view with validation errors
            return View(model);
        }

        // GET: /Approval/Success
        public IActionResult Success()
        {
            return View();
        }
    }
}
