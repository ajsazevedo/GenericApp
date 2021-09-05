using AutoMapper;
using GenericApp.Infra.CC.Localization.Interfaces;
using Microsoft.Extensions.Localization;
using Serilog;
using System.Security.Claims;

namespace GenericApp.Infra.CC.Interfaces
{
    public interface IApplicationManager
    {
        IStringLocalizer<ISharedResource> Localizer { get; }
        IMapper Mapper { get; }
        TService GetService<TService>() where TService : class;
        ILogger Logger();
        ClaimsPrincipal GetUser();
        string GetUserName();
        string GetUserLogin();
        long GetUserId();
        string GetAccessToken();
        string GetApplicationVersion();
    }
}
