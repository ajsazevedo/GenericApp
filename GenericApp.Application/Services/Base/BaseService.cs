using AutoMapper;
using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Infra.CC.Interfaces;
using Serilog;
using System.Security.Claims;

namespace GenericApp.Application.Services.Base
{
    public class BaseService : IBaseService
    {
        protected readonly IMapper _mapper;
        private readonly IApplicationManager _applicationManager;

        public BaseService(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
            _mapper = _applicationManager.Mapper;
        }

        public TService GetService<TService>() where TService : class
        {
            return _applicationManager.GetService<TService>();
        }

        public ClaimsPrincipal GetUser()
        {
            return _applicationManager.GetUser();
        }

        public string GetUserName()
        {
            return _applicationManager.GetUserName();
        }

        protected T Map<T>(object obj) => _mapper.Map<T>(obj);

        protected ILogger Logger() => _applicationManager.Logger();
    }
}
