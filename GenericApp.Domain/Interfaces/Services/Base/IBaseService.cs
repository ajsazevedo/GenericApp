using System.Security.Claims;

namespace GenericApp.Domain.Interfaces.Services.Base
{
    public interface IBaseService
    {
        TService GetService<TService>() where TService : class;
        ClaimsPrincipal GetUser();
        string GetUserName();
    }
}
