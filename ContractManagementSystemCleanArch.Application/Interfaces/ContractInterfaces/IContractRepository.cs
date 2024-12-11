using CMS.Domain.DTO;
using CMS.Domain.Entities.Contract;


namespace CMS.Application.Interfaces.ContractInterfaces
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAllAsync();
        Task<Contract> AddAsync(Contract contract);
        Task UpdateAsync(Contract contract);
        Task DeleteAsync(int id);
        Task<bool> AddContract(ContractDto contract);
        Task UpdateContract(Contract contract);
        Task<bool> DeleteContract(int id);
        Task<Contract?> GetById(int id);
        Task<bool> ContractExistsAsync(int id);
    }
}

