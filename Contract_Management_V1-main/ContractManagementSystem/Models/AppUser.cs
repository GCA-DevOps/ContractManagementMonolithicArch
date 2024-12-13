using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class AppUser:IdentityUser
    {

        [Key]
        public int EmployeeNumber { get; set; }
        
        public string? FirstName { get; set; }
        

        public string? LastName { get; set; }
        public string? OtherName { get; set; }
  
        public string? AlternativePhoneNumber { get; set; }

        public string? KraPin { get; set; }
        public string? NhifNumber { get; set; }
        public string? IdentificationNumber { get; set; }

        public string? NssfNumber { get; set; }
        
        public string? RegistrationNumber { get; set; }
        public string? CompanyName { get; set; }
        public string? PostalAddress { get; set; }
        public string? PhysicalAddress { get; set;}
        public int? DepartmentId { get; set;}

        [StringLength(255)]
        public string? ProfilePicturePath { get; set; }

        public byte[]? ProfilePictureByteArray { get; set; }
    }
}
