namespace FrontEnd.Models
{
    public class HashtagModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<HashtagNewsModel>? HashtagNewsList { get; set; }
    }
}
