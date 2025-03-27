using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly AppDbContext _context;

        public LeaveRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Leave> AddLeaveAsync(Leave leave)
        {
            await _context.Leaves.AddAsync(leave);
            await _context.SaveChangesAsync();
            return leave;
        }

        public async Task<Leave?> GetLeaveByIdAsync(Guid id)
        {
            return await _context.Leaves.FindAsync(id);
        }

        public async Task<IEnumerable<Leave>> GetAllLeavesAsync()
        {
            return await _context.Leaves.ToListAsync();
        }

        public async Task UpdateLeaveAsync(Leave leave)
        {
            _context.Leaves.Update(leave);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeaveAsync(Guid id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                _context.Leaves.Remove(leave);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AvailableLeaveDays?> GetAvailableLeaveDaysAsync(Guid employeeId, string code)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if(employee == null || employee.StartDate == default)
                return null;

            var sysSettings = await _context.SysSettings.FirstOrDefaultAsync(x => x.Code == code);
            var value = sysSettings != null ?
                await _context.SysSettingValues.FirstOrDefaultAsync(x => x.SysSettingsId == sysSettings.Id)
                : null;
            var days = 28;
            if (value != null && !string.IsNullOrEmpty(value.Value))
                int.TryParse(value.Value, out days);
            var vacations = await _context.Leaves.Where(x => x.EmployeeId == employeeId && x.StartDate <= DateTime.Now && x.StartDate != default && x.EndDate != default && x.IsPaid).ToListAsync();
            var daysOff = vacations.Count > 0 ? (int)Math.Round((double)vacations.Select(x => x.Count).Sum()) : 0;
            var yearsCount = DateTime.UtcNow.Year - employee.StartDate.Year;
            var result = 0;
            
            while(yearsCount >= 0)
            {
                var year = DateTime.UtcNow.AddYears(-yearsCount).Year;
                var endDate = year == DateTime.UtcNow.Year ? DateTime.UtcNow : new DateTime(year, 12, 31, 23, 23, 59, DateTimeKind.Utc);
                var startDate = year == employee.StartDate.Year ? employee.StartDate : new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var daysInYear = DateTime.IsLeapYear(year) ? 366 : 365;
                var count = (int)Math.Round((endDate - startDate).TotalDays * days / daysInYear);
                result += (count - daysOff);
                yearsCount -= 1;
            }

            return new AvailableLeaveDays() { Count = result, EmployeeId = employeeId };
        }
    }
}
