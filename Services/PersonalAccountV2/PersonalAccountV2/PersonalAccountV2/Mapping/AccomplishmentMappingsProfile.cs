using AutoMapper;
using BS.Contracts.Accomplishment;
using PersonalAccountV2.Models.Accomplishment;

namespace PersonalAccountV2.Mapping
{

    public class AccomplishmentMappingsProfile : Profile
    {
        public AccomplishmentMappingsProfile()
        {
            CreateMap<AccomplishmentDto, AccomplishmentModel>();
            CreateMap<CreatingOrUpdatingAccomplishmentModel, CreatingOrUpdatingAccomplishmentDto>();
        }
    }
}
