using DA.Entities;

namespace DA.Repositories.Abstractions
{
    public interface ISkillRepository : IRepository<Skill, Guid>
    {

        Task<List<Skill>> GetAllSkillEmployee(Guid employee);
        Task<Guid> CreateOrUpdate(Skill skill);
    }
}
