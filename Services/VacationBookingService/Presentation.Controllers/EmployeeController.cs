using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var employeeEntity = EmployeeMapper.ToEntity(employeeDto);
            if (employeeDto == null || employeeEntity == null)
                return BadRequest("Empty data.");

            var employee = await _employeeService.AddEmployeeAsync(employeeEntity);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, EmployeeMapper.ToDto(employee));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Empty data.");

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(EmployeeMapper.ToDto(employee));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employeeDto)
        {
            if (id == Guid.Empty || employeeDto == null)
                return BadRequest("Empty data.");

            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            employee.Name = employeeDto.Name;
            employee.Surname = employeeDto.Surname;
            employee.DateOfBirth = employeeDto.DateOfBirth;
            await _employeeService.UpdateEmployeeAsync(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Empty data.");

            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}