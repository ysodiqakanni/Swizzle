using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Swizzle.DTOs.Responses;
using Swizzle.Models.Post;
using Swizzle.Services;
using System.Net.Http;
using System.Security.Claims;

namespace Swizzle.Controllers
{
    public class PostsController : Controller
    {
        private readonly IHttpClientService _httpClient;

        public PostsController(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }
         
        [Route("posts/{communityId}/{userId}/{postTitle}")]
        public IActionResult PostDetails(string communityId, string userId, string postTitle)
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
            //var communities = await _httpClient.GetAsync<BaseApiResponse<List<CommunityListResponseDto>>>($"communities");
            var response = await _httpClient.GetAsync<List<CommunityListResponseDto>>("communities");

            // load communities
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        { 
            // validate post model
            if (string.IsNullOrEmpty(model?.Title1?.Trim()))
            {
                ViewBag.ErrorMessage = "Title is required!";
                return Json(new
                {
                    success = false,
                    Message = "Fill all required fields"
                }); 
            }
            var userId = "6649989b81da82407aa94584";
            model.CommunityId = "66499a5a463a83871675c01b";
            // fetch the loggedIn user ID: 6649989b81da82407aa94584 for test.
            // check community Id: 66499a5a463a83871675c01b for test.

            // maybe send back to the community page?

            // now make the api call here and check the response.
            Thread.Sleep(1000);

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("PostDetails", "Posts", new { communityId =model.CommunityId, userId=userId, postTitle=model.Title1 })
            });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([FromForm] CreatePostViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { success = false, message = "Invalid form data" });
        //    }

        //    try
        //    {
        //        var post = new Post
        //        {
        //            Title = model.Title,
        //            Description = model.Description,
        //            CommunityId = model.CommunityId,
        //            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) // Assuming you're using ASP.NET Identity
        //        };

        //        var result = await _postService.CreatePostAsync(post);

        //        return Json(new
        //        {
        //            success = true,
        //            redirectUrl = Url.Action("Details", "Post", new { id = result.Id })
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return Json(new { success = false, message = "Failed to create post" });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateWithGraphics([FromForm] CreatePostViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { success = false, message = "Invalid form data" });
        //    }

        //    try
        //    {
        //        var post = new Post
        //        {
        //            Title = model.Title,
        //            Description = model.Description,
        //            CommunityId = model.CommunityId,
        //            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        //        };

        //        // Handle media files
        //        if (model.MediaFiles != null && model.MediaFiles.Any())
        //        {
        //            var mediaUrls = new List<string>();
        //            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

        //            foreach (var file in model.MediaFiles)
        //            {
        //                if (file.Length > 0)
        //                {
        //                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //                    using (var stream = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        await file.CopyToAsync(stream);
        //                    }

        //                    mediaUrls.Add("/uploads/" + uniqueFileName);
        //                }
        //            }

        //            post.MediaUrls = mediaUrls;
        //        }

        //        var result = await _postService.CreatePostAsync(post);

        //        return Json(new
        //        {
        //            success = true,
        //            redirectUrl = Url.Action("Details", "Post", new { id = result.Id })
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return Json(new { success = false, message = "Failed to create post" });
        //    }
        //}

    }
}
