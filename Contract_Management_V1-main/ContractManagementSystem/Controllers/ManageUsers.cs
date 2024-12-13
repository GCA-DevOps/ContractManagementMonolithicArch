using Microsoft.AspNetCore.Mvc;

namespace ContractManagementSystem.Controllers
{
    public class ManageUsers : Controller
    {
        public IActionResult ManageEmployee()
        {
            return View();
        }
        public IActionResult ManageVendorIndividual()
        {
            return View();
        }
        public IActionResult ManageVendorCompany()
        {
            return View();
        }
    }
}

