using System.Collections.Generic;

namespace ContractManagementSystem.Models
{
    public class ManagePrivilegesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<int> PrivilegeIds { get; set; } = new List<int>(); // List of selected privilege IDs
        public List<PrivilegeViewModel> Privileges { get; set; } = new List<PrivilegeViewModel>();
    }

    public class PrivilegeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }

}
