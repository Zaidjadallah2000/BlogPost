using Blogpp.Models;
using Blogpp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogpp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        public IActionResult Index()
        {
            return View("Login");
        }

        public async Task<IActionResult> Login(SignInDTO signInDTO)
        {
            var result = await accountService.checkLogin(signInDTO);
            if (result == "EmailNotFound")
            {
                ModelState.AddModelError("Email", "Email Not Correct.");
                return View("Login");
            }
            else if(result == "PassNotFound")
            {
                ModelState.AddModelError("Password", "Password Not Correct.");
                return View("Login");
            }
            else if(result == "Success")
            {
                return RedirectToAction("index", "Post");
            }
            else
            {
                return View("Login");
            }
        }
        public IActionResult NewUser()
        {
            List<RoleDTO> roles = accountService.GetAllRoles();
            VMUsers VMusers = new VMUsers();
            VMusers.Roles = roles;
            return View("NewUser", VMusers);
        }

        public async Task<IActionResult> AddNewUser(VMUsers vmUser)
        {
            var result = await accountService.insert(vmUser.UserApp);
            List<RoleDTO> roles = accountService.GetAllRoles();
            VMUsers VMusers = new VMUsers();
            VMusers.Roles = roles;
            return View("NewUser", VMusers);
        }
        public IActionResult Role()
        {
            return View("Role");
        }
        public async Task<IActionResult> NewRole(RoleDTO roleDTO)
        {
            var result = await accountService.AddRole(roleDTO);
            return View("Role");
        }

        public IActionResult GetRole()
        {
          List<RoleDTO> roles = accountService.GetAllRoles();
            return View(roles);
        }


        public async Task<IActionResult> Logout()
        {
            // تسجيل الخروج باستخدام المخطط الصحيح
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Account"); // إعادة التوجيه إلى صفحة تسجيل الدخول
        }


    }
}
