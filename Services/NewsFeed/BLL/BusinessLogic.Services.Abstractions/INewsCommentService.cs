using BusinessLogic.Contracts.NewsComment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    /// <summary>
    /// Cервис работы с новостями (интерфейс).
    /// </summary>
    public interface INewsCommentService
    {
        /// <summary>
        /// Получить список комментариев.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Список комментариев. </returns>
        Task<ICollection<NewsCommentDto>> GetPagedAsync(int page, int pageSize);

        /// <summary>
        /// Получить комментарий новости.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО комментария. </returns>
        Task<NewsCommentDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать комментарий новости.
        /// </summary>
        /// <param name="creatingNewsCommentDto"> ДТО создаваемого комментария. </param>
        Task<Guid> CreateAsync(CreatingNewsCommentDto creatingNewsCommentDto);

        /// <summary>
        /// Изменить комментарий новости.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingNewsCommentDto"> ДТО редактируемого комментария. </param>
        Task UpdateAsync(Guid id, UpdatingNewsCommentDto updatingNewsCommentDto);

        /// <summary>
        /// Удалить комментарий новости.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Получение коллекции комментариев к Новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по новости</returns>
        Task<List<NewsCommentDto>> GetCollectionByNewsId(Guid newsId, int page, int itemsPerPage);

        /// <summary>
        /// Получение коллекции комментариев по сотруднику
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по сотруднику</returns>
        Task<List<NewsCommentDto>> GetCollectionByEmployeeId(Guid employeeId, int page, int itemsPerPage);

        /// <summary>
        /// Получение коллекции данных по комментариям
        /// </summary>
        /// <param name="employeeId">Id сотрудника, запросившего инфо</param>
        /// <param name="newsIds">Коллекция Id новостей</param>
        /// <returns>Коллекция данных по комментариям</returns>
        Task<List<NewsCommentInfoDto>> CheckIsAuthor(Guid employeeId, ICollection<Guid> newsIds);
    }
}