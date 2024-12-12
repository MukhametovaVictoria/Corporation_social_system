using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public interface IHashtagRepository : IRepository<Hashtag, Guid>
    {
        /// <summary>
        /// Получение коллекции хэштегов по названиям хэштегов
        /// </summary>
        /// <param name="hashtagNames">Коллекция наименований</param>
        /// <returns>Коллекция хештегов</returns>
        Task<List<Hashtag>> GetCollectionByNames(ICollection<string> hashtagNames);
    }
}
