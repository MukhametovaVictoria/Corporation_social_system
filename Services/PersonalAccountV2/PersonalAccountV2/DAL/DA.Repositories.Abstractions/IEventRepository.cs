using DA.Entities;

namespace DA.Repositories.Abstractions
{
    public interface IEventRepository : IRepository<Event, Guid>
    {

        Task<List<Event>> GetAllEventEmployee(Guid employee);
        Task<Guid> CreateOrUpdate(Event _event);
    }
}
