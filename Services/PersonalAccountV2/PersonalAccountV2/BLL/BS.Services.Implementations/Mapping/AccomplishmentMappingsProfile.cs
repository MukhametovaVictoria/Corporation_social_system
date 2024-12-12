using AutoMapper;
using BS.Contracts.Accomplishment;
using DA.Entities;

namespace BS.Services.Implementations.Mapping
{
    public class AccomplishmentMappingsProfile : Profile
    {
        public AccomplishmentMappingsProfile()
        {
            CreateMap<Accomplishment, AccomplishmentDto>();

            CreateMap<CreatingOrUpdatingAccomplishmentDto, Accomplishment>()
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.Employee, map => map.Ignore());
        }
    }
}
