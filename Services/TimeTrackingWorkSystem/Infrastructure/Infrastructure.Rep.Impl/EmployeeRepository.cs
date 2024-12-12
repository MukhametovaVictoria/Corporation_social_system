using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Rep.Impl
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список сотрудников. </returns>
        public async Task<List<Employee>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        /// <summary>
        /// Получить список пользователей по фильтрам
        /// </summary>
        /// <param name="filterEmployee">Фильтры</param>
        /// <returns>Коллекция пользователей</returns>
        public async Task<List<Employee>> GetCollection(Employee filterEmployee)
        {
            if (string.IsNullOrEmpty(filterEmployee.FirstName) && string.IsNullOrEmpty(filterEmployee.SurnName))
                return await GetAll().ToListAsync();

            var query = GetAll();
            var collection = await query
                .Where(x => (filterEmployee.FirstName != null ? x.FirstName.Contains(filterEmployee.FirstName) : true) 
                            && (filterEmployee.SurnName != null ? x.SurnName.Contains(filterEmployee.SurnName) : true))
                    .OrderByDescending(x => x.FirstName)
                    .ThenByDescending(x => x.SurnName)
                    .ToListAsync();
            
            return collection;
        }

        /// <summary>
        /// Создание/изменение сотрудников
        /// </summary>
        /// <param name="employees"> Список сотрудников. </param>
        public async Task<bool> CreateOrUpdateRange(List<Employee> employees)
        {
            try
            {
                foreach (var employee in employees)
                {
                    var empFromDb = Get(employee.Id);

                    if (empFromDb != null)
                    {
                        empFromDb.FirstName = employee.FirstName;
                        empFromDb.SurnName = employee.SurnName;
                        empFromDb.Position = employee.Position;

                        Update(empFromDb);
                    }
                    else
                    {
                        await AddAsync(employee);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
