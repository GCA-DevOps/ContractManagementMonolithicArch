using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class ApprovalProcess
    {
        [Key]
        public int Id { get; set; }
        public int ContractId;
        public string ContractStatus;
        public int ApproverId;
        public string ApproverComments;
        public string ApproverSignature;

        public static string Status { get; internal set; }
    }
}