using System.ComponentModel.DataAnnotations;

namespace ContractManagementSystem.Models
{
    public class Negotiations
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public int Contact { get; set; }

        [StringLength(100)]
        public string? Comments { get; set; }




    }
}

