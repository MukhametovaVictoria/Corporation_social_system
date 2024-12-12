using AutoMapper;
using BusinessLogic.Contracts.LikedNews;
using WebApi.Models.LikedNews;

namespace WebApi.Mapping
{
    public class LikedNewsInfoMappingsProfile : Profile
    {
        public LikedNewsInfoMappingsProfile()
        {
            CreateMap<LikedNewsInfoDto, LikedNewsInfoModel>();
        }
    }
}
