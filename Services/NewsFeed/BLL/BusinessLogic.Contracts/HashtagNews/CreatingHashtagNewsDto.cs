using BusinessLogic.Contracts.Hashtag;
using System;

namespace BusinessLogic.Contracts.HashtagNews
{
    public class CreatingHashtagNewsDto
    {
        public Guid HashtagId { get; set; }
        public Guid NewsId { get; set; }
        public CreatingHashtagDto Hashtag { get; set; }
    }
}
