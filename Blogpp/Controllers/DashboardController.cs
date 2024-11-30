using Microsoft.AspNetCore.Mvc;

namespace Blogpp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("index");
        }
    }
}
