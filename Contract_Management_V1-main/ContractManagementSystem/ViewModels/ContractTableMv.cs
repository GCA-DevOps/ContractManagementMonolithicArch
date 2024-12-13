using ContractManagementSystem.Models;

namespace ContractManagementSystem.ViewModels
{
    public class ContractTableMv
    {
        public required string ContractId { get; set; }
        public required string ContractedParty { get; set; }
        public required string ContractType { get; set; }
        public decimal ContractValue { get; set; }
        public required string PaymentStructure { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContractStatus Status { get; set; }
        public required string Action { get; set; }
    }
}
