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
        public string PostId { get; set; }      // The ID of the original post. Required!
        public string? CommentId { get; set; }  // The Comment under which the reply is being added.
        public string? ReplyToId { get; set; }  // If 
        public string Content { get; set; }
        // Note: api should retrive the user Id.
    }
}
