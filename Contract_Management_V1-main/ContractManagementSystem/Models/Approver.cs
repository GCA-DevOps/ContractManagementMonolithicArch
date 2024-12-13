namespace ContractManagementSystem.Models
{
    public class Approver
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }
        public int ContractId { get; set; }
        public string ContractName { get; set; }
        public string ContractType { get; set; }
        public int contractAmount { get; set; }

    }
}