using DA.Context;
using DA.Entities;
using DA.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Implementations
{
    public class EventRepository : Repository<Event, Guid>, IEventRepository
    {

        public EventRepository(DataContext context) : base(context)
        {
        }
        public async Task<Guid> CreateOrUpdate(Event entity)
        {
            Event even = null;
            if(entity.Id != Guid.Empty)
                even = Get(entity.Id);

            if (even != null)
            {
                even.IsAcrive = entity.IsAcrive;
                even.UpdatedAt = DateTime.Now;
                Update(even);
                return even.Id;
            }
            else
            {
                return (await AddAsync(entity)).Id; //vmukhametova
            }
            
        }

        public async Task<List<Event>> GetAllEventEmployee(Guid employee)
        {
            var query = GetAll();
            return await query.Where(x => x.EmployeeId == employee).ToListAsync<Event>();
        }
    }
}
