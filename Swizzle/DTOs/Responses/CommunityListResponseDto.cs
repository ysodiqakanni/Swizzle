using System.Text.Json.Serialization;

namespace Swizzle.DTOs.Responses
{
    public class CommunityListResponseDto
    {
        public string id { get; set; }
        public string name { get; set; }
        [JsonPropertyName("avatarUrl")]
        public string AvatarUrl { get; set; }
    }

    public class CommunityListResponseDto1
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("AvatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("MembersCount")]
        public int MembersCount { get; set; }

        [JsonPropertyName("CreatedByUserId")]
        public string CreatedByUserId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
