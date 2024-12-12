using DA.Entities;

namespace DA.Repositories.Abstractions
{
    public interface IExperienceRepository : IRepository<Experience, Guid>
    {

        Task<List<Experience>> GetAllExperienceEmployee(Guid employee);
        Task<Guid> CreateOrUpdate(Experience experience);
    }
}
