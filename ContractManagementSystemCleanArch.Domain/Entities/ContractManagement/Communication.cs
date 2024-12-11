using CMS.Domain.Entities.Contract;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities.ContractManagement
{
    public class Communication
    {
        [Key]
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Type { get; set; } // Email/Meeting
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }

        public CMS.Domain.Entities.Contract.Contract? Contract { get; set; }
    }
}
