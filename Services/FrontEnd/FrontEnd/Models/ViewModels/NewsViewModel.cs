using FrontEnd.Models.Picture;

namespace FrontEnd.Models
{
    public class NewsViewModel : NewsModel
    {
        public string AuthorFullName { get; set; }
        public int Likes { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public List<HashtagModel>? HashtagList { get; set; }
        public string? Hashtags { get; set; }
        public bool IsAuthor {  get; set; }
    }
}
