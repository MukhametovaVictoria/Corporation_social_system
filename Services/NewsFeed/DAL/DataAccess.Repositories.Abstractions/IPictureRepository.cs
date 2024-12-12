using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPictureRepository : IRepository<Picture, Guid>
    {
        Task<ICollection<Picture>> GetCollectionByNewsIds(ICollection<Guid> newsIds);
        Task DeleteByNewsId(Guid newsId);
    }
}
