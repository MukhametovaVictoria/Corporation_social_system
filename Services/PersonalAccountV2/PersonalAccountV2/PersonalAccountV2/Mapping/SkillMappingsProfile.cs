using AutoMapper;
using BS.Contracts.Skill;
using PersonalAccountV2.Models.Skill;

namespace PersonalAccountV2.Mapping
{

    public class SkillMappingsProfile : Profile
    {
        public SkillMappingsProfile()
        {
            CreateMap<SkillDto, SkillModel>();
            CreateMap<CreatingOrUpdatingSkillModel, CreatingOrUpdatingSkillDto>();
        }
    }
}
