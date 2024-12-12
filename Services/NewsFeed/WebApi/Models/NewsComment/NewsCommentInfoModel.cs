using System;

namespace WebApi.Models.NewsComment
{
    public class NewsCommentInfoModel
    {
        public Guid NewsId { get; set; }
        public Guid CommentId { get; set; }
        public bool IsAuthor { get; set; }
    }
}
