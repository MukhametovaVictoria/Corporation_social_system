using System;

namespace BusinessLogic.Contracts.News
{
    public class NewsFilterDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
    }
}
