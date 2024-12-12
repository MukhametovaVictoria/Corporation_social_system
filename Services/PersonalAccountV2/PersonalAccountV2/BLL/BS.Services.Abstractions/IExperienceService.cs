using BS.Contracts.Experience;

namespace BS.Services.Abstractions
{
    public interface IExperienceService
    {
        Task<ICollection<ExperienceDto>> GetAllExperienceEmployee(Guid employee);

        Task<ExperienceDto> GetByIdAsync(Guid id);

        public Task<Guid> CreateOrUpdate(CreatingOrUpdatingExperienceDto experienceEmployee);
    }
}
