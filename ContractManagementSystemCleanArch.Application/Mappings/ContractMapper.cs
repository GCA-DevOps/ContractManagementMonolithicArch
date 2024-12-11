using CMS.Application.DTOs.Request;
using CMS.Domain.DTO;

namespace CMS.Application.Mappings
{
    public class ContractMapper
    {
        public static ContractDto ToDomain(CreateContractDto createContractDto)
        {
            return new ContractDto
            {
                PartyName = createContractDto.PartyName,
                ContractId = createContractDto.ContractId,
                Email = createContractDto.Email,
                ContractType = createContractDto.ContractType,
                ContractValue = createContractDto.ContractValue,
                PaymentStructure = createContractDto.PaymentStructure,
                RequesterName = createContractDto.RequesterName,
                RequesterDepartment = createContractDto.RequesterDepartment,
                StartDate = createContractDto.StartDate,
                EndDate = createContractDto.EndDate,
                ContractTerm = createContractDto.ContractTerm,
                Status = createContractDto.Status,
                TerminationNoticePeriod = createContractDto.TerminationNoticePeriod,
                SelectedDepartment = createContractDto.SelectedDepartment
            };
        }
    }
}
