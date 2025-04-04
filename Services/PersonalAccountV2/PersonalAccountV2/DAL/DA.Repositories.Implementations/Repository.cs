﻿using DA.Common.Sql;
using DA.Common.Sql.MSsql;
using DA.Entities;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Implementations
{
    public abstract class Repository<T, TPrimaryKey> where T
        : class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext Context;
        private readonly DbSet<T> _entitySet;

        protected Repository(DbContext context)
        {
            Context = context;
            _entitySet = Context.Set<T>();
        }

        #region Get


        public virtual IQueryable<T> GetCollection(ICollection<TPrimaryKey> ids)
        {
            return _entitySet.Where(x => ids.Contains(x.Id));
        }


        public IQueryable<T> GetCollection(MappingQuery mapping)
        {
            if (mapping == null)
                return null;

            var script = SqlScriptPreparerHelper.GetSelectQuery(mapping);
            var collection = _entitySet.FromSqlRaw(script);
            return collection;
        }


        public virtual T Get(TPrimaryKey id)
        {
            return _entitySet.Find(id);
        }


        public virtual async Task<T> GetAsync(TPrimaryKey id)
        {
            return await _entitySet.FindAsync((object)id);
        }

        #endregion

        #region GetAll


        public virtual IQueryable<T> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
        }


        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }


        #endregion

        #region Create


        public virtual T Add(T entity)
        {
            var objToReturn = _entitySet.Add(entity);
            return objToReturn.Entity;
        }


        public virtual async Task<T> AddAsync(T entity)
        {
            return (await _entitySet.AddAsync(entity)).Entity;
        }


        public virtual void AddRange(List<T> entities)
        {
            var enumerable = entities as IList<T> ?? entities.ToList();
            _entitySet.AddRange(enumerable);
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }
            await _entitySet.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        public virtual void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region Delete

        public virtual bool Delete(TPrimaryKey id)
        {
            var obj = _entitySet.Find(id);
            if (obj == null)
            {
                return false;
            }
            _entitySet.Remove(obj);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            Context.Entry(entity).State = EntityState.Deleted;
            return true;
        }

        public virtual bool DeleteRange(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return false;
            }
            _entitySet.RemoveRange(entities);
            return true;
        }

        #endregion

        #region SaveChanges

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
