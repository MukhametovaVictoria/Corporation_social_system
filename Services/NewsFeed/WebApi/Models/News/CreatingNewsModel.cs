using System;
using System.Collections.Generic;
using WebApi.Models.HashtagNews;
using WebApi.Models.NewsComment;
using WebApi.Models.Picture;

namespace WebApi.Models.News
{
    public class CreatingNewsModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public Guid AuthorId { get; set; }
        public List<CreatingHashtagNewsModel> HashtagNewsList { get; set; }
        public List<CreatingPictureModel> PictureList { get; set; }
    }
}
