using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.DepartmentInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
            
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDto departmentDto)
        {
            if (ModelState.IsValid) 
            {
                var result = await _departmentService.AddAsync(departmentDto);
                if (result)
                {
                    return Ok("department successfully added");
                }

            }
            return BadRequest("failed to add department");    
        }
    }
}
