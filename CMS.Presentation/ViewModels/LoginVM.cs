using System.ComponentModel.DataAnnotations;

namespace CMS.Presentation.ViewModels
{
    public class LoginVM
    {

        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public string? Password { get; set; }


    }
}
