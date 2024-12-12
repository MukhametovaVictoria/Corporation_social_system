namespace FrontEnd.Models
{
    public class HashtagNewsModel
    {
        public Guid Id { get; set; }
        public Guid HashtagId { get; set; }
        public Guid NewsId { get; set; }
        public HashtagModel? Hashtag { get; set; }
        public NewsModel? News { get; set; }
    }
}
