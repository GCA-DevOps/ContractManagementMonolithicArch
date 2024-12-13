namespace ContractManagementSystem.Models
{
    public class RoleDetailsViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<Privilege> Privileges { get; set; } = new List<Privilege>();
    }
}
