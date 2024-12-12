using BusinessLogic.Contracts.HashtagNews;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Contracts.Hashtag
{
    public class HashtagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<HashtagNewsDto> HashtagNewsList { get; set; }
    }
}
