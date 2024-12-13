using ContractManagementSystem.Data;
using ContractManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManagementSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleService(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<ApplicationRole> CreateRoleAsync(string roleName)
        {
            var role = new ApplicationRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return role;
            else
                throw new Exception("Failed to create role");
        }

        public async Task AssignPrivilegeAsync(string roleId, int privilegeId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var privilege = await _context.Privileges.FindAsync(privilegeId);

            if (role != null && privilege != null)
            {
                var rolePrivilege = new RolePrivilege
                {
                    RoleId = role.Id,
                    PrivilegeId = privilege.Id
                };
                _context.RolePrivileges.Add(rolePrivilege);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Role or Privilege not found");
            }
        }

        public List<Privilege> GetAllPrivileges()
        {
            return _context.Privileges.ToList();
        }

        public async Task<List<Privilege>> GetPrivilegesByRoleAsync(string roleId)
        {
            var rolePrivileges = await _context.RolePrivileges
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.Privilege)
                .ToListAsync();

            return rolePrivileges;
        }

        public async Task UpdateRolePrivilegesAsync(string roleId, List<int> privilegeIds)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            // Remove existing role privileges
            var existingRolePrivileges = _context.RolePrivileges.Where(rp => rp.RoleId == roleId);
            _context.RolePrivileges.RemoveRange(existingRolePrivileges);

            // Assign new privileges
            foreach (var privilegeId in privilegeIds)
            {
                var rolePrivilege = new RolePrivilege
                {
                    RoleId = roleId,
                    PrivilegeId = privilegeId
                };
                _context.RolePrivileges.Add(rolePrivilege);
            }

            await _context.SaveChangesAsync();
        }

        // Implement other methods for managing roles if needed
    }
}
