using CMS.Domain.Entities.Approval;

namespace CMS.Application.Interfaces.ContractInterfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByContractIdAsync(int contractId);
    }
}
