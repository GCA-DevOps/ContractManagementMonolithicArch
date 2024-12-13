using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models.ContractManagement
{
    public class ContractDocument
    {
        [Key]
        public int Id { get; set; }
        public byte[]? FileData { get; set; }

        public int ContractId { get; set; }
        public Contract? Contract { get; set; }
    }
}
