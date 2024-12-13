using ContractManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPrivilegeService
{
    Task<List<Privilege>> GetAllPrivilegesAsync();
    Task<Privilege> GetPrivilegeByIdAsync(int id);
    Task<Privilege> GetPrivilegeByNameAsync(string name); // Add this method
    Task CreatePrivilegeAsync(string name);
    Task<List<string>> GetPrivilegesByRolesAsync(IList<string> roles);
}
