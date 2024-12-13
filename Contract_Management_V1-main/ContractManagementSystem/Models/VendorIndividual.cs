using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models
{
    public class VendorIndividual
    {

        [Key]
        public int VendorIndividualId { get; set; }
        public string?  FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AlternativePhoneNumber { get; set; }

        public string? NhifNumber { get; set; }

        public string? NssfNumber { get; set; }

        public string? KraPin { get; set; }

        public string? IdentificationNumber { get; set; }
        [Required]
        [ForeignKey("AppUser")] // Reference the navigation property in AppUser
        public int AppUserId { get; set; }

       
    }
}
