using System;

namespace DataAccess.Entities
{
    public class NewsComment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Employee Author { get; set; }
        public Guid NewsId { get; set; }
        public virtual News News { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
