using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
            private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager; 
        private readonly SignInManager<AppUser> _signInManager;
        public EmployeeController(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

       /* [HttpGet]
        public IActionResult CreateEmployee() {
            var model = new Employee()
            {

                Roles=_context.Roles.ToList().Select(r=>new SelectListItem
                {
                    Value = r.Id,
                    Text= r.Name,
                }),
            };
            
            return View(model);
            
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee model)
        {

            return View();
        }*/

        public IActionResult CreateEmployee() { 
        
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateEmployee(EmployeeRegisterVm model)
        {
            if (ModelState.IsValid)
            {
                var employee = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    AlternativePhoneNumber = model.PhoneNumber,
                    KraPin = model.KraPin
                };
                var result = await _userManager.CreateAsync(employee);
                
                if (result.Succeeded)
                {
                   // await _signInManager.SignInAsync(employee, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid");

            }

            return View(model);
        }




    }
}
