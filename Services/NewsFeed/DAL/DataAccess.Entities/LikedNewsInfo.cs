using System;

namespace DataAccess.Entities
{
    public class LikedNewsInfo
    {
        public Guid NewsId { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}
