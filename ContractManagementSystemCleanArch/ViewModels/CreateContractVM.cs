using CMS.Domain.Entities;
using CMS.Domain.Entities.ContractManagement;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CMSCleanArch.ViewModels
{
    public class CreateContractVM
    {
       
            [Required]
            public string? PartyName { get; set; }
            public int ContractId { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string? Email { get; set; }
            public required string ContractType { get; set; }
            public decimal ContractValue { get; set; }
            public Predefined_PaymentStructure PaymentStructure { get; set; }
            public string? RequesterName { get; set; }
            public string? RequesterDepartment { get; set; }
            [Required(ErrorMessage = "start date is required")]
            public DateTime StartDate { get; set; }
            [Required]
            public DateTime EndDate { get; set; }
            public string? ContractTerm { get; set; }

            public ContractStatus? Status { get; set; } = ContractStatus.Pending;

            public IFormFile? ContractFile { get; set; }
            [Display(Name = "Termination Notice Period")]
            [Required(ErrorMessage = "This field is required")] // Making it optional by providing an empty error message
            public int TerminationNoticePeriod { get; set; }


            public ICollection<Approval>? Approval { get; set; }
            public ICollection<ContractParty>? ContractParties { get; set; } // Navigation property

            public int SelectedDepartment { get; set; }

            //public List<Party>? Parties { get; set; }





        
    }
}
