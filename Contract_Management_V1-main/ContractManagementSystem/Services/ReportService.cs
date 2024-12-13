using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractManagementSystem.Services
{
    public interface IReportService
    {
        List<ContractReportResult> GenerateContractReport(ContractReportFilter filter);
    }

    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ContractReportResult> GenerateContractReport(ContractReportFilter filter)
        {
            var query = _context.Contracts.AsQueryable();

            // Filter by Contract Type
            if (!string.IsNullOrEmpty(filter.ContractType))
            {
                query = query.Where(c => c.ContractType == filter.ContractType);
            }

            // Filter by Start and End Dates
            if (filter.StartDate.HasValue)
            {
                query = query.Where(c => c.EffectiveDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(c => c.EndDate <= filter.EndDate);
            }

            // Filter by Department
            if (!string.IsNullOrEmpty(filter.Department))
            {
                query = query.Where(c => c.Department.DepartmentName == filter.Department);
            }

            // Include Expired Contracts
            if (!filter.IncludeExpired)
            {
                query = query.Where(c => c.EndDate > DateTime.Now);
            }

            // Select the results
            return query.Select(c => new ContractReportResult
            {
                ContractId = c.Id.ToString(),
                PartyName = c.ContractParty,
                ContractType = c.ContractType,
                StartDate = c.EffectiveDate,
                EndDate = c.EndDate,
                ContractValue = c.ContractValue,
                Status = c.Status.ToString()
            }).ToList();
        }
    }
}
