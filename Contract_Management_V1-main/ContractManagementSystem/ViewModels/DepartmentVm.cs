using System.ComponentModel.DataAnnotations;
using ContractManagementSystem.Models;

namespace ContractManagementSystem.ViewModels
{
    public class DepartmentVm
    {
        [Required]
        public Predefined_Departments SelectedDepartment { get; set; }

    }
}
