using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class LeaveMapper
    {
        public static LeaveDto? ToDto(Leave leave)
        {
            if (leave == null)
                return null;

            return new LeaveDto
            {
                Id = leave.Id,
                EmployeeId = leave.EmployeeId,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                IsPaid = leave.IsPaid,
                CreatedOn = leave.CreatedOn,
                ModifiedOn = leave.ModifiedOn
            };
        }

        public static Leave? ToEntity(LeaveDto leaveDto)
        {
            if (leaveDto == null)
                return null;

            return new Leave
            {
                Id = leaveDto.Id,
                EmployeeId = leaveDto.EmployeeId,
                StartDate = leaveDto.StartDate,
                EndDate = leaveDto.EndDate,
                IsPaid= leaveDto.IsPaid,
                CreatedOn = leaveDto.CreatedOn,
                ModifiedOn = leaveDto.ModifiedOn
            };
        }
    }
}