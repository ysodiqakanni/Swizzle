using Swizzle.Extensions;
using Swizzle.Models.Post;
using System;
using System.Text.Json.Serialization;
using Swizzle.Extensions;

namespace Swizzle.DTOs.Responses
{
    public class RecentPostsResponseDtoOld
    {
        [JsonPropertyName("id")]
        public string PostId { get; set; }
        [JsonPropertyName("id")]
        public string Title { get; set; }
        public string Name { get; set; }    // Old. Holds the main body content
        public AuthorResponse Author { get; set; }
        public string Description { get; set; }
        public string VoteCount { get; set; }
        public string Community { get; set; }
        public string PosterName { get; set; }
        public string TimePosted { get; set; }
        public string CommentCount { get; set; }
        public bool HasMedia { get; set; }
        public string MediaType { get; set; } = "image";
        public string MediaUrl { get; set; }

        public string TitleUrlFriendly
        {
            get { return Title.ToUrlFriendly(); }
        }
    }
    public class AuthorResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
     
    public class RecentPostsResponseDto
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("CommunityId")]
        public string CommunityId { get; set; }

        [JsonPropertyName("CommunityName")]
        public string CommunityName { get; set; }

        [JsonPropertyName("Content")]
        public Content Content { get; set; }

        [JsonPropertyName("Author")]
        public Author Author { get; set; }

        [JsonPropertyName("Metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("Stats")]
        public Stats Stats { get; set; }
    }
     

    public class SinglePostDetailResponseDto
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string CommunityID { get; set; }
        public string CommunityName { get; set; }
        public Author Author { get; set; }
        public Content Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Stats Stats { get; set; }
    } 

    public class Content
    {
        [JsonPropertyName("Body")]
        public string Body { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("MediaUrls")]
        public string[] MediaUrls { get; set; }
    }

    public class Author
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        public string UserName { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        public string TimePostedStr
        {
            get
            { 
                return CreatedAt.ToHumanRelativeTime();
            }
        }
    }

    public class Stats
    {
        [JsonPropertyName("upvotes")]
        public int upvotes { get; set; }

        [JsonPropertyName("downvotes")]
        public int downvotes { get; set; }

        [JsonPropertyName("commentCount")]
        public int commentCount { get; set; }

        public string CommentCountStr { get
            {
                return commentCount.ToString();
            }
        }
        public string VoteCountStr
        {
            get
            {
                return (upvotes - downvotes).ToString();
            }
        }
    }
}
