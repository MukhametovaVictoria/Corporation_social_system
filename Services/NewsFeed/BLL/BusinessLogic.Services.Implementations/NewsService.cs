using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.HashtagNews;
using BusinessLogic.Contracts.LikedNews;
using BusinessLogic.Contracts.News;
using DataAccess.Common;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repositories;
using BusinessLogic.Contracts.Picture;
using Azure;

namespace BusinessLogic.Services
{
    public class NewsService : INewsService
    {
        private readonly IMapper _mapper;
        private readonly INewsRepository _newsRepository;

        public NewsService(IMapper mapper, INewsRepository newsRepository)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// Получить постраничный список.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Количество элементов на странице. </param>
        /// <returns> Список новостей. </returns>
        public async Task<ICollection<NewsDto>> GetPagedAsync(int page, int pageSize)
        {
            ICollection<News> entities = await _newsRepository.GetPagedAsync(page, pageSize);
            _newsRepository.JoinEntities(entities, true);
            return _mapper.Map<ICollection<News>, ICollection<NewsDto>>(entities);
        }

        /// <summary>
        /// Получение коллекции Новостей по объекту NewsSearch
        /// </summary>
        /// <param name="newsSearch">Объект расширенного поиска новостей</param>
        /// <returns>Коллекция новостей</returns>
        public async Task<ICollection<NewsDto>> GetCollection(NewsSearch newsSearch)
        {
            ICollection<News> entities = await _newsRepository.GetCollection(newsSearch);
            _newsRepository.JoinEntities(entities, true);
            return _mapper.Map<ICollection<News>, ICollection<NewsDto>>(entities);
        }

        /// <summary>
        /// Получить новость.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО новости. </returns>
        public async Task<NewsDto> GetByIdAsync(Guid id)
        {
            var news = await _newsRepository.GetAsync(id);
            _newsRepository.JoinEntities(new List<News>() { news }, true);
            return _mapper.Map<NewsDto>(news);
        }

        /// <summary>
        /// Создать новость.
        /// </summary>
        /// <param name="creatingNewsDto"> ДТО создаваемой новости. </param>
        public async Task<NewsDto> CreateAsync(CreatingNewsDto creatingNewsDto)
        {
            var news = _mapper.Map<CreatingNewsDto, News>(creatingNewsDto);
            var createdNews = await _newsRepository.AddAsync(news);
            await _newsRepository.SaveChangesAsync();
            _newsRepository.JoinAuthor(createdNews);
            _newsRepository.ClearLinks(createdNews);
            return _mapper.Map<NewsDto>(createdNews);
        }

        /// <summary>
        /// Удалить новость.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(Guid id)
        {
            _newsRepository.DeleteRelatedEntities(id);
            _newsRepository.Delete(id);
            await _newsRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Лайкнуть новость
        /// </summary>
        /// <param name="id">Id Новости</param>
        /// <param name="employeeId">Id Сотрудника</param>
        /// <returns></returns>
        public async Task<LikedNewsInfoDto> Like(Guid id, Guid employeeId)
        {
            var result = await _newsRepository.Like(id, employeeId);
            _newsRepository.SaveChanges();

            return _mapper.Map<LikedNewsInfo, LikedNewsInfoDto>(result);
        }

        /// <summary>
        /// Получить инфо по лайкам
        /// </summary>
        /// <param name="newsIds">Список новостей</param>
        /// <param name="currentEmployeeId">Пользователь, запросивший инфо</param>
        /// <returns>Инфо по лайкам с учетом лайков конкретного пользовател</returns>
        public async Task<ICollection<LikedNewsInfoDto>> GetLikes(ICollection<Guid> newsIds, Guid currentEmployeeId)
        {
            return _mapper.Map<ICollection<LikedNewsInfo>, ICollection<LikedNewsInfoDto>>(await _newsRepository.GetLikes(newsIds, currentEmployeeId));
        }

        /// <summary>
        /// Получить инфо по лайкам пользователя
        /// </summary>
        /// <param name="employeeId">Id Пользовател</param>
        /// <param name="page">Станица</param>
        /// <param name="itemsPerPage">Количество записей</param>
        /// <returns>Инфо по лайкам конкретного пользователя</returns>
        public async Task<ICollection<NewsDto>> GetLikedNewsByEmployee(Guid employeeId, int page, int itemsPerPage)
        {
            ICollection<News> entities = await _newsRepository.GetLikedNewsByEmployee(employeeId, page, itemsPerPage);
            _newsRepository.JoinEntities(entities, true);
            return _mapper.Map<ICollection<News>, ICollection<NewsDto>>(entities);
        }

        /// /// <summary>
        /// Изменить новость.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingNewsDto"> ДТО редактируемой новости. </param>
        public async Task UpdateAsync(UpdatingNewsDto updatingNewsDto)
        {
            var news = await _newsRepository.GetAsync(updatingNewsDto.Id);
            if (news == null)
            {
                throw new Exception($"Новость с идентификатором {updatingNewsDto.Id} не найдена");
            }
            if (updatingNewsDto.PictureList != null && updatingNewsDto.PictureList.Count > 0)
            {
                await _newsRepository.AddNewPicturesList(updatingNewsDto.PictureList.Select(x => _mapper.Map<UpdatingPictureDto, Picture>(x)).ToList());
            }

            _newsRepository.JoinEntities(new List<News>() { news }, false);

            news.Title = updatingNewsDto.Title;
            news.ShortDescription = updatingNewsDto.ShortDescription;
            news.Content = updatingNewsDto.Content;
            news.UpdatedAt = DateTime.Now;
            news.IsArchived = updatingNewsDto.IsArchived;
            news.IsPublished = updatingNewsDto.IsPublished;

            if((updatingNewsDto.HashtagNewsList != null && updatingNewsDto.HashtagNewsList.Count > 0) ||
                (news.HashtagNewsList != null && news.HashtagNewsList.Count > 0))
            {
                news.HashtagNewsList = _newsRepository.GetNewHashtagNewsList(news.HashtagNewsList, updatingNewsDto.HashtagNewsList.Select(x => _mapper.Map<CreatingHashtagNewsDto, HashtagNews>(x)).ToList());
            }

            _newsRepository.Update(news);
            await _newsRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Добавить стопку новостей
        /// </summary>
        /// <param name="entities">Список новостей</param>
        /// <returns></returns>
        public async Task AddRangeAsync(ICollection<CreatingNewsDto> entities)
        {
            await _newsRepository.AddRangeAsync(_mapper.Map<ICollection<CreatingNewsDto>, ICollection<News>>(entities));
            _newsRepository.SaveChanges();
        }

        public async Task ChangeVisibility(Guid newsId, bool isPublished, bool isArchived)
        {
            var news = await _newsRepository.GetAsync(newsId);
            if (news == null)
            {
                throw new Exception($"Новость с идентификатором {newsId} не найдена");
            }
            _newsRepository.JoinEntities(new List<News>() { news }, false);
            news.IsArchived = isArchived;
            news.IsPublished = isPublished;
            _newsRepository.Update(news);
            await _newsRepository.SaveChangesAsync();
        }
    }
}
