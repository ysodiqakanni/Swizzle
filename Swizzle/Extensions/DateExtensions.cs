using System;

namespace Swizzle.Extensions
{ 
    public static class RelativeDateTImeConverter
    {
        public static string ToHumanRelativeTime(this DateTime utcDateTime) 
        {
            var timeSpan = DateTime.UtcNow - utcDateTime;

            if (timeSpan <= TimeSpan.FromSeconds(1))
            {
                return "just now";
            }
            if (timeSpan <= TimeSpan.FromSeconds(59))
            {
                return timeSpan.Seconds == 1 ? "1 sec ago" : $"{timeSpan.Seconds} secs ago";
            }
            if (timeSpan <= TimeSpan.FromMinutes(59))
            {
                return timeSpan.Minutes == 1 ? "1 min ago" : $"{timeSpan.Minutes} mins ago";
            }
            if (timeSpan <= TimeSpan.FromHours(23))
            {
                return timeSpan.Hours == 1 ? "1 hr ago" : $"{timeSpan.Hours} hrs ago";
            }
            if (timeSpan <= TimeSpan.FromDays(29))
            {
                return timeSpan.Days == 1 ? "1 day ago" : $"{timeSpan.Days} days ago";
            }
            if (timeSpan <= TimeSpan.FromDays(365))
            {
                var months = Math.Floor((double)timeSpan.Days / 30);
                return months == 1 ? "1 month ago" : $"{months} months ago";
            }

            var years = Math.Floor((double)timeSpan.Days / 365);
            return years == 1 ? "1 year ago" : $"{years} years ago";
        }
    }
}
