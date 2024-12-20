using Microsoft.AspNetCore.Mvc;
using Swizzle.Models.Post;

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
            var post = new PostDetailPageViewModel()
            {
                Post = new PostCardModel()
                {
                    Title = "10 Must-Know CSS Grid Techniques for Modern Layouts",
                    Content = "Here's a quick guide to using CSS Grid effectively in your projects. I've been using these techniques in production...",
                    HasMedia = false,
                    Community = "webDev",
                    PosterName = "kompiler",
                    TimePosted = "8 hours ago",
                    CommentCount = "45",
                    VoteCount = "4.9k"
                },
                Comments = new List<CommentModel>()
                {
                    new CommentModel()
                    {
                        Content = "This is a great implementation! Have you considered using SignalR for real-time updates?",
                        PosterName = "dammyrez123",
                        TimePosted = "2 hours ago",
                        VoteCount = "1.2k",
                        Replies = new List<CommentModel>()
                        {
                            new CommentModel()
                            {
                                Content = "Reply 1",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            },
                            new CommentModel()
                            {
                                Content = "Reply 2",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            },
                            new CommentModel()
                            {
                                Content = "Reply 3",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            }
                        }
                    },
                    new CommentModel()
                    {
                        Content = "This is a great implementation! Have you considered using SignalR for real-time updates?",
                        PosterName = "dammyrez123",
                        TimePosted = "2 hours ago",
                        VoteCount = "1.2k",
                        Replies = new List<CommentModel>()
                        {
                            new CommentModel()
                            {
                                Content = "Reply 1",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            }
                        }
                    },
                    new CommentModel()
                    {
                        Content = "This is a really great implementation! Have you considered using SignalR for real-time updates?",
                        PosterName = "dammyrez123",
                        TimePosted = "2 hours ago",
                        VoteCount = "1.2k",
                        Replies = new List<CommentModel>()
                        {
                            new CommentModel()
                            {
                                Content = "Yes! SignalR is actually on my roadmap for the next iteration. Planning to use it for live vote counts and new comments.",
                                PosterName = "kompiler",
                                TimePosted = "14 hours ago",
                                VoteCount = "31",
                            }
                        }
                    },
                }
            };
            return View(post);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
