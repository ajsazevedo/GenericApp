using GenericApp.Domain.Models.Base;
using GenericApp.Infra.Common.Objects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace GenericApp.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity> : IRepository
           where TEntity : BaseEntity
    {
        // Common Single
        TEntity Get(params object[] id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);

        // Common Range
        IPagedList<TEntity> GetPagedData(PagedDataSpecification specification);
        IPagedList<TEntity> FindPagedData(PagedDataSpecification specification);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetList();
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);

        // Async Single
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(params object[] id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        //Async Range
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    }
}
