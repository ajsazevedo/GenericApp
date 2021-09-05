using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Domain.Models.Base;
using GenericApp.Infra.CC.Interfaces;
using System;
using System.Threading.Tasks;

namespace GenericApp.Infra.Data.Interfaces
{
    public interface IUnitOfWork : IApplicationManager, IDisposable
    {
        public IGenericAppContext Context { get; }
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        void BeginTransaction();
        void SaveChanges();
        Task SaveChangesAsync();
        void Commit();
        void Rollback();        
    }
}
