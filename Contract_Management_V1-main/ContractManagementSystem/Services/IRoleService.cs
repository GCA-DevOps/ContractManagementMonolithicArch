using System.Threading.Tasks;
using ContractManagementSystem.Models;

namespace ContractManagementSystem.Services
{
    public interface IRoleService
    {
        Task<ApplicationRole> CreateRoleAsync(string roleName);
        Task AssignPrivilegeAsync(string roleId, int privilegeId);
        List<Privilege> GetAllPrivileges();
        Task<List<Privilege>> GetPrivilegesByRoleAsync(string roleId);
        Task UpdateRolePrivilegesAsync(string roleId, List<int> privilegeIds);
     
        // Other methods for managing roles
    }
}
