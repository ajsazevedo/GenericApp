using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Domain.Models.Base;
using GenericApp.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericApp.Services.Base
{
    //TODO Make generic Key type
    public class BaseDbService<TEntity> : BaseService, IBaseDataService<TEntity> where TEntity : BaseEntity<long>
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseDbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<TEntity>();
        }

        public virtual TEntity Add(TEntity obj)
        {
            var o = _repository.Add(RemoveVirtualProperties(obj));
            return o;
        }

        public virtual void Delete(TEntity obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Objeto a ser removido não existe");
            _repository.Delete(obj);
        }

        public virtual TEntity Get(long pk)
        {
            return _repository.Get(pk);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual TEntity Update(TEntity obj)
        {
            var o = _repository.Get(obj.Id);
            if (o == null)
                throw new Exception("Objeto a ser atualizado nao existe.");
            o = obj;
            var result = _repository.Update(RemoveVirtualProperties(o));
            return result;
        }

        public TEntity RemoveVirtualProperties(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.GetAccessors().Any(a => a.IsVirtual) &&
                    propertyInfo.Name != "Id")
                    propertyInfo.SetValue(entity, null);
            }

            return entity;
        }
    }
}
