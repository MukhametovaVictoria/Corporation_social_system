using BusinessLogic.Contracts.HashtagNews;
using BusinessLogic.Contracts.Picture;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Contracts.News
{
    public class UpdatingNewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public bool IsPublished { get; set; }
        public bool IsArchived { get; set; }
        public List<CreatingHashtagNewsDto> HashtagNewsList { get; set; }
        public List<UpdatingPictureDto> PictureList { get; set; }
    }
}
