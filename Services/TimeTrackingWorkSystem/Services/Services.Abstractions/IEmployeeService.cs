using Services.Contracts.Employee;

namespace Services.Abstractions
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
        Task<ICollection<ShortEmployeeDto>> GetPagedAsync(int page, int pageSize);

        /// <summary>
        /// Получить сотрудника.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО сотрудника. </returns>
        Task<ShortEmployeeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Создание/изменение сотрудников
        /// </summary>
        /// <param name="employees"> Список сотрудников. </param>
        Task<bool> CreateOrUpdateRange(List<ShortEmployeeDto> employees);
    }
}