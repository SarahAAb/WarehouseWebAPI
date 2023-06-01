using Microsoft.AspNetCore.Identity;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public interface IAccountService
    {
        Task LogOut();
        Task<SignInResult> SignIn(SignIn signIn);

        Task<IdentityResult> CreateAccount(SignUp signUp);
        Task<IdentityResult> InsertRole(Role role);
        List<Role> RoleList();
        List<UsersDTO> UsersList();
        Task<List<UserRoles>> UserRole(string userId);
        Task UpdateRoles(List<UserRoles> userRoles);
        Task Activate(string userId);
        Task<ApplicationUsers> GetUserInfo(string name);
        List<string> GetRoles(ApplicationUsers user);
    }
}
