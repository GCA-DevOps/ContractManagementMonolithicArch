using ContractManagementSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;


namespace ContractManagementSystem.ViewModels
{
    public class AdminUpdateVM
    {
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Employee Number is Required")]
        [Display(Name = "Employee Number")]
        public int EmployeeNumber { get; set; }

        [Display(Name = "Authorization Level")]
        public string AuthorizationLevel { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Alternative Phone Number")]
        [Phone]
        public string? AlternativePhoneNumber { get; set; }
    }
}
