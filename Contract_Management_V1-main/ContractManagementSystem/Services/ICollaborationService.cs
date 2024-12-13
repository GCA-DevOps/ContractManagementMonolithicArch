using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using ContractManagementSystem.Models.ContractManagementSystem.Models;
using ContractManagementSystem.ViewModels;
using System.Threading.Tasks;

namespace ContractManagementSystem.Services
{
    public interface ICollaborationService
    {
        Task AddCollaboratorAsync(int contractId, string email, int permissionLevel);
        
    }

    public class CollaborationService : ICollaborationService
    {
        private readonly ApplicationDbContext _context;

        public CollaborationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCollaboratorAsync(int contractId, string email, int permissionLevel)
        {
            var collaborator = new Collaborator
            {
                ContractId = contractId,
                Email = email,
                PermissionLevel = permissionLevel,
                NotifyByEmail = true, // Default value or as needed
                Message = "Welcome to the contract collaboration." // Default value or as needed
            };

            _context.Collaborators.Add(collaborator);
            await _context.SaveChangesAsync();
        }

       
    }
}
