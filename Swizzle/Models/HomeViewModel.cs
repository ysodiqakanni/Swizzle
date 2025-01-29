using Swizzle.Models.Post;
using System.Collections.Generic;

namespace Swizzle.Models
{
    public class HomeViewModel
    {
        public List<PostCardViewModel>? TimeLinePosts { get; set; } = new List<PostCardViewModel>();
    }
}
