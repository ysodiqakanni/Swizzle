using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Swizzle.DTOs.Requests;
using Swizzle.DTOs.Responses;
using Swizzle.Models.Post;
using Swizzle.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

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

        [Route("posts/{communityName}/{postId}/{postTitle}")]
        public async Task<IActionResult> Details(string communityName, string postId, string postTitle)
        {
            // Todo: Maybe return null when not found. And display a cool content.
            if(string.IsNullOrWhiteSpace(postId))
            {
                return NotFound();
            }
            var singlePostLiteResponse = await _httpClient.GetAsync<SinglePostDetailResponseDto>($"posts/{postId}");
            if (singlePostLiteResponse.Success)
            {
                
                var postObject = MapToPostCardModel(singlePostLiteResponse.Data);
                var model = new PostDetailPageViewModel()
                {
                    Post = postObject,
                    Comments = new List<CommentModel>()
                };
                return View(model);
            }
            return NotFound();
        }
        private PostCardModel MapToPostCardModel(SinglePostDetailResponseDto dto)
        {
            if (dto == null) return null;

            return new PostCardModel
            {
                PostId = dto.ID,
                Title = dto.Title,
                Content = dto.Content?.Body ?? string.Empty,
                VoteCount = dto.Stats?.VoteCountStr,
                Community = dto.CommunityName,
                PosterName = dto.Author?.UserName ?? "Unknown",
                TimePosted = dto.CreatedAt.ToString("g"), // "g" provides a concise date-time format
                CommentCount = dto.Stats?.CommentCountStr,
                HasMedia = dto.Content?.MediaUrls != null && dto.Content.MediaUrls.Any(),
                MediaType = dto.Content?.Type ?? "text", // Default media type to "text"
                MediaUrl = dto.Content?.MediaUrls?.FirstOrDefault() ?? string.Empty, // Get the first media URL if available
                CommunityId = dto.CommunityID
            };
        }


        [Route("posts/d/{communityId}/{userId}/{postTitle}")]
        public IActionResult PostDetails(string communityId, string userId, string postTitle)
        {
            var post = new PostDetailPageViewModel()
            {
                Post = new PostCardModel()
                {
                    PostId = "asd23234",
                    CommunityId = communityId,
                    Title = "10 Must-Know CSS Grid Techniques for Modern Layouts",
                    Content = "Here's a quick guide to using CSS Grid effectively in your projects. I've been using these techniques in production...",
                    HasMedia = false,
                    Community = "webDev",
                    PosterName = "kompiler",
                    TimePosted = "8 hours ago",
                    CommentCount = "45",
                    VoteCount = "49"
                },
                Comments = new List<CommentModel>()
                {
                    new CommentModel()
                    {
                        Id = "xsds122",
                        Content = "This is a great implementation! Have you considered using SignalR for real-time updates?",
                        PosterName = "dammyrez123",
                        TimePosted = "2 hours ago",
                        VoteCount = "12",
                        Replies = new List<CommentModel>()
                        {
                            new CommentModel()
                            {
                                Id = "xsds1221",
                                Content = "Reply 1",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            },
                            new CommentModel()
                            {
                                Id = "xsds1222",
                                Content = "Reply 2",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            },
                            new CommentModel()
                            {
                                Id = "xsds123",
                                Content = "Reply 3",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            }
                        }
                    },
                    new CommentModel()
                    {
                        Id = "xsds123",
                        Content = "This is a great implementation! Have you considered using SignalR for real-time updates?",
                        PosterName = "dammyrez123",
                        TimePosted = "2 hours ago",
                        VoteCount = "700",
                        Replies = new List<CommentModel>()
                        {
                            new CommentModel()
                            {
                                Id = "xsds12202",
                                Content = "Reply 1",
                                PosterName = "dammyre00",
                                TimePosted = "1 hours ago",
                                VoteCount = "1",
                            }
                        }
                    },
                    new CommentModel()
                    {
                        Id = "xsds135",
                        Content = "This is a really great implementation! Have you considered using SignalR for real-time updates?",
                        PosterName = "dammyrez123",
                        TimePosted = "2 hours ago",
                        VoteCount = "1300",
                        Replies = new List<CommentModel>()
                        {
                            new CommentModel()
                            {
                                Id = "xsds1229",
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
            if (string.IsNullOrEmpty(model?.Description?.Trim()))
            {
                ViewBag.ErrorMessage = "Content is required!";
                return Json(new
                {
                    success = false,
                    Message = "Fill all required fields"
                });
            }
            var userId = "676a3d81755343b5af07c68f";
            //model.CommunityId = "66499a5a463a83871675c01b";
            // fetch the loggedIn user ID: 6649989b81da82407aa94584 for test.
            // check community Id: 66499a5a463a83871675c01b for test.

            // maybe send back to the community page?

            // now make the api call here and check the response.
            var payload = new CreatePostRequestDto()
            {
                CommunityId = model.CommunityId,
                CommunityName = model.CommunityName,
                Title = model.Title1,
                content = model.Description,
                UserId = userId,
                PostType = "text"
            };
            var response = await _httpClient.PostAsync<string>("posts", payload);
            
            if (response.Success)
            {
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Details", "Posts", new { communityName = model.CommunityName, postId = response.Data, postTitle = model.Title1.ToUrlFriendly() })
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    Message = response.Message
                });
            }  
        }

        // Todo: needs authorize
        [HttpPost]
        [Route("posts/{postId}/comment")]
        public async Task<IActionResult> AddComment(string postId, CreateCommentViewModel model)
        {
            // for a new comment, we need
            // the postId, content
            try
            {
                if (string.IsNullOrEmpty(model.Content))
                {
                    return BadRequest("Invalid input data.");
                }

                Thread.Sleep(1000);
                var comment = new CommentModel()
                {
                    Id = "xsds122",
                    Content = model.Content,
                    PosterName = "dammyrez123",
                    TimePosted = "Just now",
                    VoteCount = "0",
                    Replies = new List<CommentModel>()
                };
                return PartialView("_CommentCardPartial", comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the comment.");
            }
             
        }

        // Todo: needs authorize
        [HttpPost]
        [Route("posts/{postId}/reply")]
        public async Task<IActionResult> AddReply(string postId, CreateReplyViewModel model)
        {
            // need the postId, commentId, replyId?, content
            try
            {
                if (string.IsNullOrEmpty(model.Content))
                {
                    return BadRequest("Invalid input data.");
                }

                Thread.Sleep(1000);
                // the api should return the new reply Id, postername,
                var comment = new CommentModel()
                {
                    Id = "rep9283",
                    Content = model.Content,
                    PosterName = "dammyrez123",
                    TimePosted = "Just now",
                    VoteCount = "0",
                };
                return PartialView("_ReplyCardPartial", comment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the comment.");
            }
        }

        [HttpPost]
        [Route("posts/{postId}/vote")]
        public async Task<IActionResult> Upvote(string postId, VoteViewModel model)
        {
            // for post, we need the postid
            return Json(new
            {
                success = false,
                newVoteCount = 23
            }); 
        }

        [HttpPost]
        [Route("posts/{postId}/vote/{commentId}")]
        public async Task<IActionResult> VoteComment(string postId, string commentId, VoteViewModel model)
        {
            // for post, we need the postid
            return Json(new
            {
                success = false,
                newVoteCount = 23
            });
        }

        [HttpPost]
        [Route("posts/{postId}/vote/{commentId}/{replyId}")]
        public async Task<IActionResult> VoteReply(string postId, string commentId, string replyId, VoteViewModel model)
        {
            // for post, we need the postid
            return Json(new
            {
                success = false,
                newVoteCount = 23
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
    public class VoteViewModel
    {
        public bool IsUpvote { get; set; }
    }
    
}
