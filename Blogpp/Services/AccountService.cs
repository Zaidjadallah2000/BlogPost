using Blogpp.data;
using Blogpp.Models;
using Microsoft.AspNetCore.Identity;

namespace Blogpp.Services
{
    public class AccountService : IAccountService
    {
        private readonly BlogContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountService(BlogContext _context,
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            SignInManager<ApplicationUser> _signInManager
            )
        {
            context = _context;
            userManager = _userManager;
            roleManager = _roleManager;
            signInManager = _signInManager;
        }

        public async Task<string> checkLogin(SignInDTO signInDTO)
        {
            if (signInDTO.Email != null && signInDTO.Password != null)
            {
                var EmailValid = await userManager.FindByEmailAsync(signInDTO.Email);
                if ((EmailValid == null))
                {
                    return "EmailNotFound";
                }

                var passValid = await signInManager.PasswordSignInAsync(
    EmailValid,
    signInDTO.Password,
    false,
    false
    );
                if (passValid.Succeeded)
                {
                    return "Success";
                }
                else
                {
                    return "PassNotFound";
                }

            }
            else
            {
                return string.Empty;
            }
        }





        public async Task<string> insert(SignUpDTO signUpDTO)
        {
            ApplicationUser NewUser = new ApplicationUser();
            NewUser.name = signUpDTO.Name;
            NewUser.Email = signUpDTO.Email;
            NewUser.UserName = signUpDTO.UserName;
           
            var createUser = await userManager.CreateAsync(NewUser, signUpDTO.Password);
            if (createUser.Succeeded)
            {
                var resultToRole = await userManager.AddToRoleAsync(NewUser, signUpDTO.RoleId);
                if (resultToRole.Succeeded)
                {
                    return "Added Success";
                }
                else
                {
                    await userManager.DeleteAsync(NewUser);
                    return "faild";
                }
            }
            else
            {
                return "faild";
            }

        }
        public async Task<IdentityResult> AddRole(RoleDTO roleDTO)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleDTO.Name;
            var result = await roleManager.CreateAsync(role);
            return result;
        }

        public List<RoleDTO> GetAllRoles()
        {
            List<IdentityRole> role = context.Roles.ToList();
            List<RoleDTO> roles = new List<RoleDTO>();
            foreach (var item in role)
            {
                RoleDTO roleDTO = new RoleDTO();
                roleDTO.RoleId = item.Id;
                roleDTO.Name = item.Name;
                roles.Add(roleDTO);
            }
            return roles;
        }
    }
    }
