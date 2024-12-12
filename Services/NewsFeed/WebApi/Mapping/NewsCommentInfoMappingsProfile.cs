using AutoMapper;
using BusinessLogic.Contracts.NewsComment;
using WebApi.Models.NewsComment;

namespace WebApi.Mapping
{
    public class NewsCommentInfoMappingsProfile : Profile
    {
        public NewsCommentInfoMappingsProfile()
        {
            CreateMap<NewsCommentInfoDto, NewsCommentInfoModel>();
        }
    }
}
