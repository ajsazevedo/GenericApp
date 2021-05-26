using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Infra.Data.Context;
using GenericApp.Infra.Data.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;


namespace GenericApp.Infra.Data
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : GenericAppContext
    {
        private bool isDisposed;
        private readonly TDbContext DbContext;
        private readonly IServiceProvider ServiceProvider;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            DbContext = (TDbContext)serviceProvider.GetRequiredService(typeof(TDbContext));
            ServiceProvider = serviceProvider;
        }

        GenericAppContext IUnitOfWork.Context { get => DbContext; }

        public void BeginTransaction()
        {
            DbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            SaveChanges();
            DbContext.Database.CommitTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                DbContext.Dispose();
            }

            //// free native resources if there are any.
            //if (nativeResource != IntPtr.Zero)
            //{
            //    Marshal.FreeHGlobal(nativeResource);
            //    nativeResource = IntPtr.Zero;
            //}

            isDisposed = true;
        }

        public TService GetService<TService>() where TService : class
        {
            return ServiceProvider.GetRequiredService<TService>();
        }

        public IBaseRepository<TEntity> Repository<TEntity>()
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>();
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
                return (IBaseRepository<TEntity>)_repositories[type];

            var repositoryType = typeof(BaseRepository<>);
            _repositories.Add(type, Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)), this)
            );
            return _repositories[type];
        }

        public void Rollback()
        {
            DbContext.Database.RollbackTransaction();
            Dispose();
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
