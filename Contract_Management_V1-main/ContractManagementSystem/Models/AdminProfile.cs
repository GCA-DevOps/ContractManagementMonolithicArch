using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class AdminProfile : IdentityUser
    {
        [Required(ErrorMessage ="First Name is Required")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Employee Number is Required")]
        [Display(Name = "Employee Number")]
        public string? EmployeeNumber { get; set; }
        
        [Display(Name = "Authorization Level")]
        public string? AuthorizationLevel { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public override string? Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        [Phone]
        public override string? PhoneNumber { get; set; }

        [Display(Name = "Alternative Phone Number")]
        [Phone]
        public string? AlternativePhoneNumber { get; set; }
    }
}
