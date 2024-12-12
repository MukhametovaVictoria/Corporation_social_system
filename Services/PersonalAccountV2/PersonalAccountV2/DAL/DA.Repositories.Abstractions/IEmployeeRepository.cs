using DA.Entities;

namespace DA.Repositories.Abstractions
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {

        Task<List<Employee>> GetAllEmployee();
        Task CreateOrUpdateRange(ICollection<Employee> employees);
        Task<Guid> UpdateEmployee(Employee employee);
    }
}
