using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Interfaces.ContractReportsInterfaces;
using CMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Application.UseCase.ContractReportsUseCase
{
    public class GetCustomContractsReportUseCase
    {
        private readonly  IReportContractRepository _contractRepository;

        public GetCustomContractsReportUseCase(IReportContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        // Single Execute method with Func<ContractsReport, bool> predicate
        public IEnumerable<ContractsReport> Execute(Func<ContractsReport, bool> predicate)
        {
            return _contractRepository.GetAll().Where(predicate);
        }
    }
}
