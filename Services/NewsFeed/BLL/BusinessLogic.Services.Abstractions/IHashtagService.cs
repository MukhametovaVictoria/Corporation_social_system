using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Contracts.Hashtag;

namespace BusinessLogic.Abstractions
{
    public interface IHashtagService
    {
        /// <summary>
        /// Получить хештег.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО хештега. </returns>
        Task<HashtagDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать хештег.
        /// </summary>
        /// <param name="creatingHashtagDto"> ДТО хештега. </param>
        Task<Guid> CreateAsync(CreatingHashtagDto creatingHashtagDto);

        ICollection<HashtagDto> GetCollection(ICollection<Guid> ids);
    }
}
