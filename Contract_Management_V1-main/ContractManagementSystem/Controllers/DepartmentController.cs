using Microsoft.AspNetCore.Mvc;
using ContractManagementSystem.ViewModels;
using ContractManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using ContractManagementSystem.Models;


namespace ContractManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() // returns a list of departments
        {
            var departments = _context.Departments.ToList(); 
            return View(departments);
        }

        [HttpGet]
        public IActionResult CreateDepartment()
        {
            //var departmentmodel = new DepartmentVm();
            return View();
        }

        [HttpPost]
        public IActionResult CreateDepartment(DepartmentVm obj)
        {
            if (!ModelState.IsValid)
            {

                return View(obj);
            }
            var selecteddepartment = obj.SelectedDepartment;
            //check if the department is arleady in the database
            var result = DepartmentExists(obj.SelectedDepartment);
            if (result)
            {

                ModelState.AddModelError("", "department already exists");
                return View(obj);
            }
            // Create a new Department entity
            var myDepartment = new Department
            {
                //SelectedDepartment = department.SelectedDepartment,
                DepartmentName = Enum.GetName(typeof(Predefined_Departments), obj.SelectedDepartment),
                DepartmentCode = (int)obj.SelectedDepartment // Explicitly cast to int if necessary
            };

            // Add the new department to the database
            _context.Departments.Add(myDepartment);
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect to Index action or another appropriate action
        }
        private bool DepartmentExists(Predefined_Departments department)
        {

            var result = _context.Departments.Find((int)department);
            if (result == null)//if it doesnt exist
            {

                return false;
            }
            else
            {
                return true;
            }

        }

        public IActionResult DeleteDepartment(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartmentConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return Ok();
        }

        /*[HttpPost]
        public async Task<IActionResult> EditDepartment(int Id, string Name, string Description)
        {
            if (ModelState.IsValid)
            {
                var existingDepartment = await _context.Departments.FindAsync(Id);
                if (existingDepartment == null)
                {
                    return NotFound(); // Department not found
                }

                // Update the department details
                existingDepartment.Name = Name;
                existingDepartment.Description = Description;

                // Save changes to the database
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(Id))
                    {
                        return NotFound(); // Department not found
                    }
                    else
                    {
                        throw; // Re-throw the exception if the department still exists
                    }
                }

                // Redirect to the list of departments
                return Ok("Department updated successfully");
            }

            return BadRequest("Error updating department.");
        }*/

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }

        public IActionResult ManageDepartment()
        {
            return RedirectToAction("Index");
        }
    }
}
