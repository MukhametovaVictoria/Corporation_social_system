using System;

namespace BusinessLogic.Contracts.NewsComment
{
    public class NewsCommentInfoDto
    {
        public Guid NewsId { get; set; }
        public Guid CommentId { get; set; }
        public bool IsAuthor { get; set; }
    }
}
