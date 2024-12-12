using AutoMapper;
using BusinessLogic.Contracts.NewsComment;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class NewsCommentMappingsProfile : Profile
    {
        public NewsCommentMappingsProfile()
        {
            CreateMap<NewsComment, NewsCommentDto>();

            CreateMap<CreatingNewsCommentDto, NewsComment>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.News, map => map.Ignore())
                .ForMember(d => d.Author, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore());

            CreateMap<UpdatingNewsCommentDto, NewsComment>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.News, map => map.Ignore())
                .ForMember(d => d.NewsId, map => map.Ignore())
                .ForMember(d => d.Author, map => map.Ignore())
                .ForMember(d => d.AuthorId, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.CreatedAt, map => map.Ignore());
        }
    }
}
