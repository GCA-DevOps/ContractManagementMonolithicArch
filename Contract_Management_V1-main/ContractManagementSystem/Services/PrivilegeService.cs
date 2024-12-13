using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PrivilegeService : IPrivilegeService
{
    private readonly ApplicationDbContext _context;

    public PrivilegeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Privilege>> GetAllPrivilegesAsync()
    {
        return await _context.Privileges.ToListAsync();
    }

    public async Task<Privilege> GetPrivilegeByIdAsync(int id)
    {
        return await _context.Privileges.FindAsync(id);
    }
    public async Task<Privilege> GetPrivilegeByNameAsync(string name) // Implement this method
    {
        return await _context.Privileges.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task CreatePrivilegeAsync(string name)
    {
        var privilege = new Privilege { Name = name };
        _context.Privileges.Add(privilege);
        await _context.SaveChangesAsync();
    }

    public async Task<List<string>> GetPrivilegesByRolesAsync(IList<string> roles)
    {
        return await _context.RolePrivileges
            .Where(rp => roles.Contains(rp.Role.Name))
            .Select(rp => rp.Privilege.Name)
            .Distinct()
            .ToListAsync();
    }
}

