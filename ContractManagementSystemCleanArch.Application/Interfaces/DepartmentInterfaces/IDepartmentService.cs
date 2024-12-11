using CMS.Application.DTOs.Request;
using CMS.Domain.Entities;

namespace CMS.Application.Interfaces.DepartmentInterfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetDepartments();
        Task<bool> AddAsync(DepartmentDto department);
    }

}
