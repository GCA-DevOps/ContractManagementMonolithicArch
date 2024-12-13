using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.ViewModels
{
    public class AddCollaboratorRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int PermissionLevel { get; set; }

        public bool NotifyByEmail { get; set; }
        public string Message { get; set; }
        public int ContractId { get; set; }
    }




}
