using DA.Context;
using DA.Entities;
using DA.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Implementations
{
    public class SkillRepository : Repository<Skill, Guid>, ISkillRepository
    {
        public SkillRepository(DataContext context) : base(context)
        {
        }
        public async Task<Guid> CreateOrUpdate(Skill skill)
        {
            Skill sk = null;
            if (skill.Id != Guid.Empty)
            {
                sk = Get(skill.Id);
            }

            if (sk != null)
            {
                sk.Description = skill.Description;
                sk.UpdatedAt = DateTime.Now;
                Update(sk);
                return sk.Id;
            }
            else
            {
                return (await AddAsync(skill)).Id; //vmukhametova
            }
        }

        public async Task<List<Skill>> GetAllSkillEmployee(Guid employee)
        {
            var query = GetAll();
            return await query.Where(x => x.EmployeeId == employee).ToListAsync<Skill>();
        }
    }
}
