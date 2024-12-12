using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class News : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsArchived { get; set; }
        public bool IsPublished { get; set; }
        public virtual Employee Author { get; set; }
        public virtual ICollection<NewsComment> NewsCommentList { get; set; }
        public virtual ICollection<HashtagNews> HashtagNewsList { get; set; }
        public virtual ICollection<Picture> PictureList { get; set; }

    }
}
