using System.Collections.Generic;

namespace Swizzle.Models.Post
{
    public class PostDetailPageViewModel
    {
        public PostCardModel Post { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
    public class PostCardModel
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string VoteCount { get; set; }
        public string Community { get; set; }
        public string PosterName { get; set; }
        public string TimePosted { get; set; }
        public string CommentCount { get; set; }
        public bool HasMedia { get; set; }
        public string MediaType { get; set; } = "image";
        public string MediaUrl { get; set; }
        public string CommunityId { get; internal set; }
    }
    public class CommentModel
    {
        public string Content { get; set; }
        public string PosterName { get; set; }
        public string TimePosted { get; set; }
        public string VoteCount { get; set; }

        public List<CommentModel> Replies { get; set; }
        public string Id { get; internal set; }
    }
}
