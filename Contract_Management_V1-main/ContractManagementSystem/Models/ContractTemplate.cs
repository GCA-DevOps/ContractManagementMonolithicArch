using System.ComponentModel.DataAnnotations;
using ContractManagementSystem.Models;

namespace ContractManagementSystem.Models
{
   public class ContractTemplate
   {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string FilePath { get; set; } // Store the file path or file data in the database
   }

}
