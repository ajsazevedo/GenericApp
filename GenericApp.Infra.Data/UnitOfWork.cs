using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Domain.Models;
using GenericApp.Domain.Models.Base;
using GenericApp.Infra.CC;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericApp.Infra.Data
{
    public class UnitOfWork<TDbContext> : ApplicationManager, IUnitOfWork where TDbContext : IGenericAppContext
    {
        private bool isDisposed;
        private readonly TDbContext DbContext;
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            DbContext = (TDbContext)serviceProvider.GetRequiredService(typeof(TDbContext));
        }

        IGenericAppContext IUnitOfWork.Context { get => DbContext; }

        public void BeginTransaction()
        {
            DbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
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

            isDisposed = true;
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
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
            if (DbContext.Database.CurrentTransaction != null)
                DbContext.Database.RollbackTransaction();
            Dispose();
        }

        public void SaveChanges()
        {
            var entries = DbContext.ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).Creator = DbContext.Set<User>().Find(GetUserId());
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.Now;
                    ((BaseEntity)entityEntry.Entity).Updater = DbContext.Set<User>().Find(GetUserId());
                }
            }

            DbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
