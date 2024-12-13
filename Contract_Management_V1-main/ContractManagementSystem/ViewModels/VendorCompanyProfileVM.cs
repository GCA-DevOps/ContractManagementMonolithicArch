using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.ViewModels
{
    public class VendorCompanyProfileVM
    {
        [Required]
        [Display(Name = "Company Name")]
        public string? CompanyName { get; set; }

        [Required]
        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "KRA Pin")]
        //[StringLength(12, MinimumLength = 10, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? KraPin { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Physical Address")]
        public string? PhysicalAddress { get; set; }

        [Required]
        [Display(Name = "Postal Address")]
        public string? PostalAddress { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = " Alternative Phone Number")]
        public string? AlternativePhoneNumber { get; set; }


    }
}
