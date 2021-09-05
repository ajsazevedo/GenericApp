using GenericApp.Domain.Dto.Models.Base;
using GenericApp.Infra.Common.Objects;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace GenericApp.Domain.Interfaces.Services.Base
{
    public interface IBaseDbService<TEntityDto> : IBaseService where TEntityDto : EntityDto
    {
        // Common Single
        TEntityDto FindOne(Expression<Func<TEntityDto, bool>> predicate);
        TEntityDto Get(params object[] id);
        TEntityDto Add(TEntityDto obj);
        TEntityDto Update(long id, TEntityDto obj);
        TEntityDto UpdateFields(long id, JsonPatchDocument<TEntityDto> obj);
        void Delete(TEntityDto obj);

        // Common Range
        IEnumerable<TEntityDto> Find(Expression<Func<TEntityDto, bool>> predicate);
        IPagedList<TEntityDto> FindPagedData(PagedDataSpecification specification);
        IEnumerable<TEntityDto> GetList();
        IPagedList<TEntityDto> GetPagedData(PagedDataSpecification specification);
        IEnumerable<TEntityDto> AddRange(IEnumerable<TEntityDto> objs);
        IEnumerable<TEntityDto> UpdateRange(IEnumerable<TEntityDto> objs);
        void DeleteRange(IEnumerable<TEntityDto> objs);

        // Async Single
        Task<TEntityDto> FindOneAsync(Expression<Func<TEntityDto, bool>> predicate);
        Task<TEntityDto> GetAsync(params object[] id);
        Task<TEntityDto> AddAsync(TEntityDto obj);
        Task<TEntityDto> UpdateAsync(long id, TEntityDto obj);
        Task DeleteAsync(TEntityDto obj);

        //Async Range
        Task<IEnumerable<TEntityDto>> FindAsync(Expression<Func<TEntityDto, bool>> predicate);
        Task<IEnumerable<TEntityDto>> GetListAsync();
        Task<IEnumerable<TEntityDto>> AddRangeAsync(IEnumerable<TEntityDto> objs);
        Task<IEnumerable<TEntityDto>> UpdateRangeAsync(IEnumerable<TEntityDto> objs);
        Task DeleteRangeAsync(IEnumerable<TEntityDto> objs);
    }
}
