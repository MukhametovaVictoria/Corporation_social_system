using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.Employee;
using DataAccess.Entities;
using DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить список сотрудников.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Список сотрудников. </returns>
        public async Task<ICollection<EmployeeDto>> GetPagedAsync(int page, int pageSize)
        {
            ICollection<Employee> entities = await _employeeRepository.GetPagedAsync(page, pageSize);
            return _mapper.Map<ICollection<Employee>, ICollection<EmployeeDto>>(entities);
        }

        /// <summary>
        /// Получить новость.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО сотрудника. </returns>
        public async Task<EmployeeDto> GetByIdAsync(Guid id)
        {
            var Employee = await _employeeRepository.GetAsync(id);
            return _mapper.Map<EmployeeDto>(Employee);
        }

        public async Task<Guid> CreateAsync(CreatingEmployeeDto creatingEmployeeDto)
        {
            var employee = _mapper.Map<CreatingEmployeeDto, Employee>(creatingEmployeeDto);
            var createdEmployee = await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
            return createdEmployee.Id;
        }

        public async Task UpdateAsync(UpdatingEmployeeDto updatingEmployeeDto)
        {
            var employee = await _employeeRepository.GetAsync(updatingEmployeeDto.Id);
            if (employee == null)
            {
                var employeeNew = _mapper.Map<UpdatingEmployeeDto, Employee>(updatingEmployeeDto);
                await _employeeRepository.AddAsync(employeeNew);
                await _employeeRepository.SaveChangesAsync();
            }
            else
            {
                employee.Firstname = updatingEmployeeDto.Firstname;
                employee.EmploymentDate = updatingEmployeeDto.EmploymentDate;
                employee.Birthdate = updatingEmployeeDto.Birthdate;
                employee.Surname = updatingEmployeeDto.Surname;
                employee.Position = updatingEmployeeDto.Position;
                employee.IsAdmin = updatingEmployeeDto.IsAdmin;
                employee.IsDeleted = updatingEmployeeDto.IsDeleted;
                employee.About = updatingEmployeeDto.About;
                employee.MainEmail = updatingEmployeeDto.MainEmail;
                employee.MainTelephoneNumber = updatingEmployeeDto.MainTelephoneNumber;
                employee.OfficeAddress = updatingEmployeeDto.OfficeAddress;

                _employeeRepository.Update(employee);
                await _employeeRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            if (employee != null)
            {
                employee.IsDeleted = true;
                await _employeeRepository.SaveChangesAsync();
            }
        }
    }
}
