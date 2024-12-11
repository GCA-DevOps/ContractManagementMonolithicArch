using CMS.Domain.Entities.Contract;
using CMS.Domain.Entities.ContractManagement;
using Microsoft.AspNetCore.Http;

namespace CMS.Domain.DTO
{
    public class ContractDto
    {
        public string PartyName { get; set; }
        public int? ContractId { get; set; }
        public string Email { get; set; }
        public Predefined_ContractTypes ContractType { get; set; }
        public decimal ContractValue { get; set; }
        public Predefined_PaymentStructure PaymentStructure { get; set; }
        public string RequesterName { get; set; }
        public string RequesterDepartment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContractTerm { get; set; }
        public ContractStatus? Status { get; set; }
        public IFormFile? ContractFile { get; set; }
        public int TerminationNoticePeriod { get; set; }
        public int SelectedDepartment { get; set; }
    }
}