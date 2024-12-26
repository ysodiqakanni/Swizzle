using Swizzle.Models.Post;

namespace Swizzle.Models
{
    public class HomeViewModel
    {
        public List<PostCardViewModel>? TimeLinePosts { get; set; } = new List<PostCardViewModel>();
    }
}
