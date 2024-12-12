using System;

namespace WebApi.Models.NewsComment
{
    public class NewsCommentFilterModel
    {
        public string Content { get; set; }
        public Guid AuthorId { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }
    }
}
