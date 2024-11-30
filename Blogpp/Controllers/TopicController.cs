using Blogpp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blogpp.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult insert(TopicDTO topicDTO)
        {
            return View();
        }
    }
}
