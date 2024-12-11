using CMS.Domain.Entities;

namespace CMS.Application.Interfaces.CollaborationInterfaces
{
    public interface ICollaborationRepository
    {
        Task<Collaboration> GetByIdAsync(int id);
        Task<List<Collaboration>> GetAllAsync();
        Task<bool> AddAsync(Collaboration collaboration);
        Task DeleteAsync(Collaboration collaboration);
        Task UpdateAsync(Collaboration collaboration);
        bool ExistsEmail(string email);
        Task DeleteAsync(int id);
    }
}
