using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.ViewModels
{
    public class ForgotPasswordVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
