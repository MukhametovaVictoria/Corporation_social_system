using AutoMapper;
using BusinessLogic.Contracts.Hashtag;
using WebApi.Models.Hashtag;

namespace WebApi.Mapping
{
    public class HashtagMappingsProfile : Profile
    {
        public HashtagMappingsProfile()
        {
            CreateMap<HashtagDto, HashtagModel>();
            CreateMap<CreatingHashtagModel, CreatingHashtagDto>();
        }
    }
}
