using AutoMapper;
using BusinessLogic.Contracts.NewsComment;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class NewsCommentInfoMappingsProfile : Profile
    {
        public NewsCommentInfoMappingsProfile()
        {
            CreateMap<NewsCommentInfo, NewsCommentInfoDto>();
        }
    }
}
