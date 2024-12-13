using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace ContractManagementSystem.Models
{
    public class Approval
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public Contract? Contract { get; set; }
        public string? ApproverId { get; set; } // User Id of the approver

        public AppUser? AppUser { get; set; }
        public DateTime DateApproved { get; set; }
        public bool IsApproved { get; set; } = false;

    }
}
