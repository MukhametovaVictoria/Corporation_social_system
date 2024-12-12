namespace FrontEnd.Models
{
    public class LikedNewsInfoModel
    {
        public Guid NewsId { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}
