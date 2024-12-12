using AutoMapper;
using BS.Contracts.Employee;
using BS.Services.Abstractions;
using DA.Entities;
using DA.Repositories.Abstractions;

namespace BS.Services.Implementations
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

        public async Task CreateOrUpdateRange(ICollection<ShortEmployeeDto> employees)
        {
            var employeeItem = _mapper.Map<ICollection<ShortEmployeeDto>, ICollection<Employee>>(employees);
            await _employeeRepository.CreateOrUpdateRange(employeeItem);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task<ICollection<EmployeeDto>> GetAllEmployee()
        {
            return _mapper.Map<ICollection<Employee>, ICollection<EmployeeDto>>(await _employeeRepository.GetAllEmployee());
        }

        public async Task<EmployeeDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<EmployeeDto>(await _employeeRepository.GetAsync(id));
        }

        public async Task<Guid> Update(UpdatingEmployeeDto employee)
        {
            var id = await _employeeRepository.UpdateEmployee(_mapper.Map<UpdatingEmployeeDto, Employee>(employee));
            await _employeeRepository.SaveChangesAsync();
            return id;
        }
    }
}
