using CMS.Domain.Entities;
using System.Collections.Generic;

namespace CMS.Application.Interfaces.ContractReportsInterfaces
{
    public interface IReportContractRepository
    {
        IEnumerable<ContractsReport> GetContractsByStatus(string status);
        IEnumerable<ContractsReport> GetContractsByType(string type);
        IEnumerable<ContractsReport> GetContractsByValue(decimal minValue, decimal maxValue);
        IEnumerable<ContractsReport> GetContractsByParties(string parties);
        IEnumerable<ContractsReport> GetContractsByDepartment(string department);
        IEnumerable<ContractsReport> GetAll();
        // Other repository methods
    }
}
