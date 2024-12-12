using AutoMapper;
using BusinessLogic.Contracts.HashtagNews;
using WebApi.Models.HashtagNews;

namespace WebApi.Mapping
{
    public class HashtagNewsMappingsProfile : Profile
    {
        public HashtagNewsMappingsProfile()
        {
            CreateMap<HashtagNewsDto, HashtagNewsModel>();
            CreateMap<CreatingHashtagNewsModel, CreatingHashtagNewsDto>();
        }
    }
}
