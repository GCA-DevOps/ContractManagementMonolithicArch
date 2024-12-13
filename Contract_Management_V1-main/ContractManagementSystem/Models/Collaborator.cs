namespace ContractManagementSystem.Models
{
    namespace ContractManagementSystem.Models
    {
        public class Collaborator
        {
            public int Id { get; set; } // Primary Key
            public int ContractId { get; set; }
            public string Email { get; set; }
            public int PermissionLevel { get; set; }
            public bool NotifyByEmail { get; set; }
            public string Message { get; set; }

           
        }
    }

}
