using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IRepositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           params string[] includes);

        Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params string[] includes);

        Task<List<TEntity>> GetPaged(Expression<Func<TEntity, bool>>? filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           int pageIndex = 0,
           int pageSize = 0,
           params string[] includes);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null,
          params string[] includes);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(int id);
        public void Delete(Expression<Func<TEntity, bool>>? filter = null,
           params string[] includes);
        void DeleteRange(Expression<Func<TEntity, bool>>? filter = null);
        Task<List<TEntity>> GetWhereAsNoTracking(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes);
    }
}
