using System;
using System.Collections.Generic;
using WebApi.Models.HashtagNews;

namespace WebApi.Models.Hashtag
{
    public class HashtagModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<HashtagNewsModel> HashtagNewsList { get; set; }
    }
}
