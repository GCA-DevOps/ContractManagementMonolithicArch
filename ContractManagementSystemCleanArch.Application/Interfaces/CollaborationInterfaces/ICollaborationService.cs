using CMS.Application.DTOs.Request;
using CMS.Domain.Entities;

namespace CMS.Application.Interfaces.CollaborationInterfaces
{
    public interface ICollaborationService
    {
        Task<Collaboration> GetCollaboratorByIdAsync(int id);
        Task<List<Collaboration>> GetAllCollaboratorsAsync();
        Task<bool> AddCollaboratorAsync(CollaborationDto collaborationDto);
        Task RemoveCollaboratorAsync(Collaboration collaboration);
        Task UpdateCollaborationAsync(CollaborationDto collaborationDto);
        bool EmailExists(string email);
        Task ShareContractAsync(int contractId, string documentPath);
    }
}
