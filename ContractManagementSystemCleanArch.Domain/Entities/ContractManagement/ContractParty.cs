using CMS.Domain.Entities.Contract.ContractManagement;

namespace CMS.Domain.Entities.ContractManagement
{
    public class ContractParty
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public CMS.Domain.Entities.Contract.Contract? Contract { get; set; }
        public int? PartyId { get; set; }
        public Party? Party { get; set; }
    }
}
