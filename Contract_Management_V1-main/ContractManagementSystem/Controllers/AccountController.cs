using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Services;
using ContractManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContractManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<ApplicationRole> roleManager,
                                 IEmailService emailService,
                                 ILogger<AccountController> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (roles.Contains("Approver"))
                    {
                        return RedirectToAction("LandingPage", "ApproverDashboard");
                    }
                    else if (roles.Contains("User Individual"))
                    {
                        return RedirectToAction("LandingPage", "VendorIndividual");
                    }
                    else if (roles.Contains("User Company"))
                    {
                        return RedirectToAction("LandingPage", "VendorCompany");
                    }
                }
                ModelState.AddModelError("", "Invalid Password or Email");
                return View(model);
            }
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        public IActionResult CreateEmployee()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            var departments = _context.Departments.ToList();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        [Authorize(Policy = "CreateUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    AlternativePhoneNumber = model.AlternativePhoneNumber,
                    KraPin = model.KraPin
                };

                var password = GeneratePassword();
                var result = await _userManager.CreateAsync(employee, password);

                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(model.SelectedRole);
                    var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentName == model.SelectedDepartment);
                    if (role != null && department != null)
                    {
                        employee.DepartmentId = department.Id;
                        await _userManager.AddToRoleAsync(employee, role.Name);
                    }

                    var loginLink = Url.Action("Login", "Account", new { email = employee.Email, password }, Request.Scheme);

                    _logger.LogInformation($"Sending email to {model.Email}...");
                    await _emailService.SendEmailAsync(model.Email, "Your login details", $"Your password is: <b>{password}</b> Login here: <a href='{loginLink}'>link</a>", null, null);
                    _logger.LogInformation($"Email sent successfully to {model.Email}");

                    // Redirect to the login page
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If model state is invalid or creation fails, return the view with errors
            return View(model);
        }
      

        private string GeneratePassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[{]}|;:,<.>?";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);
                await _emailService.SendEmailAsync(model.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>", null, null);

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return View("Error");
            }
            var model = new ResetPasswordVm { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            var model = new ForgotPasswordConfirmationVm
            {
                Message = "If an account with the provided email exists, you will receive an email with instructions to reset your password."
            };
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            var model = new ResetPasswordConfirmationVm
            {
                Message = "Your password has been reset successfully."
            };
            return View(model);
        }

        public IActionResult CreateVendorIndividual()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View();
        }

        [Authorize(Policy = "CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateVendorIndividual(VendorIndividualRegisterVm model)
        {
            if (ModelState.IsValid)
            {
                var vendorIndividual = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    AlternativePhoneNumber = model.PhoneNumber,
                    KraPin = model.KraPin,
                    IdentificationNumber = model.IdentificationNumber,
                    NssfNumber = model.NssfNumber,
                    NhifNumber = model.NhifNumber
                };
                var password = GeneratePassword();
                var result = await _userManager.CreateAsync(vendorIndividual, password);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(model.SelectedRole);
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(vendorIndividual, role.Name);
                    }
                    var loginLink = Url.Action("Login", "Account", new { email = vendorIndividual.Email, password }, Request.Scheme);

                    _logger.LogInformation($"Sending email to {model.Email}...");
                    await _emailService.SendEmailAsync(model.Email, "Your login details", $"Your password is: <b>{password}</b> Login here: <a href='{loginLink}'>link</a>", null, null);
                    _logger.LogInformation($"Email sent successfully to {model.Email}");
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid");
            }
            return View(model);
        }

        public IActionResult CreateVendorCompany()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            return View();
        }

        [Authorize(Policy = "CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateVendorCompany(VendorCompanyRegisterVm model)
        {
            if (ModelState.IsValid)
            {
                var vendorCompany = new AppUser
                {
                    CompanyName = model.CompanyName,
                    RegistrationNumber = model.RegistrationNumber,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    AlternativePhoneNumber = model.PhoneNumber,
                    KraPin = model.KraPin,
                    PostalAddress = model.PostalAddress,
                    PhysicalAddress = model.PhysicalAddress,
                };
                var password = GeneratePassword();
                var result = await _userManager.CreateAsync(vendorCompany, password);
                if (result.Succeeded)
                {
                    var role = await _roleManager.FindByIdAsync(model.SelectedRole);
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(vendorCompany, role.Name);
                    }
                    var loginLink = Url.Action("Login", "Account", new { email = vendorCompany.Email, password }, Request.Scheme);

                    _logger.LogInformation($"Sending email to {model.Email}...");
                    await _emailService.SendEmailAsync(model.Email, "Your login details", $"Your password is: <b>{password}</b> Login here: <a href='{loginLink}'>link</a>", null, null);
                    _logger.LogInformation($"Email sent successfully to {model.Email}");
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ManageUsers()
        {
            return View();
        }
    }
}
