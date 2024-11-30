using Blogpp.Models;
using Microsoft.AspNetCore.Identity;

namespace Blogpp.Services
{
    public interface IAccountService
    {
        Task<string> checkLogin(SignInDTO signInDTO);
        Task<IdentityResult> AddRole(RoleDTO roleDTO);
        Task<string> insert(SignUpDTO signUpDTO);
        List<RoleDTO> GetAllRoles();
    }
}