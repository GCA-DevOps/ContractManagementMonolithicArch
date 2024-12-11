using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CMS.Application.DTOs.Request
{
    public class CollaborationDto
    {
        //[Required]
        //public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile? DocumentData { get; set; }

        [StringLength(100)]
        public string DocumentName { get; set; }
    }
}
