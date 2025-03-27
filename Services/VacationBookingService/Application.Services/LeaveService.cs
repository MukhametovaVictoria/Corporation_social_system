using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;

        public LeaveService(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        public async Task<Leave> AddLeaveAsync(Leave leave)
        {
            return await _leaveRepository.AddLeaveAsync(leave);
        }

        public async Task<Leave?> GetLeaveByIdAsync(Guid id)
        {
            return await _leaveRepository.GetLeaveByIdAsync(id);
        }

        public async Task<IEnumerable<Leave>> GetAllLeavesAsync()
        {
            return await _leaveRepository.GetAllLeavesAsync();
        }

        public async Task UpdateLeaveAsync(Leave leave)
        {
            await _leaveRepository.UpdateLeaveAsync(leave);
        }

        public async Task DeleteLeaveAsync(Guid id)
        {
            await _leaveRepository.DeleteLeaveAsync(id);
        }

        public async Task<AvailableLeaveDays?> GetAvailableLeaveDaysAsync(Guid employeeId, string code)
        {
            return await _leaveRepository.GetAvailableLeaveDaysAsync(employeeId, code);
        }
    }
}