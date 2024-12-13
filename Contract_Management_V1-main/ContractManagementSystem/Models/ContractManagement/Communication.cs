using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models.ContractManagement
{
    public class Communication
    {
        [Key]
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Type { get; set; } // Email/Meeting
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }

        public Contract Contract { get; set; }
    }
}
