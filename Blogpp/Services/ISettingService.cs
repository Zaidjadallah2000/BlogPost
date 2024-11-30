using Blogpp.Models;
using Microsoft.AspNetCore.Identity;

namespace Blogpp.Services
{
    public interface ISettingService
    {
        Task<string> UpdateProfile(SettingDTO setting);
    }
}