using BusinessLogic.Contracts.Hashtag;
using BusinessLogic.Contracts.News;
using System;

namespace BusinessLogic.Contracts.HashtagNews
{
    public class HashtagNewsDto
    {
        public Guid Id { get; set; }
        public Guid HashtagId { get; set; }
        public Guid NewsId { get; set; }
        public HashtagDto Hashtag { get; set; }
        public NewsDto News { get; set; }
    }
}
