using AutoMapper;
using GenericApp.Infra.CC.Interfaces;
using GenericApp.Infra.CC.Localization.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using Serilog;
using System;
using System.Reflection;
using System.Security.Claims;

namespace GenericApp.Infra.CC
{
    public class ApplicationManager : IApplicationManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IStringLocalizer<ISharedResource> _localizer;

        public ApplicationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _localizer = serviceProvider.GetRequiredService<IStringLocalizer<ISharedResource>>();
        }

        public IMapper Mapper => _mapper;

        public IStringLocalizer<ISharedResource> Localizer => _localizer;

        public TService GetService<TService>() where TService : class
        {
            return _serviceProvider.GetService<TService>();
        }

        public string GetUserName()
        {
            return _accessor.HttpContext.User.FindFirst(ClaimTypes.GivenName)?.Value;
        }

        public string GetUserLogin()
        {
            return _accessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        public long GetUserId()
        {
            return Convert.ToInt64(_accessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
        }

        public ClaimsPrincipal GetUser()
        {
            return _accessor.HttpContext.User;
        }

        public ILogger Logger()
        {
            return GetService<ILogger>();
        }

        public string GetAccessToken()
        {
            return _accessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
        }

        public string GetApplicationVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
