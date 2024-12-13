namespace ContractManagementSystem.Models.ContractManagement
{
    public class ContractParty
    {
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public Contract? Contract { get; set; }
        public int? PartyId { get; set; }
        public Party? Party { get; set; }
    }
}
