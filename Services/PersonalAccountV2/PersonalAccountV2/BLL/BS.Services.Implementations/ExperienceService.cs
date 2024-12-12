using AutoMapper;
using BS.Contracts.Experience;
using BS.Services.Abstractions;
using DA.Entities;
using DA.Repositories.Abstractions;

namespace BS.Services.Implementations
{

    public class ExperienceService : IExperienceService
    {
        private readonly IMapper _mapper;
        private readonly IExperienceRepository _experienceRepository;

        public ExperienceService(IMapper mapper, IExperienceRepository experienceRepository)
        {
            _mapper = mapper;
            _experienceRepository = experienceRepository;
        }

        public async Task<Guid> CreateOrUpdate(CreatingOrUpdatingExperienceDto experienceEmployee)
        {
            var item = _mapper.Map<CreatingOrUpdatingExperienceDto, Experience>(experienceEmployee);
            var id = await _experienceRepository.CreateOrUpdate(item);
            await _experienceRepository.SaveChangesAsync();
            return id;
        }

        public async Task<ICollection<ExperienceDto>> GetAllExperienceEmployee(Guid employee)
        {
            var allExperience = await _experienceRepository.GetAllExperienceEmployee(employee);
            return _mapper.Map<ICollection<Experience>, ICollection<ExperienceDto>>(allExperience);
        }

        public  async Task<ExperienceDto> GetByIdAsync(Guid id)
        {
            var experience = await _experienceRepository.GetAsync(id);
            return _mapper.Map<ExperienceDto>(experience);
        }
    }
}
