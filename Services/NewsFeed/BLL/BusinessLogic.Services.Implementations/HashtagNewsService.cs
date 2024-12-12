using AutoMapper;
using Azure;
using BusinessLogic.Contracts.HashtagNews;
using BusinessLogic.Contracts.NewsComment;
using DataAccess.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Abstractions;
using DataAccess.Repositories;

namespace BusinessLogic.Services
{
    public class HashtagNewsService : IHashtagNewsService
    {
        private readonly IMapper _mapper;
        private readonly IHashtagNewsRepository _hashtagNewsRepository;

        public HashtagNewsService(IMapper mapper, IHashtagNewsRepository hashtagNewsRepository)
        {
            _mapper = mapper;
            _hashtagNewsRepository = hashtagNewsRepository;
        }

        /// <summary>
        /// Получить связку.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО связки. </returns>
        public async Task<HashtagNewsDto> GetByIdAsync(Guid id)
        {
            var HashtagNews = await _hashtagNewsRepository.GetAsync(id);
            return _mapper.Map<HashtagNewsDto>(HashtagNews);
        }

        /// <summary>
        /// Создать связку.
        /// </summary>
        /// <param name="creatingHashtagNewsDto"> ДТО создаваемой связки. </param>
        public async Task<Guid> CreateAsync(CreatingHashtagNewsDto creatingHashtagNewsDto)
        {
            var HashtagNews = _mapper.Map<CreatingHashtagNewsDto, HashtagNews>(creatingHashtagNewsDto);
            var createdHashtagNews = await _hashtagNewsRepository.AddAsync(HashtagNews);
            await _hashtagNewsRepository.SaveChangesAsync();
            return createdHashtagNews.Id;
        }

        /// <summary>
        /// Удалить связку.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(Guid id)
        {
            _hashtagNewsRepository.Delete(id);
            await _hashtagNewsRepository.SaveChangesAsync();
        }

        public async Task<ICollection<HashtagNewsDto>> GetCollectionByNewsIds(ICollection<Guid> newsIds)
        {
            var news = await _hashtagNewsRepository.GetCollectionByNewsId(newsIds);
            return _mapper.Map<List<HashtagNewsDto>>(news);
        }
    }
}
