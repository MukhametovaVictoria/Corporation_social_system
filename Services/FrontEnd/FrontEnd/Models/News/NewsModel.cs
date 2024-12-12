using FrontEnd.Models.Picture;

namespace FrontEnd.Models
{
    public class NewsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsArchived { get; set; }
        public bool IsPublished { get; set; }
        public EmployeeModel? Author { get; set; }
        public List<NewsCommentModel>? NewsCommentList { get; set; }
        public List<HashtagNewsModel>? HashtagNewsList { get; set; }
        public List<PictureModel>? PictureList { get; set; }
    }
}
