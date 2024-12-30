namespace Swizzle.Models.Post
{
    public class CreateCommentViewModel
    {
        public string PostId { get; set; }
        public string Content { get; set; }
        // Note: api should retrive the user Id.
    }
    public class CreateReplyViewModel
    {
        public string PostId { get; set; }
        public string CommentidId { get; set; }
        public string Content { get; set; }
        // Note: api should retrive the user Id.
    }
}
