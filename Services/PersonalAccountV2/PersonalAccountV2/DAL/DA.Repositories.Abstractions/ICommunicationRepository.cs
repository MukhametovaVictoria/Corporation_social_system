using DA.Entities;

namespace DA.Repositories.Abstractions
{
    public interface ICommunicationRepository : IRepository<Communication, Guid>
    {

        Task<List<Communication>> GetAllCommunicationEmployee(Guid employee);
        Task<Guid> CreateOrUpdate(Communication communication);
    }
}
