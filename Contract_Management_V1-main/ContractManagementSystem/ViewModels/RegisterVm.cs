using ContractManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.ViewModels

{
    public class RegisterVm
    {
        //common properties to all users
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "passwords don't match")]
        [Display(Name = " Confirm Password")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select a user type")]
        public UserType UserType { get; set; }
        
        //additional properties
        public string? PhoneNumber { get; set; }

        public string? AltPhoneNumber { get; set; }



        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public string? OtherName { get; set; }
        [Required]
        public string? EmployeeNumber { get; set; }
        [Required]
        public string? Nhif { get; set; }
        [Required]
        public string? Nssf { get; set; }

        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? RegistrationNumber { get; set; }
        [Required]
        public string? PhysicalAddress { get; set; }
        [Required]
        public string? KraPin { get; set; }

     }

        public enum UserType
        {
            Employee,
            VendorCompany,
            VendorIndividual
       }

}
