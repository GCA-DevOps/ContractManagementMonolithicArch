using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models
{
    public enum Predefined_Departments
    {
        Legal = 01,
        Procurement = 02,
        Sales = 03,
        Finance = 04,
        Human_Resource = 05,
        IT = 06,
        Compliance = 07,
        Executive= 08,
        Customer_Service = 09,
        Project_Management = 10,
        Operations = 11,
        Credit = 12,
        Risk_Management = 13,

    }
    public class Department
    {
        [Key]
        public int Id { get; set; }



        [Required]
        public string? DepartmentName { get; set; }
        public int DepartmentCode { get; set; }



    }
}
