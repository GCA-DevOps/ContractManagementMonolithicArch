using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ContractManagementSystem.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();

        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
