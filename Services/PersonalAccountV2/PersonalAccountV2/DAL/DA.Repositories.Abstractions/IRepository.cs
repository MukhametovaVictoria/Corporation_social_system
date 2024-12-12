using DA.Common.Sql;
using DA.Entities;

namespace DA.Repositories.Abstractions
{
    /// <summary>
    /// Описания общих методов для всех репозиториев.
    /// </summary>
    /// <typeparam name="T"> Тип Entity для репозитория. </typeparam>
    /// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
    public interface IRepository<T, TPrimaryKey>
        where T : IEntity<TPrimaryKey>
    {
        IQueryable<T> GetCollection(ICollection<TPrimaryKey> ids);

        IQueryable<T> GetCollection(MappingQuery mapping);

        IQueryable<T> GetAll(bool noTracking = false);

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

        T Get(TPrimaryKey id);

        Task<T> GetAsync(TPrimaryKey id);

        bool Delete(TPrimaryKey id);

        bool Delete(T entity);

        bool DeleteRange(ICollection<T> entities);

        void Update(T entity);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        void AddRange(List<T> entities);

        Task AddRangeAsync(ICollection<T> entities);
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
