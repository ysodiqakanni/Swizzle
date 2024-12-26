namespace Swizzle.Models.Post
{
    public class PostCardViewModel
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string TitleUrlFriendly
        {
            get { return Title.ToUrlFriendly(); }
        }
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

    public static class StringExtensions
    {
        public static string ToUrlFriendly(this string text)
        {
            return text.ToLower()
                      .Replace(" ", "-")
                      .Replace("&", "and")
                      .Trim()
                      .Trim('-');
                      //.RegexReplace("[^a-zA-Z0-9-]", "");
        }
    }
}
