using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface INewsCommentRepository : IRepository<NewsComment, Guid>
    {
        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список комментариев. </returns>
        Task<List<NewsComment>> GetPagedAsync(int page, int itemsPerPage);

        /// <summary>
        /// Получение коллекции комментариев к Новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по новости</returns>
        Task<List<NewsComment>> GetCollectionByNewsId(Guid newsId, int page, int itemsPerPage);

        /// <summary>
        /// Получение коллекции комментариев по сотруднику
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по сотруднику</returns>
        Task<List<NewsComment>> GetCollectionByEmployeeId(Guid employeeId, int page, int itemsPerPage);

        /// <summary>
        /// Удаление комментариев по новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        Task DeleteByNewsId(Guid newsId);

        /// <summary>
        /// Удаление комментариев по автору.
        /// </summary>
        /// <param name="authorId">Id автора</param>
        Task DeleteByAuthorId(Guid authorId);

        /// <summary>
        /// Получение коллекции данных по комментариям
        /// </summary>
        /// <param name="employeeId">Id сотрудника, запросившего инфо</param>
        /// <param name="newsIds">Коллекция Id новостей</param>
        /// <returns>Коллекция данных по комментариям</returns>
        Task<List<NewsCommentInfo>> CheckIsAuthor(Guid employeeId, ICollection<Guid> newsIds);
    }
}
