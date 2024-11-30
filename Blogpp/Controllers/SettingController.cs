using Blogpp.Models;
using Blogpp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogpp.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly ISettingService setingService;

        public SettingController(ISettingService _setingService)
        {
            setingService = _setingService;
        }
        public IActionResult Index()
        {
            return View("index");
        }
        public async Task<IActionResult> updateProfile(SettingDTO settingDTO)
                    {
            
           var result = await setingService.UpdateProfile(settingDTO);
            if(result== "User not found")
            {
                ModelState.AddModelError("Name", "User Not Found");
            }else if (result == "currentPassError")
            {
                ModelState.AddModelError("CurrentPassword", "Current Password Incorrect");
            }
            return View("index");
        }
    }
}
