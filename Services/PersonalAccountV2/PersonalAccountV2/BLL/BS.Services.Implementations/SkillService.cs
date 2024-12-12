using AutoMapper;
using BS.Contracts.Skill;
using BS.Services.Abstractions;
using DA.Entities;
using DA.Repositories.Abstractions;

namespace BS.Services.Implementations
{

    public class SkillService : ISkillService
    {
        private readonly IMapper _mapper;
        private readonly ISkillRepository _skillRepository;

        public SkillService(IMapper mapper, ISkillRepository skillRepository)
        {
            _mapper = mapper;
            _skillRepository = skillRepository;
        }

        public async Task<Guid> CreateOrUpdate(CreatingOrUpdatingSkillDto skillEmployee)
        {
            var item = _mapper.Map<CreatingOrUpdatingSkillDto, Skill>(skillEmployee);
            var id = await _skillRepository.CreateOrUpdate(item);
            await _skillRepository.SaveChangesAsync();
            return id;
        }

        public async Task<ICollection<SkillDto>> GetAllSkillEmployee(Guid employee)
        {
            var allSkills = await _skillRepository.GetAllSkillEmployee(employee);
            return _mapper.Map<ICollection<Skill>, ICollection<SkillDto>>(allSkills);
        }

        public async Task<SkillDto> GetByIdAsync(Guid id)
        {
            var skill = await _skillRepository.GetAsync(id);
            return _mapper.Map<SkillDto>(skill);
        }
    }
}
