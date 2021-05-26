using System.Collections.Generic;

namespace GenericApp.Application.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
