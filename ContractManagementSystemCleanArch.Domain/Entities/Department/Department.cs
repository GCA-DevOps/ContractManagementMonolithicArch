using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities
{
    public enum Predefined_Departments
    {
        LEGAL = 01,
        PROCUREMENT = 02,
        SALES = 03,
        FINANCE = 04,
        HUMAN_RESOURCE = 05,
        IT = 06,
        COMPLIANCE = 07,
        EXECUTIVE = 08,
        CUSTOMER_SERVICE = 09,
        PROJECT_MANAGEMENT = 10,
        OPERATIONS = 11,
        CREDIT = 12,
        RISK_MANAGEMENT = 13,

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
