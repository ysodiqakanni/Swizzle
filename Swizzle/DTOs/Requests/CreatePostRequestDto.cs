using System.Text.Json.Serialization;

namespace Swizzle.DTOs.Requests
{
    public class CreatePostRequestDto
    {
        public string Title { get; set; }
        public string content { get; set; }
        [JsonPropertyName("community_id")]
        public string CommunityId { get; set; }
        public string CommunityName { get; set; }

        [JsonPropertyName("created_by_user_id")]    // Todo: Remove this and let api fetch userId from the token.
        public string UserId { get; set; }
        public string PostType { get; set; }
    }
}
