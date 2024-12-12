using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Contracts.HashtagNews;

namespace BusinessLogic.Abstractions
{
    public interface IHashtagNewsService
    {

        /// <summary>
        /// Получить связку.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО связки. </returns>
        Task<HashtagNewsDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать связку.
        /// </summary>
        /// <param name="creatingHashtagNewsDto"> ДТО создаваемой связки. </param>
        Task<Guid> CreateAsync(CreatingHashtagNewsDto creatingHashtagNewsDto);

        /// <summary>
        /// Удалить связку.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);

        Task<ICollection<HashtagNewsDto>> GetCollectionByNewsIds(ICollection<Guid> newsIds);
    }
}
