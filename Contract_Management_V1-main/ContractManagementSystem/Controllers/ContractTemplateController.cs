using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;
using X.PagedList;
using ContractManagementSystem.Helpers;

namespace ContractManagementSystem.Controllers
{
    public class ContractTemplateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractTemplateController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index(int? page)
        {
            int pageSize = 5; // Number of items per page
            int pageNumber = page ?? 1; // Default to first page
            int totalItems = _context.ContractTemplates.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var templates = _context.ContractTemplates
                .OrderBy(t => t.CreatedDate) // Optionally sort
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Pagination = PaginationHelper.GeneratePaginationLinks(pageNumber, totalPages, Url.Action("Index"));

            return View(templates);
        }

        public IActionResult ContractTemplates()
        {
            return View();
        }

    // POST: ContractTemplate/Upload
    [HttpPost]
        public async Task<IActionResult> UploadTemplate(IFormFile file, string description, string createdBy)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/templates", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var template = new ContractTemplate
                {
                    Name = Path.GetFileNameWithoutExtension(file.FileName),
                    Description = description,
                    CreatedDate = DateTime.Now,
                    CreatedBy = createdBy,
                    FilePath = filePath
                };

                _context.ContractTemplates.Add(template);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // GET: ContractTemplate/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var template = await _context.ContractTemplates.FindAsync(id);
            if (template != null)
            {
                System.IO.File.Delete(template.FilePath);
                _context.ContractTemplates.Remove(template);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: ContractTemplate/Duplicate/5
        public async Task<IActionResult> Duplicate(int id)
        {
            var template = await _context.ContractTemplates.FindAsync(id);
            if (template != null)
            {
                var newTemplate = new ContractTemplate
                {
                    Name = template.Name + " - Copy",
                    Description = template.Description,
                    CreatedDate = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    FilePath = template.FilePath // Consider copying the file to a new location
                };

                _context.ContractTemplates.Add(newTemplate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: ContractTemplate/Export/5
        public IActionResult Export(int id)
        {
            var template = _context.ContractTemplates.Find(id);
            if (template != null)
            {
                // Check if the file exists before reading it
                if (System.IO.File.Exists(template.FilePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(template.FilePath);
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{template.Name}.docx");
                }
                else
                {
                    // Handle the case where the file is not found
                    return NotFound($"The file {template.Name}.docx was not found on the server.");
                }
            }
            return NotFound("Template not found.");
        }

    }
}
