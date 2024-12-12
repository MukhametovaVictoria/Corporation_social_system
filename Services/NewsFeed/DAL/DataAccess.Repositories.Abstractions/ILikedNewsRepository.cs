using DataAccess.Entities;
using System;

namespace DataAccess.Repositories
{
    public interface ILikedNewsRepository : IRepository<LikedNews, Guid>
    {
    }
}
