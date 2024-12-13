using Microsoft.AspNetCore.Authorization;

namespace ContractManagementSystem.Authorization
{
    public class PrivilegeRequirement : IAuthorizationRequirement
    {
        public string PrivilegeName { get; }

        public PrivilegeRequirement(string privilegeName)
        {
            PrivilegeName = privilegeName;
        }
    }
}
