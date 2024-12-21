using Swizzle.Models.Post;

namespace Swizzle.Models.User
{
    public class UserPublicProfilePageViewModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
        public string CoverPhotoUrl { get; set; }
        public string LogoUrl { get; set; }
        public string PostsCount { get; set; }
        public string CommentsCount { get; set; }
        public string JoinedDate { get; set; }


        public List<PostCardViewModel>? PagePosts { get; set; } = new List<PostCardViewModel>();
    }
}
