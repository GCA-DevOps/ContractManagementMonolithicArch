using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.ViewModels
{
    public class CollaboratorViewModel
    {
        public int ContractId { get; set; }
        public string CollaboratorEmail { get; set; }
        public string Permissions { get; set; }
        public bool NotifyByEmail { get; set; }
        public string OptionalMessage { get; set; }
    }


}
