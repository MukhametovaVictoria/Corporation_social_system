using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class HashtagNewsRepository : Repository<HashtagNews, Guid>, IHashtagNewsRepository
    {
        public HashtagNewsRepository(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Получение записей промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="hashtagNewsDto">Дто связки</param>
        /// <returns>Коллекция записей</returns>
        public async Task<List<HashtagNews>> GetCollection(HashtagNews hashtagNewsDto)
        {
            var query = GetAll();
            return await query
                .Where(x => x.HashtagId == hashtagNewsDto.HashtagId && x.NewsId == hashtagNewsDto.NewsId)
                .ToListAsync();
        }

        /// <summary>
        /// Получение записей промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="newsIds">Id новостей</param>
        /// <returns>Коллекция записей</returns>
        public async Task<List<HashtagNews>> GetCollectionByNewsId(ICollection<Guid> newsIds)
        {
            var query = GetAll();
            return await query
                .Where(x => newsIds.Contains(x.NewsId))
                .ToListAsync();
        }

        /// <summary>
        /// Получение записей промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="hashtagIds">Id хэштегов</param>
        /// <returns>Коллекция записей</returns>
        public async Task<List<HashtagNews>> GetCollectionByHashtagId(ICollection<Guid> hashtagIds)
        {
            var query = GetAll();
            return await query
                .Where(x => hashtagIds.Contains(x.HashtagId))
                .ToListAsync();
        }

        /// <summary>
        /// Удаление записи промежуточной таблицы, связывающей хэштеги и новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        public async void DeleteByNewsId(Guid newsId)
        {
            var collection = await GetAll().Where(x => x.NewsId == newsId).ToListAsync();
            DeleteRange(collection);
        }
    }
}
