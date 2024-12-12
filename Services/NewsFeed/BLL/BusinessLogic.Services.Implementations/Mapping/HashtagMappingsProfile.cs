using AutoMapper;
using BusinessLogic.Contracts.Hashtag;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class HashtagMappingsProfile : Profile
    {
        public HashtagMappingsProfile()
        {
            CreateMap<Hashtag, HashtagDto>();

            CreateMap<CreatingHashtagDto, Hashtag>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.HashtagNewsList, map => map.Ignore());
        }
    }
}
