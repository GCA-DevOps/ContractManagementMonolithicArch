using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractManagementSystem.Models.ContractDetails
{
    public class Deliverables
    {
        [Key]
        public string Id { get; set; }

        public int ContractId { get; set; }
        
        public Contract Contract { get; set; }
        public string Description { get; set; }

        public DateTime DueDate { get; set; }


    }
}
