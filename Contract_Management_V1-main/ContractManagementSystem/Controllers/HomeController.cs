using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractManagementSystem.Controllers
{

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ContractService _contractService;

        public HomeController(ILogger<HomeController> logger, ContractService contractService)
        {
            _logger = logger;
            _contractService = contractService;
        }

        public IActionResult Index()
        {
            var counts = _contractService.GetContractCount();

            return View(counts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateContracts()
        {
            return View();
        }


        public IActionResult Reports()
        {
            return View();
        }
       

        public IActionResult UsersProfile()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
