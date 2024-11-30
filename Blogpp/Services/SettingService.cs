using Blogpp.data;
using Blogpp.Models;
using Microsoft.AspNetCore.Identity;

namespace Blogpp.Services
{
    public class SettingService : ISettingService
    {
        private readonly BlogContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public SettingService(BlogContext _context,
            UserManager<ApplicationUser> _userManager
            )
        {
            context = _context;
            userManager = _userManager;
        }
        public async Task<string> UpdateProfile(SettingDTO setting)
        {
            var FindUser = await userManager.FindByIdAsync(setting.id);
            if (FindUser == null)
            {
                return  "User not found";

            }
          
            FindUser.name = setting.Name;
            var result = await userManager.ChangePasswordAsync(FindUser, setting.CurrentPassword, setting.NewPassword);
            if (!result.Succeeded)
            {
                return "currentPassError";
            }
           
            var updateResult = await userManager.UpdateAsync(FindUser);
            if (updateResult.Succeeded)
            {
                return "success";
            }
            else
            {
                return "faild";
            }
        }
    }
}
