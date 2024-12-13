using System;

namespace ContractManagementSystem.Models.Reports
{
    public class ContractReportFilter
    {
        public string ContractType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Department { get; set; }
        public bool IncludeExpired { get; set; }
    }

    public class ContractReportResult
    {
        public string ContractId { get; set; }
        public string PartyName { get; set; }
        public string ContractType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ContractValue { get; set; }
        public string Status { get; set; }
    }
}
