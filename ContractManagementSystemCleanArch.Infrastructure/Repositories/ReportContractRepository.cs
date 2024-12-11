using CMS.Application.Interfaces.ContractReportsInterfaces;
using CMS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Infrastructure.Repositories
{
    public class ReportContractRepository : IReportContractRepository
    {
        private readonly List<ContractsReport> _contracts;

        public ReportContractRepository()
        {
            // Initialize with some dummy data or fetch from the database
            _contracts = new List<ContractsReport>
            {
                new ContractsReport { Status = "Active", Type = "Type1", Value = 1000, Parties = "Party1", Department = "Dept1" },
                // Add more dummy data as needed
            };
        }

        public IEnumerable<ContractsReport> GetContractsByStatus(string status)
        {
            return _contracts.Where(c => c.Status == status);
        }

        public IEnumerable<ContractsReport> GetContractsByType(string type)
        {
            return _contracts.Where(c => c.Type == type);
        }

        public IEnumerable<ContractsReport> GetContractsByValue(decimal minValue, decimal maxValue)
        {
            return _contracts.Where(c => c.Value >= minValue && c.Value <= maxValue);
        }

        public IEnumerable<ContractsReport> GetContractsByParties(string parties)
        {
            return _contracts.Where(c => c.Parties.Contains(parties));
        }

        public IEnumerable<ContractsReport> GetContractsByDepartment(string department)
        {
            return _contracts.Where(c => c.Department == department);
        }

        public IEnumerable<ContractsReport> GetAll()
        {
            return _contracts;
        }

        // Implement other repository methods as needed
    }
}
