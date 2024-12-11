using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Domain.Entities.Contract.ContractManagement
{
    public class TerminationClause
    {

        [Key]
        public int Id { get; set; }

        public int ContractId { get; set; }

        public Contract? Contract { get; set; }
        public int NoticePeriod { get; set; }



    }
}
