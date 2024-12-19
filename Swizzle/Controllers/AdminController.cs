using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Swizzle.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Administrator22")]
        public IActionResult Metrics()
        {
            return View();
        }
    }//https://localhost:44311/Authentication/Login
}
