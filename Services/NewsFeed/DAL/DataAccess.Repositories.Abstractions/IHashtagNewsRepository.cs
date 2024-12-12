using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IHashtagNewsRepository : IRepository<HashtagNews, Guid>
    {
        /// <summary>
        /// Получение записей промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="hashtagNewsDto">Дто связки</param>
        /// <returns>Коллекция записей</returns>
        Task<List<HashtagNews>> GetCollection(HashtagNews hashtagNewsDto);

        /// <summary>
        /// Получение записей промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="postIds">Id новостей</param>
        /// <returns>Коллекция записей</returns>
        Task<List<HashtagNews>> GetCollectionByNewsId(ICollection<Guid> postIds);

        /// <summary>
        /// Получение записей промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="hashtagIds">Id хэштегов</param>
        /// <returns>Коллекция записей</returns>
        Task<List<HashtagNews>> GetCollectionByHashtagId(ICollection<Guid> hashtagIds);

        /// <summary>
        /// Удаление записи промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        void DeleteByNewsId(Guid newsId);
    }
}
