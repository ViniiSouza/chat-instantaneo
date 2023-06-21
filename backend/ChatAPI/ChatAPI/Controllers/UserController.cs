using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
