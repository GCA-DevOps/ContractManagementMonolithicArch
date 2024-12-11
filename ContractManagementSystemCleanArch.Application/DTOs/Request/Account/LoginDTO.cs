using System.ComponentModel.DataAnnotations;

namespace CMS.Application.DTOs.Request.Account
{
    public class LoginDTO
    {
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        //private const string PasswordPattern = @"^(?=.[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$!%?&])[A-Za-z\d@$!%?&]{8,}$\r\n";

        [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression(EmailPattern, ErrorMessage = "Please provide a valid email.")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Password is required.")]
        //[RegularExpression(PasswordPattern, ErrorMessage = "Your password must be a mix of alphanumeric and special characters, and at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}
