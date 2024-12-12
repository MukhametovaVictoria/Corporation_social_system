using AutoMapper;
using BusinessLogic.Abstractions;
using BusinessLogic.Contracts.Employee;
using BusinessLogic.Contracts.Picture;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PictureService : IPictureService
    {
        private readonly IMapper _mapper;
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IMapper mapper, IPictureRepository pictureRepository)
        {
            _mapper = mapper;
            _pictureRepository = pictureRepository;
        }

        /// <summary>
        /// Получить картинку.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО картинки. </returns>
        public async Task<PictureDto> GetByIdAsync(Guid id)
        {
            var pic = await _pictureRepository.GetAsync(id);
            return _mapper.Map<PictureDto>(pic);
        }

        public async Task<ICollection<PictureDto>> GetCollectionByNewsIds(ICollection<Guid> newsIds)
        {
            ICollection<Picture> entities = await _pictureRepository.GetCollectionByNewsIds(newsIds);
            return _mapper.Map<ICollection<Picture>, ICollection<PictureDto>>(entities);
        }

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(Guid id)
        {
            _pictureRepository.Delete(id);
            await _pictureRepository.SaveChangesAsync();
        }
    }
}
