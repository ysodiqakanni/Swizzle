using System.ComponentModel.DataAnnotations;

namespace Swizzle.Models.Post
{
    public class CreatePostViewModel
    {
        [Required]
        public string CommunityId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string CommunityName { get; set; }
    }
}
