using System;

namespace DataAccess.Entities
{
    public class NewsCommentInfo
    {
        public Guid NewsId { get; set; }
        public Guid CommentId { get; set; }
        public bool IsAuthor { get; set; }
    }
}
