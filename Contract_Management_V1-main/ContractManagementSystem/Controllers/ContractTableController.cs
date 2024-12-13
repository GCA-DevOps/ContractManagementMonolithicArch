using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagementSystem.Controllers
{
    public class ContractTableController : Controller
    {

/*        private static List<Contract> contracts = new List<Contract>
        {
            new Contract { Id = 1, ContractType = "Contract 1", DocumentUrl = "/path/to/document1.pdf" },
            new Contract { Id = 2, ContractType = "Contract 2", DocumentUrl = "/path/to/document2.pdf" }
        };*/
        public IActionResult Index()
        {
            return View();
        }
    }
}
