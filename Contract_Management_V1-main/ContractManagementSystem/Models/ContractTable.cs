using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class ContractTable
    {
        [Key]
        public required string ContractId { get; set; }

        public required string ContractedParty { get; set; }
        public required string ContractType { get; set; }
        public decimal ContractValue { get; set; }
        public required string PaymentStructure { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContractStatus Status { get; set; }
        public required string Action { get; set; } // Action buttons


        // Other properties removed for simplicity
    }

}
