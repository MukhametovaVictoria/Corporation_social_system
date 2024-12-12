using DA.Context;
using DA.Entities;
using DA.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Implementations
{
    public class AccomplishmentRepository : Repository<Accomplishment, Guid>, IAccomplishmentRepository
    {
        public AccomplishmentRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Accomplishment>> GetAllAccomplishmentEmployee(Guid employee)
        {
            var query = GetAll();
            return await query.Where(x => x.EmployeeId == employee).ToListAsync<Accomplishment>();
        }

        public async Task<Guid> CreateOrUpdate(Accomplishment accomplishment)
        {
            Accomplishment acc = null;
            if(accomplishment.Id != Guid.Empty)
                acc = Get(accomplishment.Id);

            if (acc != null)
            {
                acc.Description = accomplishment.Description;
                acc.Date = accomplishment.Date;
                acc.UpdatedAt = DateTime.Now;
                Update(acc);
                return acc.Id;
            }
            else
            {
                return (await AddAsync(accomplishment)).Id; //vmukhametova
            }
        }
    }
}
