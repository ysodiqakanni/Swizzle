using Microsoft.AspNetCore.Mvc;
using Swizzle.DTOs.Responses;
using Swizzle.Models.Community;
using Swizzle.Models.Post;
using Swizzle.Services;
using System.Net.Http;

namespace Swizzle.Controllers
{
    [Route("c")]
    public class CommunitiesController : Controller
    {
        private readonly IHttpClientService _httpClient;

        public CommunitiesController(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("{communityName}/{title}")]
        public async Task<IActionResult> Details()
        {
            var model = new CommunityPageViewModel()
            {
                Name = "programming",
                Title = "ComputerProgramming",
                Description = "Computer Programming: A place for programming and tech discussions. News, articles, and discussions about programming, software development, and related topics.\r\n\r\n",
                CoverPhotoUrl = "https://picsum.photos/1200/192",
                LogoUrl = "https://picsum.photos/80/80",
                MembersCount = "23.1m",
                OnlineMembersCount = "600",
                CreatedDate = "Jan 25, 2008"
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
                    Title = "10 Must-Know CSS Grid Techniques for Modern Layouts",
                    Description = "Here's a quick guide to using CSS Grid effectively in your projects. I've been using these techniques in production...",
                    HasMedia = false,
                    Community = "webDev",
                    PosterName = "kompiler",
                    TimePosted = "8 hours ago",
                    CommentCount = "45",
                    VoteCount = "4.9k"
                },
                new PostCardViewModel
                {
                    Title = "Custom work- how do you make customers understand pricing",
                    Description = "I have small business in which I make hand sewn home goods like lampshades and pillows. I just gave a local business a 75% discount on some custom work and they still haggled and were extremely put out by the fact that I don’t just charge for materials. How do you kindly make people understand that your hand work and time is valuable?\r\n\r\n",
                    HasMedia = false,
                    Community = "webDev",
                    PosterName = "kompiler",
                    TimePosted = "1 minute ago",
                    CommentCount = "6",
                    VoteCount = "98"
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

        [Route("search")]
        public async Task<IActionResult> Search(string query)
        { 
            var response = await _httpClient.GetAsync<List<CommunityListResponseDto>>("communities");
            var filtered = response.Data.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToList();
            if (response.Success)
            {
                return Json(new
                {
                    success = true,
                    data = filtered.Any() ? filtered : response.Data.Take(4)
                });
            }
            return Json(new
            {
                success = false,
                message = "Error loading page data. Please retry after a few minutes."
            });
        }
    }
}
