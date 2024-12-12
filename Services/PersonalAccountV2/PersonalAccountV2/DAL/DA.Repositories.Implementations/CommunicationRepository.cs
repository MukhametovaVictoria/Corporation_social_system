using DA.Context;
using DA.Entities;
using DA.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Implementations
{
    public class CommunicationRepository : Repository<Communication, Guid>, ICommunicationRepository
    {
        public CommunicationRepository(DataContext context) : base(context)
        {
        }
        public async Task<Guid> CreateOrUpdate(Communication communication)
        {
            Communication com = null;
            if(communication.Id != Guid.Empty)
                com = Get(communication.Id);

            if (com != null)
            {
                com.Value = communication.Value;
                com.UpdatedAt = DateTime.Now;
                Update(com);
                return com.Id;
            }
            else
            {
                return (await AddAsync(communication)).Id; //vmukhametova
            }
        }

        public async Task<List<Communication>> GetAllCommunicationEmployee(Guid employee)
        {
            var query = GetAll();
            return await query.Where(x => x.EmployeeId == employee).ToListAsync<Communication>();
        }
    }
}
