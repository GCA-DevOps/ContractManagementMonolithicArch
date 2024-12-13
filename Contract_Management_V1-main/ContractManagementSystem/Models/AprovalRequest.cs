namespace ContractManagementSystem.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Other document properties
    }

    public class ApprovalRequest
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string ApproverName { get; set; }
        public bool IsApproved { get; set; }
    }

}