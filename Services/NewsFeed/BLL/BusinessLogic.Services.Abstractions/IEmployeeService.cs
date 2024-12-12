using BusinessLogic.Contracts.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    /// <summary>
    /// Cервис работы с сотрудниками (интерфейс).
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Получить список сотрудников.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Список сотрудников. </returns>
        Task<ICollection<EmployeeDto>> GetPagedAsync(int page, int pageSize);

        /// <summary>
        /// Получить новость.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО сотрудника. </returns>
        Task<EmployeeDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создание/изменение сотрудников
        /// </summary>
        /// <param name="employees"> Список сотрудников. </param>
        Task<bool> CreateOrUpdateRange(List<ShortEmployeeDto> employees);
    }
}