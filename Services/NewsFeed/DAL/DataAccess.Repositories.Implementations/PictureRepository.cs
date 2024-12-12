using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class PictureRepository : Repository<Picture, Guid>, IPictureRepository
    {
        protected readonly DataContext _dataContext;
        public PictureRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        /// <summary>
        /// Получить список пользователей по фильтрам
        /// </summary>
        /// <param name="filterPicture">Фильтры</param>
        /// <returns>Коллекция пользователей</returns>
        public async Task<ICollection<Picture>> GetCollectionByNewsIds(ICollection<Guid> newsIds)
        {
            var query = GetAll();
            var collection = await query
                .Where(x => newsIds.Contains(x.NewsId)).ToListAsync();
            
            return collection;
        }

        public async Task DeleteByNewsId(Guid newsId)
        {
            var collection = await GetAll().Where(x => x.NewsId == newsId).ToListAsync();
            DeleteRange(collection);
        }
    }
}
