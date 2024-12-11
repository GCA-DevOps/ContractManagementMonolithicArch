using CMS.Application.Interfaces.ContractReportsInterfaces;
using CMS.Domain.Entities;

namespace CMS.Application.UseCase.ContractReportsUseCase
{
    public class GetContractsByDepartmentUseCase
    {
        private readonly IReportContractRepository _contractRepository;

        public GetContractsByDepartmentUseCase(IReportContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public IEnumerable<ContractsReport> Execute(string department)
        {
            return _contractRepository.GetContractsByDepartment(department);
        }
    }
}
