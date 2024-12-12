using System;

namespace BusinessLogic.Contracts.NewsComment
{
    public class NewsCommentFilterDto
    {
        public string Content { get; set; }
        public Guid AuthorId { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }
    }
}
