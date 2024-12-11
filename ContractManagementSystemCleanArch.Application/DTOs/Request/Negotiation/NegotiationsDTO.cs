using CMS.Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.DTOs.Request
{
    public class NegotiationsDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        //[EmailAddress]

        [ValidateNegotiatorEmail]
        public string Email { get; set; }

        public IFormFile? DocumentData { get; set; }

        public string DocumentName { get; set; }
    }
}
