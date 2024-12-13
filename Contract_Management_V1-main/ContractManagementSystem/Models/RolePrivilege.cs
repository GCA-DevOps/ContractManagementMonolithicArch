namespace ContractManagementSystem.Models
{
    public class RolePrivilege
    {
        public string RoleId { get; set; }
        public ApplicationRole Role { get; set; }
        public int PrivilegeId { get; set; }
        public Privilege Privilege { get; set; }
    }
}
