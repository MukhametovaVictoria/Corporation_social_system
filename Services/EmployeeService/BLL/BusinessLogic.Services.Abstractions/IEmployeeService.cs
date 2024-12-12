using BusinessLogic.Contracts.Employee;
using System;
using System.Collections.Generic;
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
        /// Получить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО сотрудника. </returns>
        Task<EmployeeDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать сотрудника.
        /// </summary>
        /// <param name="creatingEmployeeDto"> ДТО создаваемого сотрудника. </param>
        Task<Guid> CreateAsync(CreatingEmployeeDto creatingEmployeeDto);

        /// <summary>
        /// Изменить сотрудника.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingEmployeeDto"> ДТО редактируемого сотрудника. </param>
        Task UpdateAsync(UpdatingEmployeeDto updatingEmployeeDto);

        /// <summary>
        /// Удалить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);
    }
}