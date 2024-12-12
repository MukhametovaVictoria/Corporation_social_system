using System;

namespace BusinessLogic.Contracts.LikedNews
{
    public class LikedNewsInfoDto
    {
        public Guid NewsId { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}
