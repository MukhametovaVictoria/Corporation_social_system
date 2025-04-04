﻿using FrontEnd.Models.Picture;

namespace FrontEnd.Models
{
    public class UpdatingNewsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public List<CreatingHashtagNewsModel>? HashtagNewsList { get; set; }
        public List<UpdatingPictureModel>? PictureList { get; set; }
    }
}
