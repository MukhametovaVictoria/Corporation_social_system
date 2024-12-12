using BusinessLogic.Contracts.Employee;
using BusinessLogic.Contracts.News;
using System;

namespace BusinessLogic.Contracts.NewsComment
{
    public class NewsCommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public EmployeeDto Author { get; set; }
        public Guid NewsId { get; set; }
        public NewsDto News { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
