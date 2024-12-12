using AutoMapper;
using BS.Contracts.Skill;
using DA.Entities;

namespace BS.Services.Implementations.Mapping
{
    public class SkillMappingsProfile : Profile
    {
        public SkillMappingsProfile()
        {
            CreateMap<Skill, SkillDto>();

            CreateMap<CreatingOrUpdatingSkillDto, Skill>()
                .ForMember(d => d.CreatedAt, map => map.Ignore())
                .ForMember(d => d.UpdatedAt, map => map.Ignore())
                .ForMember(d => d.Employee, map => map.Ignore());
        }
    }
}
