using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.ViewModels

{
    public class VendorCompanyRegisterVm
    {
        //common properties to all users
        [Required(ErrorMessage = "Company Name is required")]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Company Registration Number is required")]
        public string? RegistrationNumber { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        public string? Email { get; set; }
/*
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "passwords don't match")]
        [Display(Name = " Confirm Password")]
        public string? ConfirmPassword { get; set; }*/

       /* [Required(ErrorMessage = "Please select a Role")]
        public string Role { get; set; }*/
        [Required(ErrorMessage = "KRA pin is required")]  
        public string? KraPin { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; }
       
        public string? AlternativePhoneNumber { get; set; }
        [Required(ErrorMessage = "Postal Address is required")]
        public string? PostalAddress { get; set; }
        [Required(ErrorMessage = "Physical Address is required")]
        public string? PhysicalAddress { get; set; }
        [Required]
        public string SelectedRole { get; set; } // Role selection


    }




}      
