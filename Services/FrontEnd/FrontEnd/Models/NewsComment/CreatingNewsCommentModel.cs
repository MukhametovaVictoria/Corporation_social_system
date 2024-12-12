namespace FrontEnd.Models
{
    public class CreatingNewsCommentModel
    {
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public Guid NewsId { get; set; }
    }
}
