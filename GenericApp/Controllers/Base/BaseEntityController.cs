using AutoMapper;
using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Domain.Models.Base;
using GenericApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Wis.Common.Objects;

namespace GenericApp.Controllers.Base
{
    public class BaseEntityController<TEntity, TEntityVM> : GenericController where TEntity : IEntity<long>
    {
        protected readonly IBaseDataService<TEntity> _service;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseEntityController(IBaseDataService<TEntity> service, ILogger<BaseEntityController<TEntity, TEntityVM>> logger, [FromServices] IUnitOfWork unitOfWork,
            [FromServices] IMapper mapper) : base(logger, mapper)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.BeginTransaction();
            _service = service;
        }

        protected IActionResult CommitOk(object value)
        {
            _unitOfWork.Commit();
            return Ok(value);
        }

        protected IActionResult CommitOk()
        {
            _unitOfWork.Commit();
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            return Ok(Result<IEnumerable<TEntityVM>>.Successfull(_service.GetAll().ToList().ConvertAll(x => ToModel(x))));
        }

        [HttpGet("[action]")]
        public IActionResult GetById(long id)
        {
            return Ok(Result<TEntityVM>.Successfull(ToModel(_service.Get(id))));
        }

        [HttpPost("[action]")]
        public IActionResult Add(TEntityVM entity)
        {
            return CommitOk(Result<TEntityVM>.Successfull(ToModel(_service.Add(ToEntity(entity)))));
        }

        [HttpDelete("[action]")]
        public IActionResult Delete(long id)
        {
            _service.Delete(_service.Get(id));
            return CommitOk();
        }

        [HttpPost("[action]")]
        public IActionResult Update(TEntityVM entity)
        {
            return CommitOk(Result<TEntityVM>.Successfull(ToModel(_service.Update(ToEntity(entity)))));
        }

        protected TEntity ToEntity(TEntityVM vm)
        {
            return Map<TEntity>(vm);
        }

        protected TEntityVM ToModel(TEntity e)
        {
            return Map<TEntityVM>(e);
        }
    }
}
