using System.ComponentModel;

namespace ContractManagementSystem.ViewModels
{
    public class RoleViewModel
    {
        [DisplayName("Roles")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string CustomId { get; set; }
    }
}
