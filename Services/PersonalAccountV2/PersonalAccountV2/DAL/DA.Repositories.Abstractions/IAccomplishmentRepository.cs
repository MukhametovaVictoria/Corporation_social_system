using DA.Entities;

namespace DA.Repositories.Abstractions
{
    public interface IAccomplishmentRepository : IRepository<Accomplishment, Guid>
    {

        Task<List<Accomplishment>> GetAllAccomplishmentEmployee(Guid employee);
        Task<Guid> CreateOrUpdate(Accomplishment accomplishments);
    }
}
