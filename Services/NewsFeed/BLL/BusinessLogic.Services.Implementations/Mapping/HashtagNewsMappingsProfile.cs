using AutoMapper;
using BusinessLogic.Contracts.HashtagNews;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class HashtagNewsMappingsProfile : Profile
    {
        public HashtagNewsMappingsProfile()
        {
            CreateMap<HashtagNews, HashtagNewsDto>();

            CreateMap<CreatingHashtagNewsDto, HashtagNews>()
                .ForMember(d => d.News, map => map.Ignore())
                .ForMember(d => d.Id, map => map.Ignore());
        }
    }
}
