using Microsoft.AspNetCore.Mvc;
using Swizzle.Models.Community;
using Swizzle.Models.Post;
using Swizzle.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Swizzle.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Public profile page for the user. We can determine access level later.
        /// Private, public, restricted to (followers, community, etc)
        /// </summary>
        /// <returns></returns>
        [Route("{username}")]
        public async Task<IActionResult> PublicProfile(string username)
        {
            // Todo: find the user and load from the api.
            // if no user, render a friendly error message on page.

            var model = new UserPublicProfilePageViewModel()
            {
                UserName = "kompiler",
                FullName = "Sodiq Yusuff",
                About = "An alledged software guru who built Swizzle.",
                CoverPhotoUrl = "https://picsum.photos/1200/192",
                LogoUrl = "https://picsum.photos/80/80",
                PostsCount = "890",
                CommentsCount = "2.5k",
                JoinedDate = "Jan 25, 2024"
            };
            var posts = new List<PostCardViewModel>()
            {
                new PostCardViewModel
                {
                    Title = "Building a Swizzle Clone with .NET MVC and Bootstrap",
                    HasMedia = true,
                    MediaType = "image",
                    MediaUrl = "https://picsum.photos/400/200",
                    Community = "programming",
                    PosterName = "kompiler",
                    TimePosted = "8 hours ago",
                    CommentCount = "128",
                    VoteCount = "14.2k"
                },
                new PostCardViewModel
                {
                    Title = "In real deep",
                    Description = "I really went all in on my SaaS start up. Mid-30s, married with kids. Left a $350k a year job. Dumped $300k+ into it. Ended up taking a lot longer than I thought and it cost me so much to maintain the biz and pay for my cost of living from savings for 3 years without an income. Ended up raising a pre-seed, but it wasn’t enough to pay myself anything. Ended up selling my second home to use the equity for living expenses. I’m now $150k in debt (0% interest), and down to my primary residence and a modest retirement account. We’ve rented out the house and moved to a cheaper place, but it’s not enough.\r\n\r\n",
                    HasMedia = false,
                    Community = "webDev",
                    PosterName = "kompiler",
                    TimePosted = "1 minute ago",
                    CommentCount = "6",
                    VoteCount = "98"
                },
                new PostCardViewModel
                {
                    Title = "AITA for kicking my fiancée out of the house after finding out she lied about being infertile?",
                    Description = "My fiancée (30F) and I (33M) have been together for four years and engaged for one. Early in our relationship, she told me she was infertile due to a medical condition she had in her teens. I was fine with this, as I’ve never really wanted biological kids and figured we could explore adoption if we ever changed our minds.\r\n\r\nFast forward to last week. I came home to a positive pregnancy test sitting on the bathroom counter. At first, I thought she might’ve been helping a friend, but when I confronted her, she broke down and admitted she’s not infertile. She never was. Apparently, she lied because she thought I’d leave her if I knew...",
                    HasMedia = false,
                    Community = "webDev",
                    PosterName = "kompiler",
                    TimePosted = "17 hour ago",
                    CommentCount = "3k",
                    VoteCount = "13k"
                }
            };

            model.PagePosts = posts;

            return View(model);
        }
    }
}
