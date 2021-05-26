using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApp.Application.Interfaces.Services.Base
{
    public interface IBaseDataService<TEntity> : IBaseService where TEntity : class
    {
        TEntity Get(long pk);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity obj);
        void Delete(TEntity obj);
        TEntity Add(TEntity obj);
    }
}
