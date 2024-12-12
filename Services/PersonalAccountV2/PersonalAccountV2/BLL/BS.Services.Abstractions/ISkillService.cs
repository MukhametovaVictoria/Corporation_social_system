using BS.Contracts.Skill;

namespace BS.Services.Abstractions
{
    public interface ISkillService
    {
        Task<ICollection<SkillDto>> GetAllSkillEmployee(Guid employee);

        Task<SkillDto> GetByIdAsync(Guid id);

        public Task<Guid> CreateOrUpdate(CreatingOrUpdatingSkillDto skillEmployee);
    }
}
