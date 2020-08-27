using SPA.Domain.Audit;
using SPA.Domain.Models;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SPA.Repository.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IDbModel
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private DbSet<TEntity> _dbset;
        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbset == null)
                {
                    _dbset = UnitOfWork.Context.Set<TEntity>();
                }
                return _dbset;
            }
        }

        protected virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = DbSet.Where(e => e.Deleted != 1);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual TEntity GetById(object id)
        {
            var entity = DbSet.Find(id);
            var result = new List<TEntity>();
            
            if (entity.Deleted != 1)
            {
                return entity;
            }
            return null;
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            var entity = DbSet.FindAsync(id);
            if (entity.Result.Deleted != 1)
            {
                return entity;
            }
            return DbSet.FindAsync(0);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }

        public void Create(TEntity entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            entity.UpdateDate = DateTime.UtcNow;
            entity.Deleted = 0;
            DbSet.Add(entity);
        }

        public void Create(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreateDate = DateTime.UtcNow;
                entity.UpdateDate = DateTime.UtcNow;
                entity.Deleted = 0;
            }
            DbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            var entity = DbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.Deleted = 1;
            Update(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> filter)
        {
            var objects = GetQueryable(filter);
            
            foreach (var obj in objects)
            {
                Delete(obj);
            }
        }

        public virtual void Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            if (UnitOfWork.Context.Entry(entity).State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            UnitOfWork.Context.Entry(entity).Property(nameof(entity.CreateDate)).IsModified = false;
        }

        public virtual void Update(TEntity entity, string[] fieldsToUpdate)
        {
            entity.UpdateDate = DateTime.UtcNow;

            this.DbSet.Attach(entity);
            var dbEntityEntry = UnitOfWork.Context.Entry(entity);
            var dbProperties = dbEntityEntry.GetDatabaseValues();
            foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
            {
                var original = dbProperties.GetValue<object>(property);
                var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                if (fieldsToUpdate.Contains(property) && (original == null && current != null || original != null && current == null || original != null 
                    && current != null) && ((original != null && !original.Equals(current)) || (current != null && !current.Equals(original))))
                {
                    dbEntityEntry.Property(property).IsModified = true;
                }
                else
                {
                    dbEntityEntry.Property(property).OriginalValue = dbEntityEntry.Property(property).CurrentValue = original;
                    dbEntityEntry.Property(property).IsModified = false;
                }
            }
        }
    }
}
