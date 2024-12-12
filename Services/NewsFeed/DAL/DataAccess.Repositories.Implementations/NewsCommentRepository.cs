using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class NewsCommentRepository : Repository<NewsComment, Guid>, INewsCommentRepository
    {
        public NewsCommentRepository(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список комментариев. </returns>
        public async Task<List<NewsComment>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        /// <summary>
        /// Получение коллекции комментариев к Новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по новости</returns>
        public async Task<List<NewsComment>> GetCollectionByNewsId(Guid newsId, int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Where(x => x.NewsId == newsId)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        /// <summary>
        /// Удаление комментариев по новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        public async Task DeleteByNewsId(Guid newsId)
        {
            var collection = await GetAll().Where(x => x.NewsId == newsId).ToListAsync();
            DeleteRange(collection);
        }

        /// <summary>
        /// Удаление комментариев по автору.
        /// </summary>
        /// <param name="authorId">Id автора</param>
        public async Task DeleteByAuthorId(Guid authorId)
        {
            var collection = await GetAll().Where(x => x.AuthorId == authorId).ToListAsync();
            DeleteRange(collection);
        }

        /// <summary>
        /// Получение коллекции комментариев к Новости
        /// </summary>
        /// <param name="employeeId">Id новости</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по новости</returns>
        public async Task<List<NewsComment>> GetCollectionByEmployeeId(Guid employeeId, int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Where(x => x.AuthorId == employeeId)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        /// <summary>
        /// Получение коллекции данных по комментариям
        /// </summary>
        /// <param name="employeeId">Id сотрудника, запросившего инфо</param>
        /// <param name="newsIds">Коллекция Id новостей</param>
        /// <returns>Коллекция данных по комментариям</returns>
        public async Task<List<NewsCommentInfo>> CheckIsAuthor(Guid employeeId, ICollection<Guid> newsIds)
        {
            var query = GetAll();
            var result = await query
                .Where(x => newsIds.Contains(x.NewsId))
                .ToListAsync();

            var list = new List<NewsCommentInfo>();
            foreach (var comment in result)
            {
                list.Add(new NewsCommentInfo() { NewsId = comment.NewsId, CommentId = comment.Id, IsAuthor = comment.AuthorId == employeeId });
            }

            return list;
        }
    }
}
