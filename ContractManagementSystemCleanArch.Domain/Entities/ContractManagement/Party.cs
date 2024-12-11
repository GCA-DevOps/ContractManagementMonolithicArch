using CMS.Domain.Entities.ContractManagement;

namespace CMS.Domain.Entities.Contract.ContractManagement
{
    public class Party
    {
        public int Id { get; set; }
        public string? PartyName { get; set; }
        public string? Email { get; set; }

        public ICollection<ContractParty>? ContractParties { get; set; } // Navigation property

    }
}
