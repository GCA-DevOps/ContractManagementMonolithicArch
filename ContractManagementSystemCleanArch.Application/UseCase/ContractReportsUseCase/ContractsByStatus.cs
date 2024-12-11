using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Interfaces.ContractReportsInterfaces;
using CMS.Domain.Entities;
using System.Collections.Generic;

namespace CMS.Application.UseCase.ContractReportsUseCase
{
    public class GetContractsByStatusUseCase
    {
        private readonly IReportContractRepository _contractRepository;

        public GetContractsByStatusUseCase(IReportContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IEnumerable<ContractsReport> Execute(string status)
        {
            switch (status.ToLower())
            {
                case "pending":
                case "active":
                case "rejected":
                case "expired":
                    return _contractRepository.GetContractsByStatus(status);
                default:
                    return new List<ContractsReport>(); // Returning an empty list for invalid status
            }
        }
    }

    // Similarly, create other use cases for different report types
}
