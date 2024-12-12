namespace FrontEnd.Models
{
    public class NewsCommentModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public EmployeeModel? Author { get; set; }
        public Guid NewsId { get; set; }
        public NewsModel? News { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
