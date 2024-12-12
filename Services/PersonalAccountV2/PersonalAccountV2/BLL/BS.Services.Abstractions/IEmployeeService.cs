using BS.Contracts.Employee;

namespace BS.Services.Abstractions
{

    public interface IEmployeeService
    {
        Task<ICollection<EmployeeDto>> GetAllEmployee();

        Task<EmployeeDto> GetByIdAsync(Guid id);

        public Task CreateOrUpdateRange(ICollection<ShortEmployeeDto> employees);
        public Task<Guid> Update(UpdatingEmployeeDto employee);
    }
}
