using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ContractManagementSystem.Models
{
    public class Employee : IdentityUser
    {
        [Key]
        public int EmployeeNumber { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; } 
        [Required]
        public string? AlternativePhoneNumber { get; set; }



        [Required]
        public string? KraPin { get; set; }
        // Foreign key
        public int DepartmentId { get; set; }

        // Navigation property
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }



    }



}
