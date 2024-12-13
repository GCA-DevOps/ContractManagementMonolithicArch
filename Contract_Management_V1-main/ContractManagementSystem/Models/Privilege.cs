using System.Collections.Generic;

namespace ContractManagementSystem.Models
{
    public class Privilege
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();
    }
}
