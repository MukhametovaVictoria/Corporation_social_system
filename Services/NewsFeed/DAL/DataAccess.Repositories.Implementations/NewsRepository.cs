using DataAccess.Common;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Context;
using System.Net.Http.Headers;
using System.IO;

namespace DataAccess.Repositories
{
    /// <summary>
    /// Репозиторий работы с новостями.
    /// </summary>
    public class NewsRepository : Repository<News, Guid>, INewsRepository
    {
        protected readonly DataContext _dataContext;
        public NewsRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="itemsPerPage"> Количество элементов на странице. </param>
        /// <returns> Список новостей. </returns>
        public async Task<List<News>> GetPagedAsync(int page, int itemsPerPage)
        {
            var query = GetAll();
            return await query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        /// <summary>
        /// Получение коллекции Новостей по объекту NewsSearch
        /// </summary>
        /// <param name="newsSearch">Объект расширенного поиска новостей</param>
        /// <returns>Коллекция новостей</returns>
        public async Task<List<News>> GetCollection(NewsSearch newsSearch)
        {   
            var query = GetAll();
            var collection = await query
                .Where(x => !string.IsNullOrEmpty(newsSearch.Title) ? x.Title.Contains(newsSearch.Title) : true)
                    .Where(x => !string.IsNullOrEmpty(newsSearch.Body) ? x.Content.Contains(newsSearch.Body) : true)
                    .Where(x => newsSearch.From != DateTime.MinValue ? x.CreatedAt >= newsSearch.From : true)
                    .Where(x => newsSearch.To != DateTime.MinValue ? x.CreatedAt <= newsSearch.To : true)
                    .Where(x => newsSearch.AuthorId != Guid.Empty ? x.AuthorId == newsSearch.AuthorId : true)
                    .Where(x => newsSearch.IsPublished == x.IsPublished && newsSearch.IsArchived == x.IsArchived)
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(newsSearch.Skip)
                    .Take(newsSearch.Take > 0 ? newsSearch.Take : 100)
                    .ToListAsync();

            if (newsSearch.Hashtags != null && newsSearch.Hashtags.Count > 0)
            {
                var names = newsSearch.Hashtags.Select(x => x.Name).ToArray();
                var hashtagsFromDb = new HashtagRepository(_dataContext).GetCollectionByNames(names).Result;
                var hashtagIds = hashtagsFromDb != null && hashtagsFromDb.Count > 0 ? hashtagsFromDb.Select(x => x).Select(x => x.Id).ToList() : new List<Guid>();

                var hashtagsPosts = new HashtagNewsRepository(_dataContext).GetCollectionByHashtagId(hashtagIds).Result;
                var newsIds = hashtagsPosts != null && hashtagsPosts.Count > 0 ? hashtagsPosts.Select(x => x).Select(x => x.NewsId).ToList() : new List<Guid>();
                collection = collection.Where(x => newsIds.Contains(x.Id)).ToList();
            }

            return collection;
        }

        /// <summary>
        /// Удаление связанных сущностей.
        /// </summary>
        /// <param name="newsId"> Id новости </param>
        public async void DeleteRelatedEntities(Guid newsId)
        {
            if (newsId == Guid.Empty)
                return;
            
            var hashtagsNewsRep = new HashtagNewsRepository(_dataContext);
            hashtagsNewsRep.DeleteByNewsId(newsId);

            var newsCommentRep = new NewsCommentRepository(_dataContext);
            await newsCommentRep.DeleteByNewsId(newsId);

            var picturesRep = new PictureRepository(_dataContext);
            await picturesRep.DeleteByNewsId(newsId);
        }

        /// <summary>
        /// Создание новости с хештегами.
        /// </summary>
        /// <param name="news">Новость</param>
        /// <returns>Новость</returns>
        public override News Add(News news)
        {
            var hNames = news.HashtagNewsList != null && news.HashtagNewsList.Count > 0 ? 
                news.HashtagNewsList
                .Where(x => x.Hashtag != null && !String.IsNullOrEmpty(x.Hashtag.Name))
                .Select(x => x.Hashtag.Name)
                .ToList() : null;

            news.HashtagNewsList = PrepareHashtagNews(hNames).Result;
            if(news.PictureList != null && news.PictureList.Count > 0)
                news.PictureList = PreparePictures(news.PictureList);
            return _dataContext.Set<News>().Add(news).Entity;
        }

        /// <summary>
        /// Создание новости с хештегами.
        /// </summary>
        /// <param name="news">Новость</param>
        /// <returns>Новость</returns>
        public override async Task<News> AddAsync(News news)
        {
            var hNames = news.HashtagNewsList != null && news.HashtagNewsList.Count > 0 ?
                news.HashtagNewsList
                .Where(x => x.Hashtag != null && !String.IsNullOrEmpty(x.Hashtag.Name))
                .Select(x => x.Hashtag.Name)
                .ToList() : null;

            news.HashtagNewsList = await PrepareHashtagNews(hNames);
            if (news.PictureList != null && news.PictureList.Count > 0)
                news.PictureList = PreparePictures(news.PictureList);
            return (await _dataContext.Set<News>().AddAsync(news)).Entity;
        }

        /// <summary>
        /// Джоин связанных сущностей
        /// </summary>
        /// <param name="news">Список новостей</param>
        public void JoinEntities(ICollection<News> news, bool needClear = true)
        {
            var authorsIds = news.Select(x => x.AuthorId).Distinct().ToList();
            var authors = GetEmployees(authorsIds);

            var hashtagNews = GetHashtagNews(news.Select(x => x.Id).ToList());
            var hashtags = GetHashtags(hashtagNews.Select(x => x.HashtagId).Distinct().ToList());
            var pictures = GetPictures(news.Select(x => x.Id).ToList());

            foreach (var item in news)
            {
                item.Author = authors.FirstOrDefault(x => x.Id == item.AuthorId);
                item.HashtagNewsList = hashtagNews.Where(x => x.NewsId == item.Id).ToList();
                if (item.HashtagNewsList != null && item.HashtagNewsList.Count > 0)
                {
                    foreach (var hn in item.HashtagNewsList) 
                    {
                        hn.Hashtag = hashtags.FirstOrDefault(x => x.Id == hn.HashtagId);
                    }
                }
                if(pictures != null)
                    item.PictureList = pictures.Where(x => x.NewsId == item.Id).ToList();

                if(needClear)
                    ClearLinks(item);
            }
        }

        /// <summary>
        /// Джоин сущности автора
        /// </summary>
        /// <param name="news">Новость</param>
        public void JoinAuthor(News news)
        {
            var author = GetEmployees(new List<Guid>() { news.AuthorId }).FirstOrDefault();
            if (author != null)
            {
                author.NewsList = null;
                author.NewsCommentList = null;
            }

            news.Author = author;   
        }

        /// <summary>
        /// Зачистка ссылок для предотвращения зацикливания
        /// </summary>
        /// <param name="news">Новость</param>
        public void ClearLinks(News news)
        {
            if (news.Author != null)
            {
                news.Author.NewsList = null;
                news.Author.NewsCommentList = null;
                news.Author.PictureList = null;
            }

            if (news.HashtagNewsList != null && news.HashtagNewsList.Count > 0)
            {
                foreach (var hn in news.HashtagNewsList)
                {
                    hn.News = null;

                    if (hn.Hashtag != null)
                        hn.Hashtag.HashtagNewsList = null;
                }
            }

            if(news.NewsCommentList != null)
            {
                foreach(var comment  in news.NewsCommentList)
                {
                    comment.News = null;
                    if (comment.Author != null)
                    {
                        comment.Author.NewsList = null;
                        comment.Author.NewsCommentList = null;
                        comment.Author.PictureList = null;
                    }
                }
            }

            if(news.PictureList != null)
            {
                foreach(var pic in news.PictureList)
                {
                    pic.News = null;
                    if (pic.Author != null)
                    {
                        pic.Author.NewsList = null;
                        pic.Author.NewsCommentList = null;
                        pic.Author.PictureList = null;
                    }
                }
            }
        }

        /// <summary>
        /// Обновление списка хештегов новости
        /// </summary>
        /// <param name="oldList">Старый список</param>
        /// <param name="newList">Новый список</param>
        /// <returns>Новый список</returns>
        public List<HashtagNews> GetNewHashtagNewsList(ICollection<HashtagNews> oldList, ICollection<HashtagNews> newList)
        {
            var forDelete = new List<HashtagNews>();
            foreach (var oldHN in oldList)
            {
                if (!newList.Select(x => x.Id).ToList().Contains(oldHN.Id))
                    forDelete.Add(oldHN);
            }

            if(forDelete.Count > 0)
            {
                var hnRepo = new HashtagNewsRepository(_dataContext);
                hnRepo.DeleteRange(forDelete);
            }

            var newHNList = newList.Where(x => x.Id == Guid.Empty && (x.Hashtag != null || x.HashtagId != Guid.Empty)).ToList();
            var newHashtagNames = newHNList.Where(x => x.Hashtag != null).Select(x => x.Hashtag).Where(x => !String.IsNullOrEmpty(x.Name)).Select(x => x.Name).ToList();
            var hashtagsIds = newHNList.Where(x => x.HashtagId != Guid.Empty).Select(x => x.HashtagId).ToList();
            
            var result = new List<HashtagNews>(); 
            if (newHNList.Count > 0)
            {
                var createdHashtags = newHashtagNames != null && newHashtagNames.Count > 0 ? GetHashtags(newHashtagNames) : new List<Hashtag>();
                createdHashtags.AddRange(GetHashtags(hashtagsIds));
                
                foreach (var newHN in newList)
                {
                    if(newHN.Id == Guid.Empty)
                    {
                        //Пустой объект
                        if (newHN.HashtagId == Guid.Empty && (newHN.Hashtag == null || String.IsNullOrEmpty(newHN.Hashtag.Name)))
                            continue;

                        var hashtag = newHN.Hashtag != null ? createdHashtags.Where(x => x.Name == newHN.Hashtag.Name).FirstOrDefault() :
                            (newHN.HashtagId != Guid.Empty ? createdHashtags.Where(x => x.Id == newHN.HashtagId).FirstOrDefault() : null);

                        if(hashtag != null)
                        {
                            newHN.Hashtag = hashtag;
                            newHN.HashtagId = hashtag.Id;
                        }
                    }

                    result.Add(newHN);
                }
            }

            return result;
        }

        public async Task AddNewPicturesList(ICollection<Picture> newList)
        {
            var picRepo = new PictureRepository(_dataContext);
            await picRepo.AddRangeAsync(newList);
            await picRepo.SaveChangesAsync();
        }

        /// <summary>
        /// Лайкнуть новость
        /// </summary>
        /// <param name="id">Id Новости</param>
        /// <param name="employeeId">Id Сотрудника</param>
        /// <returns></returns>
        public async Task<LikedNewsInfo> Like(Guid id, Guid employeeId)
        {
            var repoLN = new LikedNewsRepository(_dataContext);
            var query = repoLN.GetAll();
            var likes = query.Where(x => x.NewsId == id && x.EmployeeId == employeeId).ToList();
            var count = query.Where(x => x.NewsId == id).ToList().Count;
            var newsInfo = new LikedNewsInfo()
            {
                NewsId = id,
                LikesCount = count,
            };
            if (likes == null || likes.Count == 0)
            {
                await repoLN.AddAsync(new LikedNews() { NewsId = id, EmployeeId = employeeId });
                newsInfo.IsLiked = true;
                newsInfo.LikesCount += 1;
            }
            else
            {
                repoLN.DeleteRange(likes);
                newsInfo.IsLiked = false;
                newsInfo.LikesCount = newsInfo.LikesCount > 0 ? newsInfo.LikesCount - 1 : 0;
            }
            return newsInfo;
        }

        /// <summary>
        /// Получить инфо по лайкам
        /// </summary>
        /// <param name="newsIds">Список новостей</param>
        /// <param name="currentEmployeeId">Пользователь, запросивший инфо</param>
        /// <returns>Инфо по лайкам с учетом лайков конкретного пользовател</returns>
        public async Task<ICollection<LikedNewsInfo>> GetLikes(ICollection<Guid> newsIds, Guid currentEmployeeId)
        {
            var likes = await GetLikesByNews(newsIds);
            var list = new List<LikedNewsInfo>();
            foreach(var id in newsIds)
            {
                var count = likes.Where(x => x.NewsId == id).ToArray().Length;
                list.Add(new LikedNewsInfo() { 
                    NewsId = id, 
                    LikesCount = count, 
                    IsLiked = likes.FirstOrDefault(x => x.NewsId == id && x.EmployeeId == currentEmployeeId) != null });
            }

            return list;
        }

        /// <summary>
        /// Получить инфо по лайкам пользователя
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="itemsPerPage">Количество элементов на странице</param>
        /// <returns></returns>
        public async Task<ICollection<News>> GetLikedNewsByEmployee(Guid employeeId, int page, int itemsPerPage)
        {
            var likedNews = await GetLikesByEmployee(employeeId);
            var newsList = likedNews.Select(x => x.NewsId).ToList();
            var query = GetAll();
            return await query
                .Where(x => newsList.Contains(x.Id))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }

        /// <summary>
        /// Получить список лайков
        /// </summary>
        /// <param name="newsIds">Id Новостей</param>
        /// <returns>Список лайков</returns>
        public async Task<ICollection<LikedNews>> GetLikesByNews(ICollection<Guid> newsIds)
        {
            var repoLN = new LikedNewsRepository(_dataContext);
            var query = repoLN.GetAll();
            return await query.Where(x => newsIds.Contains(x.NewsId)).ToListAsync();
        }

        /// <summary>
        /// Получить список лайков по сотруднику
        /// </summary>
        /// <param name="employeeId">Id сотрудника</param>
        /// <returns>Список лайков</returns>
        public async Task<ICollection<LikedNews>> GetLikesByEmployee(Guid employeeId)
        {
            var repoLN = new LikedNewsRepository(_dataContext);
            var query = repoLN.GetAll();
            return await query.Where(x => x.EmployeeId == employeeId).ToListAsync();
        }

        #region Private

        /// <summary>
        /// Получить коллекцию авторов по коллекции Id
        /// </summary>
        /// <param name="ids">Id авторов</param>
        /// <returns>Коллекция авторов</returns>
        private IQueryable<Employee> GetEmployees(ICollection<Guid> ids)
        {
            var repoEmp = new EmployeeRepository(_dataContext);
            return repoEmp.GetCollection(ids);
        }

        /// <summary>
        /// Получить коллекцию сущностей таблицы, связывающей хештеги и новости 
        /// </summary>
        /// <param name="newsIds">Id новостей</param>
        /// <returns>Коллекция сущностей таблицы, связывающей хештеги и новости </returns>
        private List<HashtagNews> GetHashtagNews(ICollection<Guid> newsIds)
        {
            var repoHN = new HashtagNewsRepository(_dataContext);
            return repoHN.GetCollectionByNewsId(newsIds).Result;
        }

        /// <summary>
        /// Получить коллекцию хештегов по Id
        /// </summary>
        /// <param name="ids">Id хештегов</param>
        /// <returns>Коллекция хештегов</returns>
        private IQueryable<Hashtag> GetHashtags(ICollection<Guid> ids)
        {
            var repoH = new HashtagRepository(_dataContext);
            return repoH.GetCollection(ids);
        }

        /// <summary>
        /// Получить коллекцию хештегов по Id
        /// </summary>
        /// <param name="names">Наименования хештегов</param>
        /// <returns>Коллекция хештегов</returns>
        private List<Hashtag> GetHashtags(ICollection<string> names)
        {
            var repoH = new HashtagRepository(_dataContext);
            return repoH.GetCollectionByNames(names).Result;
        }
        /// <summary>
        /// Получить коллекцию картинок по Id
        /// </summary>
        /// <param name="ids">Id картинок</param>
        /// <returns>Коллекция картинок</returns>
        private ICollection<Picture> GetPictures(ICollection<Guid> ids)
        {
            var repoH = new PictureRepository(_dataContext);
            return repoH.GetCollectionByNewsIds(ids).Result;
        }


        /// <summary>
        /// Создание коллекции сущностей таблицы, связывающей хештеги и новости
        /// </summary>
        /// <param name="hNames">Коллекция названий хештегов</param>
        /// <returns>Коллекция сущностей таблицы, связывающей хештеги и новости</returns>
        private async Task<List<HashtagNews>> PrepareHashtagNews(List<string> hNames)
        {
            if (hNames != null && hNames.Count > 0)
            {
                var hashtagRep = new HashtagRepository(_dataContext);
                var createdHashtags = await hashtagRep.GetCollectionByNames(hNames);
                var hashtagNewsRep = new HashtagNewsRepository(_dataContext);
                var createdHashtagsNames = createdHashtags != null ? createdHashtags.Select(x => x.Name).ToList() : null;

                var hnList = new List<HashtagNews>();

                foreach (var name in hNames)
                {
                    if (String.IsNullOrEmpty(name))
                        continue;

                    var hashtagAdded = createdHashtagsNames != null &&
                                        createdHashtagsNames.Count > 0 &&
                                        createdHashtagsNames.Contains(name) ?
                                        createdHashtags.FirstOrDefault(x => x.Name == name) : null;
                   
                    var newHN = hashtagAdded != null ? new HashtagNews()
                    {
                        HashtagId = hashtagAdded.Id,
                        Hashtag = hashtagAdded,
                    } :
                    new HashtagNews(){ Hashtag = new Hashtag() { Name = name } };
                    hnList.Add(newHN);
                }

                return hnList;
            }

            return null;
        }

        private ICollection<Picture> PreparePictures(ICollection<Picture> pictures)
        {
            foreach (var picture in pictures)
            {
                if(picture != null)
                {
                    if(picture.Data != null && picture.Data.Length > 0)
                    {
                        if (String.IsNullOrEmpty(picture.ByteAsString))
                        {
                            picture.ByteAsString = Convert.ToBase64String(picture.Data);
                        }
                        if (picture.Size == 0)
                        {
                            picture.Size = picture.Data.Length;
                        }
                    }
                    if (!String.IsNullOrEmpty(picture.Name))
                    {
                        if (String.IsNullOrEmpty(picture.Format))
                        {
                            picture.Format = Path.GetExtension(picture.Name);
                        }
                    }
                }
            }

            return pictures;
        }

        #endregion
    }
}