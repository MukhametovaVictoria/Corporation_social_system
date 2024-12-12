using AutoMapper;
using BusinessLogic.Contracts.NewsComment;
using WebApi.Models.NewsComment;

namespace WebApi.Mapping
{
    public class NewsCommentMappingsProfile : Profile
    {
        public NewsCommentMappingsProfile()
        {
            CreateMap<NewsCommentDto, NewsCommentModel>();
            CreateMap<CreatingNewsCommentModel, CreatingNewsCommentDto>();
            CreateMap<UpdatingNewsCommentModel, UpdatingNewsCommentDto>();
            CreateMap<NewsCommentFilterModel, NewsCommentFilterDto>();
        }
    }
}
