using System.Collections.Generic;
using System;
using BusinessLogic.Contracts.News;
using BusinessLogic.Contracts.NewsComment;
using BusinessLogic.Contracts.Picture;

namespace BusinessLogic.Contracts.Employee
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public List<NewsDto> NewsList { get; set; }
        public List<NewsCommentDto> NewsCommentList { get; set; }
        public List<PictureDto> PictureList { get; set; }
    }
}
