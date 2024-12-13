using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ContractManagementSystem.Controllers
{
   // [Authorize(Roles="Admin")]
    public class AdminDashboardsController : Controller
    {
        private readonly ContractService _contractService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminDashboardsController(ContractService contractService, IWebHostEnvironment webHostEnvironment)
        {
            _contractService = contractService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult QuillEditor()
        {
            string contractTemplatesPath = Path.Combine(_webHostEnvironment.WebRootPath, "ContractTemplates");

            // Check if the ContractTemplates folder exists
            if (Directory.Exists(contractTemplatesPath))
            {
                // Get all files in the ContractTemplates folder
                string[] contractTemplateFiles = Directory.GetFiles(contractTemplatesPath);

                // Extract only the file names (without the path)
                var contractTemplates = contractTemplateFiles.Select(Path.GetFileName).ToList();

                ViewBag.ContractTemplates = contractTemplates;
            }
            else
            {
                // If the ContractTemplates folder doesn't exist, handle the case accordingly
                ViewBag.ContractTemplates = new List<string>(); // Empty list
            }

            return View();
        }

        public IActionResult Landingface()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

    }
}