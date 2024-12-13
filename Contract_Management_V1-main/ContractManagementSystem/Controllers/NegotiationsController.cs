using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManagementSystem.Controllers
{
    public class NegotiationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NegotiationsController> _logger;

        public NegotiationsController(ApplicationDbContext context, ILogger<NegotiationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index() // returns a list of negotiators
        {
            var negotiations = await _context.Negotiations.ToListAsync();
            return View(negotiations);
        }

        public IActionResult AddNegotiator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNegotiator(Negotiations obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingNegotiator = await _context.Negotiations.FirstOrDefaultAsync(d => d.Name == obj.Name);
                    if (existingNegotiator != null)
                    {
                        ModelState.AddModelError("", "Negotiator already exists");
                        return View(obj);
                    }

                    var negotiator = new Negotiations
                    {
                        Name = obj.Name,
                        Comments = obj.Comments,
                        Email = obj.Email,
                        Contact = obj.Contact
                    };

                    await _context.Negotiations.AddAsync(negotiator);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Negotiator {Name} added successfully.", obj.Name);
                    return RedirectToAction("NegotiatorSuccess");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding negotiator {Name}.", obj.Name);
                    ModelState.AddModelError("", "An error occurred while adding the negotiator.");
                }
            }

            return View(obj);
        }

        public IActionResult DeleteNegotiator(int id)
        {
            var negotiator = _context.Negotiations.Find(id);
            if (negotiator == null)
            {
                return NotFound();
            }
            return View(negotiator);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNegotiatorConfirmed(int id)
        {
            try
            {
                var negotiator = await _context.Negotiations.FindAsync(id);
                if (negotiator == null)
                {
                    return NotFound();
                }

                _context.Negotiations.Remove(negotiator);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Negotiator {Name} deleted successfully.", negotiator.Name);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting negotiator with ID {Id}.", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        public IActionResult NegotiatorSuccess()
        {
            return View();
        }
    }
}
