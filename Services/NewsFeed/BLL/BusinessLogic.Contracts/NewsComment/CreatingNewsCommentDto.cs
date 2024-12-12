using System;

namespace BusinessLogic.Contracts.NewsComment
{
    public class CreatingNewsCommentDto
    {
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public Guid NewsId { get; set; }
    }
}
