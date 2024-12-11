using Microsoft.AspNetCore.Identity;

namespace CMS.Domain.Entities.Authentication
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
       
        public string MiddleName { get; set; }
    }
}
