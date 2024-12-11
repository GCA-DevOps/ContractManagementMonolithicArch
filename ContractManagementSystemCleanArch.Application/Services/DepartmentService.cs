using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.DepartmentInterfaces;
using CMS.Domain.Entities;

namespace CMS.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public Task<List<Department>> GetDepartments()
        {

            return _departmentRepository.GetAllDepartmentsAsync();
        }

        public async Task <bool> AddAsync(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                DepartmentName = Enum.GetName(typeof(Predefined_Departments), departmentDto.SelectedDepartment),
                DepartmentCode = (int)departmentDto.SelectedDepartment
            };

            var result =  await _departmentRepository.AddAsync(department);

            if (result) { return true; }
            else { return false; }
        }

        public Task<bool> AddAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
