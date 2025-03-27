using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILeaveService
    {
        Task<Leave> AddLeaveAsync(Leave leave);
        Task<Leave?> GetLeaveByIdAsync(Guid id);
        Task<IEnumerable<Leave>> GetAllLeavesAsync();
        Task UpdateLeaveAsync(Leave leave);
        Task DeleteLeaveAsync(Guid id);
        Task<AvailableLeaveDays?> GetAvailableLeaveDaysAsync(Guid employeeId, string code);
    }
}
