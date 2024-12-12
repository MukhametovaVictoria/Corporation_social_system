using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.NewsComment;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class NewsCommentService : INewsCommentService
    {
        private readonly IMapper _mapper;
        private readonly INewsCommentRepository _newsCommentRepository;

        public NewsCommentService(IMapper mapper, INewsCommentRepository newsCommentRepository)
        {
            _mapper = mapper;
            _newsCommentRepository = newsCommentRepository;
        }

        /// <summary>
        /// Получить список комментариев.
        /// </summary>
        /// <param name="page"> Номер страницы. </param>
        /// <param name="pageSize"> Объем страницы. </param>
        /// <returns> Список комментариев. </returns>
        public async Task<ICollection<NewsCommentDto>> GetPagedAsync(int page, int pageSize)
        {
            ICollection<NewsComment> entities = await _newsCommentRepository.GetPagedAsync(page, pageSize);
            return _mapper.Map<ICollection<NewsComment>, ICollection<NewsCommentDto>>(entities);
        }

        /// <summary>
        /// Получить комментарий новости.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО комментария. </returns>
        public async Task<NewsCommentDto> GetByIdAsync(Guid id)
        {
            var news = await _newsCommentRepository.GetAsync(id);
            return _mapper.Map<NewsCommentDto>(news);
        }

        /// <summary>
        /// Создать комментарий новости.
        /// </summary>
        /// <param name="creatingNewsCommentDto"> ДТО создаваемого комментария. </param>
        public async Task<Guid> CreateAsync(CreatingNewsCommentDto creatingNewsCommentDto)
        {
            var news = _mapper.Map<CreatingNewsCommentDto, NewsComment>(creatingNewsCommentDto);
            var createdNews = await _newsCommentRepository.AddAsync(news);
            await _newsCommentRepository.SaveChangesAsync();
            return createdNews.Id;
        }

        /// <summary>
        /// Удалить комментарий новости.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(Guid id)
        {
            _newsCommentRepository.Delete(id);
            await _newsCommentRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Изменить комментарий новости.
        /// </summary>
        /// <param name="id"> Иентификатор. </param>
        /// <param name="updatingNewsCommentDto"> ДТО редактируемого комментария. </param>
        public async Task UpdateAsync(Guid id, UpdatingNewsCommentDto updatingNewsCommentDto)
        {
            var news = await _newsCommentRepository.GetAsync(id);
            if (news == null)
            {
                throw new Exception($"Комментарий с идентфикатором {id} не найдена");
            }

            news.Content = updatingNewsCommentDto.Content;
            news.UpdatedAt = DateTime.Now;
            _newsCommentRepository.Update(news);
            await _newsCommentRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получение коллекции комментариев к Новости
        /// </summary>
        /// <param name="newsId">Id новости</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по новости</returns>
        public async Task<List<NewsCommentDto>> GetCollectionByNewsId(Guid newsId, int page, int itemsPerPage)
        {
            var news = await _newsCommentRepository.GetCollectionByNewsId(newsId, page, itemsPerPage);
            return _mapper.Map<List<NewsCommentDto>>(news);
        }

        /// <summary>
        /// Получение коллекции комментариев к Новости
        /// </summary>
        /// <param name="employeeId">Id новости</param>
        /// <param name="page">Страница</param>
        /// <param name="itemsPerPage">Количество на странице</param>
        /// <returns>Коллекция комментариев по новости</returns>
        public async Task<List<NewsCommentDto>> GetCollectionByEmployeeId(Guid employeeId, int page, int itemsPerPage)
        {
            var news = await _newsCommentRepository.GetCollectionByEmployeeId(employeeId, page, itemsPerPage);
            return _mapper.Map<List<NewsCommentDto>>(news);
        }

        /// <summary>
        /// Получение коллекции данных по комментариям
        /// </summary>
        /// <param name="employeeId">Id сотрудника, запросившего инфо</param>
        /// <param name="newsIds">Коллекция Id новостей</param>
        /// <returns>Коллекция данных по комментариям</returns>
        public async Task<List<NewsCommentInfoDto>> CheckIsAuthor(Guid employeeId, ICollection<Guid> newsIds)
        {
            var result = await _newsCommentRepository.CheckIsAuthor(employeeId, newsIds);
            return _mapper.Map<List<NewsCommentInfoDto>>(result);
        }
    }
}
