using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace VnStyle.Services.Data
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        #region Fields

        /// <summary>
        /// <see cref="DbContext"/>
        /// </summary>
        private readonly IDbContext _dbContext;

        /// <summary>
        /// <see cref="DbSet"/>
        /// </summary>
        private IDbSet<TEntity> _dbSet;


        public IQueryable<TEntity> Table => _dbSet;

        #endregion Fields

        #region "Constructors"

        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        #endregion

        public TEntity GetById(params object[] ids)
        {
            return _dbSet.Find(ids);
        }

        public virtual IQueryable<TEntity> GetAll(params string[] includes)
        {
            return Get(null, null, includes);
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> @where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = this.Table;
            //Apply eager loading
            dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
            if (where != null) dbQuery = dbQuery.Where(where);
            return orderBy != null ? orderBy(dbQuery) : dbQuery;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params string[] includeProperties)
        {
            var query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return orderBy != null ? orderBy(query) : query;
        }

        public TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public List<TEntity> InsertRange(IEnumerable<TEntity> listEntity)
        {
            return listEntity.Select(entity =>
            {
                _dbSet.Add(entity);
                return entity;
            }).ToList();
        }

        public void Update(TEntity entity, params string[] changedProes)
        {
            if (changedProes == null)
            {
                changedProes = new string[] { };
            }
            _dbSet.Attach(entity);
            if (changedProes.Any())
            {
                //Only change some properties
                foreach (string propertyName in changedProes)
                {
                    _dbContext.Entry(entity).Property(propertyName).IsModified = true;
                }
            }
            else
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public int Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            return Table.Where(filterExpression).Update(updateExpression);
        }

        //int Update<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression) where TEntity : class;

        public void Delete(TEntity entity)
        {
            try
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
            catch (Exception)
            {
                RefreshEntity(entity);
                throw;
            }
        }

        

        public virtual async Task<int> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).DeleteAsync();
        }

        public virtual int DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate).Delete();
        }

        public virtual int DeleteRange(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as TEntity[] ?? entities.ToArray();
            try
            {
                var result = ((DbSet<TEntity>)_dbSet).RemoveRange(enumerable);
                return result.Count();
            }
            catch
            {
                foreach (var entity in enumerable)
                {
                    RefreshEntity(entity);
                }
                throw;
            }
        }

        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? Table.Count() : Table.Count(filter);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? Table.AsNoTracking().Any() : Table.AsNoTracking().Any(filter);
        }

        
        public virtual void RefreshEntity(TEntity entityToReload)
        {
            _dbContext.Entry(entityToReload).Reload();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
