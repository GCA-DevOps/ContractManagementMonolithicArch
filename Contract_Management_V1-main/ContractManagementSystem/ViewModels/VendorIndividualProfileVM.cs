using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.ViewModels
{
    public class VendorIndividualProfileVM
    {
        [Required]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Other Name")]
        public string? OtherName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "NHIF Number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? NhifNumber { get; set; }

        [Required]
        [Display(Name = "NSSF Number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? NssfNumber { get; set; }

        [Required]
        [Display(Name = "KRA Pin")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? KraPin { get; set; }

        [Required(ErrorMessage = "Please enter your ID Number.")]
        [Display(Name = "ID Number")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? IdentificationNumber { get; set; }
    }
}
