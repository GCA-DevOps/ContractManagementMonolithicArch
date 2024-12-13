namespace ContractManagementSystem.ViewModels
{
    public class AddApproversVM
    {
        public required string WorkflowName { get; set; }
        public required string Description { get; set; }
        public required string ApprovalType { get; set; }
        public required string SLAName { get; set; }
        public required List<string> Approver { get; set; }
    }
}
