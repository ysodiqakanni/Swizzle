using Swizzle.Models.Post;
using System.Collections.Generic;

namespace Swizzle.Models.Community
{
    public class CommunityPageViewModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string LogoUrl { get; set; }
        public string MembersCount { get; set; }
        public string OnlineMembersCount { get; set; }
        public string CreatedDate { get; set; }


        public List<PostCardViewModel>? PagePosts { get; set; } = new List<PostCardViewModel>();
    }
}
