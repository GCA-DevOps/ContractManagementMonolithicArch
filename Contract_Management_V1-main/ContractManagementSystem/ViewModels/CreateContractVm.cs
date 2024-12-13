using ContractManagementSystem.Models;
using ContractManagementSystem.Models.ContractManagement;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Ensure this is included for IFormFile

namespace ContractManagementSystem.ViewModels
{
    public class CreateContractVm
    {
        public int ContractId { get; set; }

        [Required(ErrorMessage = "Party Name is required.")]
        public string? PartyName { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Contract Type is required.")]
        public Predefined_ContractTypes ContractType { get; set; }

        [Required(ErrorMessage = "Contract Value is required.")]
        public decimal ContractValue { get; set; }

        [Required(ErrorMessage = "Payment Structure is required.")]
        public Predefined_PaymentStructure PaymentStructure { get; set; }

        [Required(ErrorMessage = "Requester Name is required.")]
        public string? RequesterName { get; set; }

        public string? RequesterDepartment { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "The start date cannot be earlier than today.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public IFormFile? ContractFile { get; set; }

        [Required(ErrorMessage = "Contract Term is required.")]
        public string? ContractTerm { get; set; }

        [Required(ErrorMessage = "Termination Notice Period is required.")]
        public int TerminationNoticePeriod { get; set; }

        public ICollection<Approval>? Approval { get; set; }
        public ICollection<ContractParty>? ContractParties { get; set; } // Navigation property

        public int SelectedDepartment { get; set; }

    }
}
