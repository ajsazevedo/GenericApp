using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Domain.Models.Base;
using GenericApp.Infra.Common.Extensions;
using GenericApp.Infra.Common.Objects;
using GenericApp.Infra.Data.Extensions;
using GenericApp.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace GenericApp.Infra.Data.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly IGenericAppContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = _unitOfWork.Context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual TEntity Get(params object[] id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _unitOfWork.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _unitOfWork.SaveChanges();
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _unitOfWork.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public virtual IEnumerable<TEntity> GetList()
        {
            return GetQuery();
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetSortedQuery(string sortBy, bool descending)
        {
            return GetQuery().OrderBy(sortBy, descending);
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            _unitOfWork.SaveChanges();
            return entities;
        }

        public virtual IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            _unitOfWork.SaveChanges();
            return entities;
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            _unitOfWork.SaveChanges();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> GetAsync(params object[] id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _unitOfWork.SaveChangesAsync();
            return entities;
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public IPagedList<TEntity> GetPagedData(PagedDataSpecification specification)
        {
            return new PagedList<TEntity>(GetSortedQuery(specification.SortBy, specification.Descending), specification.PageNumber, specification.PageSize);
        }

        public virtual IPagedList<TEntity> FindPagedData(PagedDataSpecification specification)
        {
            return new PagedList<TEntity>(FindSpecificList(specification), specification.PageNumber, specification.PageSize);
        }

        public virtual IQueryable<TEntity> FindSpecificList(PagedDataSpecification specification)
        {
            return GetSortedQuery(specification.SortBy, specification.Descending).ContainValues(specification.Filters);
        }

        public IQueryable<TEntity> WhereContains(string property, string argument)
        {
            return from c in _dbSet
                   where EF.Functions.Like(property, argument)
                   select c;
        }

        public IQueryable<TEntity> Filter(Dictionary<string, object> pairs, IQueryable<TEntity> query)
        {
            foreach (var pair in pairs)
                query.Concat(WhereContains(pair.Key.ToTitleCase(), pair.Value.ToString()));

            return query;
        }
    }
}
