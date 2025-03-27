using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.Helpers;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        [ApiController]
        [Route("api/[controller]")]
        public class LeaveController : ControllerBase
        {
            private readonly ILeaveService _leaveService;
            private readonly IEmployeeService _employeeService;
            private readonly IConfiguration _configuration;

            public LeaveController(ILeaveService leaveService, IEmployeeService employeeService, IConfiguration configuration)
            {
                _leaveService = leaveService;
                _employeeService = employeeService;
                _configuration = configuration;
            }

            [HttpPost]
            public async Task<IActionResult> CreateLeave([FromBody] LeaveDto leaveDto)
            {
                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }

                var leaveEntity = LeaveMapper.ToEntity(leaveDto);
                if (leaveDto == null || leaveEntity == null)
                    return BadRequest("Empty data.");

                var leave = await _leaveService.AddLeaveAsync(leaveEntity);
                return CreatedAtAction(nameof(GetLeaveById), new { id = leave.Id }, LeaveMapper.ToDto(leave));
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetLeaveById(Guid id)
            {
                if (id == Guid.Empty)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }

                var leave = await _leaveService.GetLeaveByIdAsync(id);
                if (leave == null) return NotFound();
                return Ok(LeaveMapper.ToDto(leave));
            }

            [HttpGet]
            public async Task<IActionResult> GetAllLeaves()
            {
                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                var leaves = await _leaveService.GetAllLeavesAsync();
                return Ok(leaves);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateLeave(Guid id, [FromBody] LeaveDto leaveDto)
            {
                if (leaveDto == null || id == Guid.Empty)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                var leave = await _leaveService.GetLeaveByIdAsync(id);
                if (leave == null) return NotFound();
                leave.EmployeeId = leaveDto.EmployeeId;
                leave.StartDate = leaveDto.StartDate;
                leave.EndDate = leaveDto.EndDate;
                leave.IsPaid = leaveDto.IsPaid;
                leave.ModifiedOn = leaveDto.ModifiedOn;

                await _leaveService.UpdateLeaveAsync(leave);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteLeave(Guid id)
            {
                if (id == Guid.Empty)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                await _leaveService.DeleteLeaveAsync(id);
                return NoContent();
            }

            [HttpGet("{employeeId}")]
            public async Task<IActionResult> GetAvailableLeaveDaysAsync(Guid employeeId)
            {
                if (employeeId == Guid.Empty)
                    return BadRequest("Empty data.");

                if (!AuthChecking.CheckIsAuthorized(User, _employeeService))
                {
                    return Unauthorized();
                }
                var code = _configuration["SysSettings:VacationDaysCountPerYearCode"];
                if (code == null)
                    return NotFound();

                var availableDays = await _leaveService.GetAvailableLeaveDaysAsync(employeeId, code);
                return Ok(availableDays);
            }
        }
    }
}