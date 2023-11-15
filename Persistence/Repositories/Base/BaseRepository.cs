using Microsoft.EntityFrameworkCore;
using Persistence.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected AppDbContext appDbContext;
        private readonly DbSet<TEntity> dbSet;
        public BaseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.dbSet = appDbContext.Set<TEntity>();
        }
        public async virtual Task<List<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
             params string[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }
            if (orderBy != null)
                query = orderBy(query);
            return await query.ToListAsync();
        }

        public async virtual Task<int> GetPagedCount(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync();
        }

        public async virtual Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();

        }
        public async virtual Task<List<TEntity>> GetWhereAsNoTracking(Expression<Func<TEntity, bool>>? filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.AsNoTracking().ToListAsync();

        }

        public async virtual Task<List<TEntity>> GetPaged(Expression<Func<TEntity, bool>>? filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           int pageIndex = 0,
           int pageSize = 0,
           params string[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);
            if (pageIndex != default(int) && pageSize != default(int))
            {
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return await query.ToListAsync();
        }
        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null,
          params string[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);
            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }
            return query.FirstOrDefault();
        }
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void AddRange(IEnumerable<TEntity> entity)
        {
            dbSet.AddRange(entity);
        }
        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);
            //appDbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.AttachRange(entities);
            appDbContext.Entry(entities).State = EntityState.Modified;
        }
        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (appDbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>>? filter = null,
           params string[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
            {
                foreach (string include in includes)
                    query = query.Include(include);
            }

            TEntity entityToDelete = query.FirstOrDefault(filter);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }
        public virtual void DeleteRange(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            IQueryable<TEntity> entitiesToDelete = query.Where(filter);
            foreach (var item in entitiesToDelete)
            {
                Delete(item);
            }
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (appDbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
    }

}
