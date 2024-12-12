using DataAccess.Entities;
using System;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class LikedNewsRepository : Repository<LikedNews, Guid>, ILikedNewsRepository
    {
        public LikedNewsRepository(DataContext context) : base(context)
        {
        }
    }
}
