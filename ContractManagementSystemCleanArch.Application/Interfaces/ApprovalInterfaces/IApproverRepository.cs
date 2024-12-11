using CMS.Application.DTOs.Request.Approval;
using CMS.Domain.Entities.Approval;

namespace CMS.Application.Interfaces.ApprovalInterfaces
{
    public interface IApproverRepository
    {
        Task<Approver> GetByIdAsync(int id);
        Task<IEnumerable<ApproverDTO>> GetAllApproversAsync();
        Task<Approver> AddAsync(Approver approver);
        Task UpdateAsync(Approver approver);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
