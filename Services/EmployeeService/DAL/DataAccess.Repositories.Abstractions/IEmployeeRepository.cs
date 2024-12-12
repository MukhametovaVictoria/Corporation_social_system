using DataAccess.Entities;
using Services.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstractions
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
        void CreateOrUpdateRange(List<Employee> employees);
    }
}
