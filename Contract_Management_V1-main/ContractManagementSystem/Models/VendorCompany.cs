using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models
{
    public class VendorCompany
    {

        [Key]
        public int VendorCompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? RegistrationNumber { get; set;}
        public string? PostalAddress { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AlternativePhoneNumber { get; set; }
        public string? PhysicalAddress { get; set; }
        public string? KraPin { get; set; }

        [Required]
        [ForeignKey("AppUser")] // Reference the navigation property in AppUser
        public int AppUserId { get; set; }
    }
}
