using Microsoft.AspNetCore.Mvc;

namespace Swizzle.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         
        [Route("posts/{communityId}/{userId}/{postTitle}")]
        public IActionResult PostDetails(string communityId, string userId, string postId, string postTitle)
        {
            return View();
        }
    }
}
