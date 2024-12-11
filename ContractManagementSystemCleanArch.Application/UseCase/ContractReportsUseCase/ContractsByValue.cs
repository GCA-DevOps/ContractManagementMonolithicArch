using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Interfaces.ContractReportsInterfaces;
using CMS.Domain.Entities;
using System.Collections.Generic;

namespace CMS.Application.UseCase.ContractReportsUseCase
{
    public class GetContractsByValueUseCase
    {
        private readonly IReportContractRepository _contractRepository;

        public GetContractsByValueUseCase(IReportContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IEnumerable<ContractsReport> Execute(decimal minValue, decimal maxValue)
        {
            return _contractRepository.GetContractsByValue(minValue, maxValue);
        }
    }
}

