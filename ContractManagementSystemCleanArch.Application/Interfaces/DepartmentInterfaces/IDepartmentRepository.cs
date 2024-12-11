using CMS.Domain.Entities;

namespace CMS.Application.Interfaces.DepartmentInterfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<bool> AddAsync(Department department);
    }
}
