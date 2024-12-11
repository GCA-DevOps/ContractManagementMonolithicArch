using CMS.Application.DTOs.Request;
using CMS.Application.Interfaces.ContractInterfaces;
using CMS.Application.Mappings;
using CMS.Domain.Entities.Contract;


namespace CMS.Application.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        
        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }
        public async Task<bool> AddContract(CreateContractDto contractDto)
        {

            // Convert CreateContractDto to ContractDto
            var domainContractDto = ContractMapper.ToDomain(contractDto);


            var result = await _contractRepository.AddContract(domainContractDto);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ContractExistsAsync(int id)
        {
            var result = await _contractRepository.ContractExistsAsync(id);
            if (result) 
            { 
                return true;
            
            }
            return false;

        }

        public async Task<bool> DeleteContract(int id)
        {
            var result = await _contractRepository.DeleteContract(id);
            if (result)
            {
                return true;
            }
            return false;

        }

        public Task<Contract?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContract(CreateContractDto contract)
        {
            throw new NotImplementedException();
        }
    }
}