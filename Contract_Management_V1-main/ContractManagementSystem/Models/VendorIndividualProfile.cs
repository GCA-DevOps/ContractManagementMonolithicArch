using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class VendorIndividualProfile
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Other Name")]
        public string? OtherName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Telephone Number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your NHIF Number.")]
        [Display(Name = "NHIF Number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? NHIFNumber { get; set; }

        [Required(ErrorMessage = "Please enter your NSSF Number.")]
        [Display(Name = "NSSF Number")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? NSSFNumber { get; set; }

        [Required(ErrorMessage = "Please enter your KRA Pin.")]
        [Display(Name = "KRA Pin")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "The {0} must be at least 10 characters long and not more than 15 characters.")]
        public string? KRAPin { get; set; }
    }
}
