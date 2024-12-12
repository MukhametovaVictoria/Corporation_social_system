using BusinessLogic.Contracts.Picture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IPictureService
    {
        Task<PictureDto> GetByIdAsync(Guid id);
        Task<ICollection<PictureDto>> GetCollectionByNewsIds(ICollection<Guid> newsIds);

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        Task DeleteAsync(Guid id);
    }
}
