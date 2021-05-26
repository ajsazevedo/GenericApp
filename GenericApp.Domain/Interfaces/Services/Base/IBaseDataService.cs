using GenericApp.Domain.Models.Base;
using System.Collections.Generic;

namespace GenericApp.Domain.Interfaces.Services.Base
{
    public interface IBaseDataService<TEntity> : IBaseService where TEntity : IEntity<long>
    {
        TEntity Get(long pk);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity obj);
        void Delete(TEntity obj);
        TEntity Add(TEntity obj);
    }
}
