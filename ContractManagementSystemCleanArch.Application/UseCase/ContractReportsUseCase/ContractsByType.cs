using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Interfaces.ContractReportsInterfaces;
using CMS.Domain.Entities;

namespace CMS.Application.UseCase.ContractReportsUseCase
{
    public class GetContractsByTypeUseCase
    {
        private readonly IReportContractRepository _contractRepository;

        public GetContractsByTypeUseCase(IReportContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IEnumerable<ContractsReport> Execute(string type)
        {
            return _contractRepository.GetContractsByType(type);
        }
    }
}


