using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models.ContractManagement
{
   public enum MilestoneStatus
    {
        Pending,Completed
    }
    public class Milestone //Used to define and track important milestones or deadlines associated with a contract.
                            //Each milestone record includes a description of the milestone,
                            //its due date, and its current status (e.g., pending, completed).
    {

        public int Id { get; set; }
        public int ContractId { get; set; }
       
        public Contract Contract { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public MilestoneStatus Status { get; set; }=MilestoneStatus.Pending;//default
    }
}
