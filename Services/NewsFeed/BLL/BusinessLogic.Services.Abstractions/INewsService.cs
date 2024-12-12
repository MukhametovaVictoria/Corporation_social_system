using BusinessLogic.Contracts.Hashtag;
using BusinessLogic.Contracts.LikedNews;
using BusinessLogic.Contracts.News;
using BusinessLogic.Contracts.Picture;
using DataAccess.Common;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    /// <summary>
    /// Cервис работы с новостями (интерфейс).
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Получить список новостей.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Список новостей. </returns>
        Task<ICollection<NewsDto>> GetPagedAsync(int page, int pageSize);

        /// <summary>
        /// Получение коллекции Новостей по объекту NewsSearch
        /// </summary>
        /// <param name="newsSearch">Объект расширенного поиска новостей</param>
        /// <returns>Коллекция новостей</returns>
        Task<ICollection<NewsDto>> GetCollection(NewsSearch newsSearch);

        /// <summary>
        /// Получить новость.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО новости. </returns>
        Task<NewsDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать новость.
        /// </summary>
        /// <param name="creatingNewsDto"> ДТО создаваемой новости. </param>
        Task<NewsDto> CreateAsync(CreatingNewsDto creatingNewsDto);

        /// <summary>
        /// Изменить новость.
        /// </summary>
        /// <param name="updatingNewsDto"> ДТО редактируемой новости. </param>
        Task UpdateAsync(UpdatingNewsDto updatingNewsDto);

        /// <summary>
        /// Удалить новость.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Лайкнуть новость
        /// </summary>
        /// <param name="id">Id Новости</param>
        /// <param name="employeeId">Id Сотрудника</param>
        /// <returns></returns>
        Task<LikedNewsInfoDto> Like(Guid id, Guid employeeId);

        /// <summary>
        /// Получить инфо по лайкам
        /// </summary>
        /// <param name="newsIds">Список новостей</param>
        /// <param name="currentEmployeeId">Пользователь, запросивший инфо</param>
        /// <returns>Инфо по лайкам с учетом лайков конкретного пользовател</returns>
        Task<ICollection<LikedNewsInfoDto>> GetLikes(ICollection<Guid> newsIds, Guid currentEmployeeId);

        /// <summary>
        /// Получить инфо по лайкам пользователя
        /// </summary>
        /// <param name="employeeId">Id Пользовател</param>
        /// <param name="page">Станица</param>
        /// <param name="itemsPerPage">Количество записей</param>
        /// <returns>Инфо по лайкам конкретного пользователя</returns>
        Task<ICollection<NewsDto>> GetLikedNewsByEmployee(Guid employeeId, int page, int itemsPerPage);

        /// <summary>
        /// Добавить стопку новостей
        /// </summary>
        /// <param name="entities">Список новостей</param>
        /// <returns></returns>
        Task AddRangeAsync(ICollection<CreatingNewsDto> entities);

        Task ChangeVisibility(Guid newsId, bool isPublished, bool isArchived);
    }
}