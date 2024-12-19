namespace Swizzle.Models.Post
{
    public class PostCardViewModel
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VoteCount { get; set; }
        public string Community { get; set; }
        public string PosterName { get; set; }
        public string TimePosted { get; set; }
        public string CommentCount { get; set; }
        public bool HasMedia { get; set; }
        public string MediaType { get; set; } = "image";
        public string MediaUrl { get; set; }
    }
}
