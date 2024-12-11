using CMS.Domain.Entities.ContractManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities.Contract.ContractManagement
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; } // Primary key
        public int ContractId { get; set; }

        public Contract Contract { get; set; }
        public int PaymentID { get; set; } // Foreign key referencing Payment ID

        public Payment Payment { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        // Other properties as needed

        // Navigation property to refer to the Payment

    }
}
