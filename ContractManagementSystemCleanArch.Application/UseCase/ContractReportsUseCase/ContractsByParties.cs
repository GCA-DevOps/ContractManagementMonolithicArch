using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Interfaces.ContractReportsInterfaces;
using CMS.Domain.Entities;
using System.Collections.Generic;

namespace CMS.Application.UseCase.ContractReportsUseCase
{
    public class GetContractsByPartiesUseCase
    {
        private readonly IReportContractRepository _contractRepository;

        public GetContractsByPartiesUseCase(IReportContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IEnumerable<ContractsReport> Execute(string party)
        {
            return _contractRepository.GetContractsByParties(party);
        }
    }
}
