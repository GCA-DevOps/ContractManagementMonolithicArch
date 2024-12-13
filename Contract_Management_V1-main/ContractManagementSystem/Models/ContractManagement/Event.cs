using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models.ContractManagement
{
    public class Event //Used to log significant events related to a contract, such as approvals, renewals, or breaches.
                       //Each event record contains details such as the type of event, the date it occurred, and any
                       //additional information or details about the event.
    {
        [Key]
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string? Type { get; set; } // Approval/Renewal/Breach
        public DateTime Date { get; set; }= DateTime.Now;
        public string? Details { get; set; }


    }
}
