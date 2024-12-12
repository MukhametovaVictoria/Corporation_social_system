using System;

namespace DataAccess.Entities
{
    public class HashtagNews : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid HashtagId { get; set; }
        public Guid NewsId { get; set; }
        public virtual Hashtag Hashtag { get; set; }
        public virtual News News { get; set; }
    }
}
