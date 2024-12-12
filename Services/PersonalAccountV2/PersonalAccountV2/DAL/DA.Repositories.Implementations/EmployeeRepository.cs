using DA.Context;
using DA.Entities;
using DA.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Implementations
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Employee>> GetCollection(Employee filterEmployee)
        {
            if (string.IsNullOrEmpty(filterEmployee.Firstname) && string.IsNullOrEmpty(filterEmployee.Surname))
                return await GetAll().ToListAsync();

            var query = GetAll();
            var collection = await query
                .Where(x => (filterEmployee.Firstname == null || x.Firstname.Contains(filterEmployee.Firstname))
                            && (filterEmployee.Surname == null || x.Surname.Contains(filterEmployee.Surname)))
                    .OrderByDescending(x => x.Firstname)
                    .ThenByDescending(x => x.Surname)
                    .ToListAsync();

            return collection;
        }

        public async Task CreateOrUpdateRange(ICollection<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var emp = employee.Id != Guid.Empty ? Get(employee.Id) : null;

                if (emp != null)
                {
                    emp.Surname = employee.Surname;
                    emp.Firstname = employee.Firstname;
                    emp.Position = employee.Position;
                    emp.IsDeleted = employee.IsDeleted;
                    emp.IsAdmin = employee.IsAdmin;
                    emp.UpdatedAt = DateTime.UtcNow;

                    Update(emp);
                }
                else
                {
                    await AddAsync(employee);
                }
            }
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            var query = GetAll();
            return await query.ToListAsync();
        }

        public async Task<Guid> UpdateEmployee(Employee employee)
        {
            var emp = employee.Id != Guid.Empty ? Get(employee.Id) : null;

            if (emp != null)
            {
                emp.Surname = !String.IsNullOrEmpty(employee.Surname) ? employee.Surname : emp.Surname;
                emp.Firstname = !String.IsNullOrEmpty(employee.Firstname) ? employee.Firstname : emp.Firstname;
                emp.Position = !String.IsNullOrEmpty(employee.Position) ? employee.Position : emp.Position;
                emp.Birthdate = employee.Birthdate != default ? employee.Birthdate : emp.Birthdate;
                emp.OfficeAddress = !String.IsNullOrEmpty(employee.OfficeAddress) ? employee.OfficeAddress : emp.OfficeAddress;
				emp.UpdatedAt = DateTime.UtcNow;
                emp.About = employee.About;
                emp.MainEmail = employee.MainEmail;
                emp.MainTelephoneNumber = employee.MainTelephoneNumber;
                emp.Language = !String.IsNullOrEmpty(employee.Language) ? employee.Language : emp.Language;
				emp.EmploymentDate = employee.EmploymentDate != default ? employee.EmploymentDate : emp.EmploymentDate;

				Update(emp);

                return emp.Id;
            }
            else
            {
                return (await AddAsync(employee)).Id;
            }
        }
    }
}
