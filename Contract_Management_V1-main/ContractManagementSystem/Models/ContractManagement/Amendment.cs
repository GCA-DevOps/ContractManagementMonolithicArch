using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models.ContractManagement
{
    public class Amendment
    {
        [Key]
        public int Id { get; set; }
        public int ContractId{ get; set; }
        public Contract Contract { get; set; }
        public string Description { get; set; } //description of the changes
        public DateTime EffectiveDate { get; set; }// when changes will become effective
        public string Documents { get; set; }
    }
}
