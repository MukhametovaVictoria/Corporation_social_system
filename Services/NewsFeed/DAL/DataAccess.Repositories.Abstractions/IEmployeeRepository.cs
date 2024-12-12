using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список сотрудников. </returns>
        Task<List<Employee>> GetPagedAsync(int page, int itemsPerPage);

        /// <summary>
        /// Получить список пользователей по фильтрам
        /// </summary>
        /// <param name="filterEmployee">Фильтры</param>
        /// <returns>Коллекция пользователей</returns>
        Task<List<Employee>> GetCollection(Employee employee);

        /// <summary>
        /// Создание/изменение сотрудников
        /// </summary>
        /// <param name="employees"> Список сотрудников. </param>
        Task<bool> CreateOrUpdateRange(List<Employee> employees);
    }
}
