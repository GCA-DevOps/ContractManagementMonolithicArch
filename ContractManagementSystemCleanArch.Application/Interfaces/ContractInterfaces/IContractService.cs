using CMS.Application.DTOs.Request;
using CMS.Domain.Entities.Contract;

namespace CMS.Application.Interfaces.ContractInterfaces
{

    public interface IContractService
    {
        // Task<IEnumerable<Contract>> GetAllContracts();

        Task<bool> AddContract(CreateContractDto contract);
        Task UpdateContract(CreateContractDto contract);
        Task<bool> DeleteContract(int id);
        Task<Contract?> GetById(int id);
        Task<bool> ContractExistsAsync(int id);
    }
}
