using AutoMapper;
using BusinessLogic.Contracts.LikedNews;
using DataAccess.Entities;

namespace BusinessLogic.Services.Mapping
{
    public class LikedNewsInfoMappingsProfile : Profile
    {
        public LikedNewsInfoMappingsProfile()
        {
            CreateMap<LikedNewsInfo, LikedNewsInfoDto>();
        }
    }
}
