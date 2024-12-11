using System.ComponentModel.DataAnnotations;

namespace CMSCleanArch.ViewModels
{
    public class TokenRequestVM
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }


    }
}
