using CMS.Domain.Entities.Approval;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities.ContractManagement
{
    public class Stage
    {
        [Key]
        public int Id { get; set; }
        public int WorkflowId { get; set; } // Foreign key referencing Workflow ID
        public string WorkflowName { get; set; }
        // Other properties as needed

        // Navigation property to refer to the associated approvers
        public ICollection<Approver> Approvers { get; set; }

    }
}
