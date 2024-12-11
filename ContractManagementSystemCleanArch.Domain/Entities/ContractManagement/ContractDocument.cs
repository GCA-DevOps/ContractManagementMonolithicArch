using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities.ContractManagement
{
    public class ContractDocument
    {
        [Key]
        public int Id { get; set; }
        public byte[]? FileData { get; set; }

        public string FileName { get; set; }
        public string ContentType { get; set; } 
        public int ContractId { get; set; }
        public CMS.Domain.Entities.Contract.Contract? Contract { get; set; }
    }
}
