using GenericApp.Domain.Dto.Models.Base;
using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Infra.CC.Localization.Resources;
using GenericApp.Infra.Common.Exceptions;
using GenericApp.Infra.Common.Objects;
using GenericApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Wis.Common.Objects;
using X.PagedList;

namespace GenericApp.Controllers.Base
{
    public class BaseEntityController<TEntityDto, TService> : GenericController
        where TEntityDto : EntityDto
        where TService : IBaseDbService<TEntityDto>
    {
        protected readonly TService _service;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseEntityController(TService service,
            ILogger logger,
            [FromServices] IUnitOfWork unitOfWork
            ) : base(logger)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.BeginTransaction();
            _service = service;
        }

        protected IActionResult CommitOk(object value)
        {
            return CommitOk(value, HttpStatusCode.OK);
        }

        protected IActionResult CommitOk(object value, HttpStatusCode statusCode)
        {
            _unitOfWork.Commit();
            return statusCode switch
            {
                HttpStatusCode.OK => Ok(value),
                HttpStatusCode.Created => Created("", value),
                HttpStatusCode.Accepted => Accepted(value),
                HttpStatusCode.NoContent => NoContent(),
                _ => throw new ControllerException(SharedResource.ResponseCodeNotIdentified),
            };
        }

        protected IActionResult CommitOk(HttpStatusCode statusCode)
        {
            return CommitOk(null, statusCode);
        }

        protected IActionResult CommitOk()
        {
            return CommitOk(HttpStatusCode.OK);
        }

        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public IActionResult Find([FromBody] PagedDataSpecification specification)
        {
            return Ok(Check(_service.FindPagedData(specification)));
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public virtual IActionResult GetList()
        {
            return Ok(Check(_service.GetList()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public virtual IActionResult Get([FromRoute] long id)
        {
            return Ok(Check(_service.Get(id)));
        }

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public virtual IActionResult GetPagedData([FromQuery] int pageCount, int pageSize)
        {
            return Ok(Check(_service.GetPagedData(new PagedDataSpecification { PageNumber = pageCount, PageSize = pageSize })));
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [Authorize("Collaborator")]
        public virtual IActionResult Add([FromBody] TEntityDto entity)
        {
            var result = Check(_service.Add(entity));
            result.Message = SharedResource.RecordCreated;
            return CommitOk(result, HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [Authorize("Collaborator")]
        public virtual IActionResult Delete([FromRoute] long id)
        {
            _service.Delete(_service.Get(id));
            return CommitOk(Result.Successfull(SharedResource.DeletedRecord), HttpStatusCode.OK);
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [Authorize("Collaborator")]
        public virtual IActionResult Put([FromRoute] long id, [FromBody] TEntityDto entity)
        {
            var result = Check(_service.Update(id, entity));
            result.Message = SharedResource.UpdatedRecord;
            return CommitOk(result);
        }

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [Authorize("Collaborator")]
        public virtual IActionResult Patch([FromRoute] long id, [FromBody] JsonPatchDocument<TEntityDto> entity)
        {
            var result = Check(_service.UpdateFields(id, entity));
            result.Message = SharedResource.UpdatedRecord;
            return CommitOk(result);
        }

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> AsyncFind([FromBody] Expression<Func<TEntityDto, bool>> predicate)
        {
            var result = await _service.FindAsync(predicate);
            return Ok(Check(result));
        }

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> AsyncGet()
        {
            var result = await _service.GetListAsync();
            return Ok(Check(result));
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        public async Task<IActionResult> AsyncGet([FromRoute] long id)
        {
            var result = await _service.GetAsync(id);
            return Ok(Check(result));
        }

        [HttpPost("[action]")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [Authorize("Collaborator")]
        public async Task<IActionResult> AsyncAdd([FromBody] TEntityDto entity)
        {
            var result = await _service.AddAsync(entity);
            var chkResult = Check(result);
            chkResult.Message = SharedResource.RecordCreated;
            return CommitOk(chkResult, HttpStatusCode.Created);
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [Authorize("Collaborator")]
        public async Task<IActionResult> AsyncDelete([FromRoute] long id)
        {
            var obj = await _service.GetAsync(id);
            await _service.DeleteAsync(obj);
            return CommitOk(Result.Successfull(SharedResource.DeletedRecord), HttpStatusCode.OK);
        }

        [HttpPut("[action]/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.PreconditionFailed)]
        [Authorize("Collaborator")]
        public async Task<IActionResult> AsyncPut([FromRoute] long id, [FromBody] TEntityDto entity)
        {
            var result = await _service.UpdateAsync(id, entity);
            var chkResult = Check(result);
            chkResult.Message = SharedResource.UpdatedRecord;
            return CommitOk(chkResult, HttpStatusCode.OK);
        }

        string GetFailedMessage(string failedMessage)
        {
            return string.IsNullOrEmpty(failedMessage) ? SharedResource.RecordNotFound : failedMessage;
        }

        protected Result<TEntityDto> Check(TEntityDto entity)
        {
            return Check<TEntityDto>(entity);
        }

        protected Result<T> Check<T>(T entity)
        {
            return Check(entity, SharedResource.RecordNotFound);
        }

        protected Result<TEntityDto> Check(TEntityDto entity, string failedMessage)
        {
            return Check<TEntityDto>(entity, failedMessage);
        }

        protected Result<T> Check<T>(T entity, string failedMessage)
        {
            if (entity != null)
                return Result<T>.Successfull(entity);

            return Result<T>.Failed(GetFailedMessage(failedMessage));
        }

        protected Result<IEnumerable<TEntityDto>> Check(IEnumerable<TEntityDto> entities)
        {
            return Check<TEntityDto>(entities, SharedResource.RecordNotFound);
        }

        protected Result<IEnumerable<TEntityDto>> Check(IEnumerable<TEntityDto> entities, string failedMessage)
        {
            return Check<TEntityDto>(entities, failedMessage);
        }

        protected Result<IEnumerable<T>> Check<T>(IEnumerable<T> entities)
        {
            return Check(entities, SharedResource.RecordNotFound);
        }

        protected Result<IEnumerable<T>> Check<T>(IEnumerable<T> entities, string failedMessage)
        {
            if (entities != null && entities.Any())
                return Result<IEnumerable<T>>.Successfull(entities);

            return Result<IEnumerable<T>>.Failed(GetFailedMessage(failedMessage));
        }

        protected Result<PagedData<TEntityDto>> Check(IPagedList<TEntityDto> pagedList)
        {
            return Check(pagedList, SharedResource.RecordNotFound);
        }

        protected Result<PagedData<TEntityDto>> Check(IPagedList<TEntityDto> pagedList, string failedMessage)
        {
            return Check<TEntityDto>(pagedList, failedMessage);
        }

        protected Result<PagedData<T>> Check<T>(IPagedList<T> pagedList)
        {
            return Check(pagedList, SharedResource.RecordNotFound);
        }

        protected Result<PagedData<T>> Check<T>(IPagedList<T> pagedList, string failedMessage)
        {
            if (pagedList != null && pagedList.Any())
                return Result<PagedData<T>>.Successfull(new PagedData<T>(pagedList, pagedList.GetMetaData()));

            return Result<PagedData<T>>.Failed(GetFailedMessage(failedMessage));
        }
    }
}
