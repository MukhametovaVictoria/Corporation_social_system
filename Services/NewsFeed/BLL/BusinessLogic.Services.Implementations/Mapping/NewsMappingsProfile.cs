using AutoMapper;
using BusinessLogic.Contracts.News;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class NewsMappingsProfile : Profile
    {
        public NewsMappingsProfile()
        {
            CreateMap<News, NewsDto>();

            CreateMap<CreatingNewsDto, News>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Author, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.IsPublished, map => map.Ignore())
                .ForMember(d => d.IsArchived, map => map.Ignore())
                .ForMember(d => d.NewsCommentList, map => map.Ignore());

            CreateMap<UpdatingNewsDto, News>()
                .ForMember(d => d.Author, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.AuthorId, map => map.Ignore())
                .ForMember(d => d.NewsCommentList, map => map.Ignore());
        }
    }
}
