namespace FrontEnd.Models
{
    public class NewsCommentInfoModel
    {
        public Guid NewsId { get; set; }
        public Guid CommentId { get; set; }
        public bool IsAuthor { get; set; }
    }
}
