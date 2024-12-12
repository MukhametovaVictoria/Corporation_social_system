using AutoMapper;
using BusinessLogic.Contracts.News;
using WebApi.Models.News;

namespace WebApi.Mapping
{
    public class NewsMappingsProfile : Profile
    {
        public NewsMappingsProfile()
        {
            CreateMap<NewsDto, NewsModel>();
            CreateMap<CreatingNewsModel, CreatingNewsDto>();
            CreateMap<UpdatingNewsModel, UpdatingNewsDto>()
                .ForMember(d => d.IsPublished, map => map.Ignore())
                .ForMember(d => d.IsArchived, map => map.Ignore());
            CreateMap<NewsFilterModel, NewsFilterDto>();
        }
    }
}
