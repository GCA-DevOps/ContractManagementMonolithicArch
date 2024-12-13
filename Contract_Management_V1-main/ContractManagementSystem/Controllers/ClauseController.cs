using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Services;
using ContractManagementSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManagementSystem.Controllers
{
    public class ClauseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ClauseService _clauseService;

        public ClauseController(UserManager<AppUser> userManager, ApplicationDbContext db, ClauseService clauseService)
        {
            _userManager = userManager;
            _context = db;
            _clauseService = clauseService;
        }

        public IActionResult Index()
        {
            var clauses = _context.ClauseLibrary.ToList();
            return View(clauses);
        }

        [HttpGet]
        public IActionResult ClauseLibrary()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ClauseLibrary(ClauseLibraryVM clauseLibraryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(clauseLibraryVM);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userName = user?.UserName;
            var userId = user?.Id;

            byte[] docbyte = await ProcessFile(clauseLibraryVM.File);
            string base64file = Convert.ToBase64String(docbyte);

            var clauseLibrary = new ClauseLibrary
            {
                Name = clauseLibraryVM.Name,
                ClauseType = clauseLibraryVM.ClauseType,
                Data = base64file,
                AppUserId = userId,
                UserName = userName,
                DateModified = DateTime.Now,
                
            };

            _context.Add(clauseLibrary);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _clauseService.DeleteClause(id);
            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Error deleting clause" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Duplicate(int id)
        {
            var clause = _context.ClauseLibrary.Find(id);
            if (clause == null)
            {
                return Json(new { success = false, message = "Clause not found" });
            }

            var newClause = new ClauseLibrary
            {
                Name = clause.Name,
                ClauseType = clause.ClauseType,
                UserName = clause.UserName,
                AppUserId = clause.AppUserId,
                DateModified = DateTime.Now,
                Data = clause.Data,
                
            };

            _context.ClauseLibrary.Add(newClause);
            _context.SaveChanges();

            return Json(new
            {
                success = true,
                newId = newClause.Id,
                newName = newClause.Name,
                newClauseType = newClause.ClauseType,
                newUserName = newClause.UserName,
                newDateModified = newClause.DateModified.ToShortDateString()
            });
        }

        [HttpGet]
        public IActionResult ViewDocument(int id)
        {
            var clause = _context.ClauseLibrary.Find(id);
            if (clause == null)
            {
                return NotFound();
            }

            var documentBytes = Convert.FromBase64String(clause.Data);
            return File(documentBytes, "application/pdf", $"{clause.Name}.pdf"); // Adjust MIME type if necessary
        }

        [HttpGet]
        public IActionResult EditClause(int id)
        {
            var clause = _context.ClauseLibrary.Find(id);
            if (clause == null)
            {
                return NotFound();
            }

            var viewModel = new ClauseLibraryVM
            {
                Id = clause.Id,
                Name = clause.Name,
                ClauseType = clause.ClauseType,
                
                File = null // We don't need the file here, but ensure it's in the VM for other purposes
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditClause(ClauseLibraryVM clauseLibraryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(clauseLibraryVM);
            }

            var clause = _context.ClauseLibrary.Find(clauseLibraryVM.Id);
            if (clause == null)
            {
                return NotFound();
            }

            clause.Name = clauseLibraryVM.Name;
            clause.ClauseType = clauseLibraryVM.ClauseType;
            

            if (clauseLibraryVM.File != null)
            {
                byte[] docbyte = await ProcessFile(clauseLibraryVM.File);
                string base64file = Convert.ToBase64String(docbyte);
                clause.Data = base64file; // Update the file data
            }

            clause.DateModified = DateTime.Now;

            _context.Update(clause);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetContent(int id)
        {
            var clause = _context.ClauseLibrary.Find(id);
            if (clause == null)
            {
                return Json(new { success = false, message = "Clause not found" });
            }

            return Json(new { success = true });
        }

        private async Task<byte[]> ProcessFile(IFormFile file)
        {
            byte[] documentBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                documentBytes = memoryStream.ToArray();
            }
            return documentBytes;
        }
    }
}
