using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericApp.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity> : IRepository
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(params object[] id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void DeleteRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
