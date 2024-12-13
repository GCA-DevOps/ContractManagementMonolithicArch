using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> signinManager;
        private readonly UserManager<AppUser> userManager;
        public LoginController(SignInManager<AppUser> signinManager, UserManager<AppUser> userManager) //dependency injection
        {

            this.signinManager = signinManager;
            this.userManager = userManager;

        }


        public IActionResult Login()
        {
            return View();
        }




    }
}