using GenericApp.Domain.Dto.Models.Base;
using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Domain.Models.Base;
using GenericApp.Infra.CC.Localization.Resources;
using GenericApp.Infra.CC.Mapping.Extensions;
using GenericApp.Infra.Common.Exceptions;
using GenericApp.Infra.Common.Objects;
using GenericApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace GenericApp.Application.Services.Base
{
    //TODO Make generic Key type
    public class BaseDbService<TEntityDto, TEntity> : BaseService, IBaseDbService<TEntityDto>
        where TEntityDto : EntityDto
        where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseDbService(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TEntity>();
        }

        public TEntityDto FindOne(Expression<Func<TEntityDto, bool>> predicate)
        {
            return ToDto(_repository.Find(ToEntityExpression(predicate)).FirstOrDefault());
        }

        public virtual TEntityDto Get(params object[] id)
        {
            return ToDto(_repository.Get(id));
        }

        public virtual TEntityDto Add(TEntityDto obj)
        {
            return ToDto(_repository.Add(RemoveVirtualProperties(obj)));
        }

        public virtual TEntityDto Update(long id, TEntityDto obj)
        {
            var o = FindOne(x => x.Id == id);
            if (o == null)
                throw new ServiceException(SharedResource.ObjectToBeUpdatedDoesNotExists);
            obj.Id = o.Id;
            o = obj;
            return ToDto(_repository.Update(RemoveVirtualProperties(o)));
        }

        public virtual TEntityDto UpdateFields(long id, JsonPatchDocument<TEntityDto> obj)
        {
            var o = FindOne(x => x.Id == id);
            if (o == null)
                throw new ServiceException(SharedResource.ObjectToBeUpdatedDoesNotExists);
            obj.ApplyTo(o);
            return ToDto(_repository.Update(RemoveVirtualProperties(o)));
        }

        public virtual void Delete(TEntityDto obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), SharedResource.ObjectToBeRemovedDoesNotExists);
            _repository.Delete(RemoveVirtualProperties(obj));
        }

        public IEnumerable<TEntityDto> Find(Expression<Func<TEntityDto, bool>> predicate)
        {
            return _repository.Find(ToEntityExpression(predicate)).ToList().ConvertAll(m => ToDto(m));
        }

        public virtual IEnumerable<TEntityDto> GetList()
        {
            return _repository.GetList().ToList().ConvertAll(m => ToDto(m));
        }

        public virtual IEnumerable<TEntityDto> AddRange(IEnumerable<TEntityDto> objs)
        {
            return _repository.AddRange(objs.ToList().ConvertAll(x => RemoveVirtualProperties(x))).ToList().ConvertAll(m => ToDto(m));
        }

        public virtual IEnumerable<TEntityDto> UpdateRange(IEnumerable<TEntityDto> objs)
        {
            return _repository.UpdateRange(objs.ToList().ConvertAll(x => RemoveVirtualProperties(x))).ToList().ConvertAll(m => ToDto(m));
        }

        public virtual void DeleteRange(IEnumerable<TEntityDto> objs)
        {
            _repository.DeleteRange(objs.ToList().ConvertAll(m => RemoveVirtualProperties(m)));
        }

        public async Task<TEntityDto> FindOneAsync(Expression<Func<TEntityDto, bool>> predicate)
        {
            return await _repository.FindOneAsync(ToEntityExpression(predicate))
                .ContinueWith(x => ToDto(x.Result));
        }

        public async Task<TEntityDto> GetAsync(params object[] id)
        {
            return await _repository.GetAsync(id).ContinueWith(x => ToDto(x.Result));
        }

        public async Task<TEntityDto> AddAsync(TEntityDto obj)
        {
            return await _repository.AddAsync(RemoveVirtualProperties(obj))
                .ContinueWith(x => ToDto(x.Result));
        }

        public async Task<TEntityDto> UpdateAsync(long id, TEntityDto obj)
        {
            var o = await GetAsync(id);
            if (o == null)
                throw new ServiceException(SharedResource.ObjectToBeUpdatedDoesNotExists);
            obj.Id = o.Id;
            o = obj;
            return await _repository.UpdateAsync(RemoveVirtualProperties(o))
                .ContinueWith(x => ToDto(x.Result));
        }

        public async Task DeleteAsync(TEntityDto obj)
        {
            await _repository.DeleteAsync(ToEntity(obj));
        }

        public async Task<IEnumerable<TEntityDto>> FindAsync(Expression<Func<TEntityDto, bool>> predicate)
        {
            return await _repository.FindAsync(ToEntityExpression(predicate))
                .ContinueWith(x => x.Result.ToList().ConvertAll(m => ToDto(m)));
        }

        public async Task<IEnumerable<TEntityDto>> GetListAsync()
        {
            return await _repository.GetListAsync()
                .ContinueWith(x => x.Result.ToList().ConvertAll(m => ToDto(m)));
        }

        public async Task<IEnumerable<TEntityDto>> AddRangeAsync(IEnumerable<TEntityDto> objs)
        {
            return await _repository.AddRangeAsync(objs.ToList().ConvertAll(x => RemoveVirtualProperties(x)))
                .ContinueWith(x => x.Result.ToList().ConvertAll(m => ToDto(m)));
        }

        public async Task<IEnumerable<TEntityDto>> UpdateRangeAsync(IEnumerable<TEntityDto> objs)
        {
            return await _repository.UpdateRangeAsync(objs.ToList().ConvertAll(x => RemoveVirtualProperties(x)))
                .ContinueWith(x => x.Result.ToList().ConvertAll(m => ToDto(m)));
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntityDto> objs)
        {
            await _repository.AddRangeAsync(objs.ToList().ConvertAll(x => RemoveVirtualProperties(x)));
        }

        protected TEntity RemoveVirtualProperties(TEntityDto obj)
        {
            return RemoveVirtualProperties(ToEntity(obj));
        }

        private TEntity RemoveVirtualProperties(TEntity obj)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.GetAccessors().Any(a => a.IsVirtual) &&
                    propertyInfo.Name != "Id")
                    propertyInfo.SetValue(obj, null);
            }

            return obj;
        }

        protected TEntityDto ToDto(TEntity e)
        {
            return Map<TEntityDto>(e);
        }

        protected TEntity ToEntity(TEntityDto vm)
        {
            if (vm != null)
                return Map<TEntity>(vm);
            throw new ArgumentNullException(nameof(vm), SharedResource.CannotMapNullDto);
        }

        protected Expression<Func<TEntity, bool>> ToEntityExpression(Expression<Func<TEntityDto, bool>> expression)
            => Map<Expression<Func<TEntity, bool>>>(expression);

        public IPagedList<TEntityDto> FindPagedData(PagedDataSpecification specification)
        {
            return _repository.FindPagedData(specification).ToMappedPagedList<TEntity, TEntityDto>(_mapper);
        }

        public IPagedList<TEntityDto> GetPagedData(PagedDataSpecification specification)
        {
            return _repository.GetPagedData(specification).ToMappedPagedList<TEntity, TEntityDto>(_mapper);
        }
    }
}
