using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models.ContractManagement
{
    public class ContractType
    {
        [Key]
        public int Id { get; set; } // Primary key
        public string Name { get; set; } // Name of the contract type, e.g., "Sales Contract", "Service Agreement", etc.
        public string Description { get; set; } // Description of the contract type
                                                // Other properties as needed

        // Navigation property to refer to associated workflows
        public ICollection<Workflow> Workflows { get; set; }
    }


}

