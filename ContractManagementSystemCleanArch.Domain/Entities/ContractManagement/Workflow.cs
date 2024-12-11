using CMS.Domain.Entities.ContractManagement;
using System.ComponentModel.DataAnnotations;

namespace CMS.Domain.Entities.Contract.ContractManagement
{
    public class Workflow // to define steps that a contract go through its life cycle
                          //helps in organizing various tasks ,approvals and processes 
    {
        [Key]
        public int Id { get; set; }
        public string ContractTypeId { get; set; }
        // Other properties as needed

        // Navigation property to refer to the associated stages
        public ContractType ContractType { get; set; }
        public ICollection<Stage> Stages { get; set; }

    }
}
