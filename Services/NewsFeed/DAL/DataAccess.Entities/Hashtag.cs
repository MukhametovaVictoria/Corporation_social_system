using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Hashtag : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<HashtagNews> HashtagNewsList { get; set; }
    }
}
