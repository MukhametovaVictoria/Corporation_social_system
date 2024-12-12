using DA.Context;
using DA.Entities;
using DA.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Implementations
{
    public class ExperienceRepository : Repository<Experience, Guid>, IExperienceRepository
    {
        public ExperienceRepository(DataContext context) : base(context)
        {
        }

        public async Task<Guid> CreateOrUpdate(Experience experience)
        {
            Experience exp = null;
            if(experience.Id != Guid.Empty)
                exp = Get(experience.Id);
            if (exp != null)
            {
                exp.Company = experience.Company;
                exp.EmployementDate = experience.EmployementDate;
                exp.DescriptionWork = experience.DescriptionWork;
                exp.DismissalDate = experience.DismissalDate;
                exp.DescriptionCompany = experience.DescriptionCompany;
                exp.DescriptionWork = experience.DescriptionWork.ToString();
                exp.UpdatedAt = DateTime.Now;
                Update(exp);
                return exp.Id;
            }
            else
            {
                return (await AddAsync(experience)).Id; //vmukhametova
            }
        }

        public async Task<List<Experience>> GetAllExperienceEmployee(Guid employee)
        {
            var query = GetAll();
            return await query.Where(x => x.EmployeeId == employee).ToListAsync<Experience>();
        }
    }
}
