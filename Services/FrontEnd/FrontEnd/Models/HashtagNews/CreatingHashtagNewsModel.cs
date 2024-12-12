namespace FrontEnd.Models
{
    public class CreatingHashtagNewsModel
    {
        public Guid HashtagId { get; set; }
        public Guid NewsId { get; set; }
        public CreatingHashtagModel? Hashtag { get; set; }
    }
}
