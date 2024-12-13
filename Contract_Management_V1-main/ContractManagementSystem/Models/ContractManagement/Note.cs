using NuGet.Common;
using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models.ContractManagement
{
    public class Note   //Used to record and track notes or comments related to a contract.
                        //Each note record includes details such as the user who created the note,
                        //the date it was created, and the content of the note.
    {

        [Key]
        public int Id { get; set; } // Primary key
        public int ContractId { get; set; } // Foreign key referencing Contract Id

        // Foreign key referencing User ID
        public int UserId { get; set; }
        public int ApprovalId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; }
       
        public AppUser AppUser { get; set; }
        public Contract Contract { get; set; }
        public Approval Approval { get; set; }
    }
}
