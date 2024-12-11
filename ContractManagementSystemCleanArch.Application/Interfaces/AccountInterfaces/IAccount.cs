
using CMS.Application.DTOs.Request.Account;
using CMS.Application.DTOs.Response.Account;
using CMS.Application.DTOs.Response;
using CMS.Domain.Entities.Authentication;

namespace CMS.Application.Interfaces.AccountInterfaces
{
    public interface IAccount
    {
        Task CreateAdmin();

        Task<GeneralResponse> CreateAccountAsync(CreateAccountDTO model);

        Task<LoginResponse> LoginAccountAsync(LoginDTO model);

        Task<LoginResponse> RefreshTokenAsync(RefreshTokenDTO model);

        Task<GeneralResponse> CreateRoleAsync(CreateRoleDTO model);

        Task<IEnumerable<GetRoleDTO>> GetRolesAsync();

        Task<IEnumerable<GetUsersWithRolesResponseDTO>> GetUsersWithRolesAsync();

        Task<GeneralResponse> ChangeUserRoleAsync(ChangeUserRoleDTO model);

        Task<AppUser?> FindUserByEmailAsync(string email);
    }
}
