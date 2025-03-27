using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto? ToDto(Employee employee)
        {
            if (employee == null)
                return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Patronymic = employee.Patronymic,
                DateOfBirth = employee.DateOfBirth,
                CreatedOn = employee.CreatedOn,
                ModifiedOn = employee.ModifiedOn,
                IsActive = employee.IsActive
            };
        }

        public static Employee? ToEntity(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return null;

            return new Employee
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Patronymic = employeeDto.Patronymic,
                DateOfBirth = employeeDto.DateOfBirth,
                CreatedOn = employeeDto.CreatedOn,
                ModifiedOn = employeeDto.ModifiedOn,
                IsActive = employeeDto.IsActive
            };
        }
    }
}