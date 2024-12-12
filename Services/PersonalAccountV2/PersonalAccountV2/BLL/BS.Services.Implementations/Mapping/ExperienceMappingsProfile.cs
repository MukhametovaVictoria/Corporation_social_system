using AutoMapper;
using BS.Contracts.Experience;
using DA.Entities;

namespace BS.Services.Implementations.Mapping
{
    public class ExperienceMappingsProfile : Profile
    {
        public ExperienceMappingsProfile()
        {
            CreateMap<Experience, ExperienceDto>();

            CreateMap<CreatingOrUpdatingExperienceDto, Experience>()
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.Employee, map => map.Ignore());
        }
    }
}
