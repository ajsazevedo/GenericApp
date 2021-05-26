using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Infra.Data.Context;
using System;

namespace GenericApp.Infra.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public GenericAppContext Context { get; }
        TService GetService<TService>() where TService : class;
        IBaseRepository<TEntity> Repository<TEntity>();
        void BeginTransaction();
        void SaveChanges();
        void Commit();
        void Rollback();
    }
}
