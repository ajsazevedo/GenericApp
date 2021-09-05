using GenericApp.Application.Services;
using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Dto.Return;
using GenericApp.Infra.CC.Localization.Resources;
using GenericApp.Infra.Common.Enums;
using GenericApp.Infra.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace GenericApp.Filters
{
    public class AuthorizationFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // pegando os dados do usuário autenticado
            var t = (ClaimsIdentity)context.HttpContext.User.Identity;

            // coleta os dados do JWT
            var user = new UserDto
            {
                Id = long.Parse(t.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault()),
                Name = t.Claims.Where(c => c.Type == ClaimTypes.GivenName).Select(c => c.Value).SingleOrDefault(),
                Email = t.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault(),
                Role = t.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault().ToEnum<Role>(),
            };

            if (user.Email != null)
            {
                if (!context.HttpContext.Response.Headers.Keys.Any(x => x == "Authorization"))
                {
                    var _tokenManagerService = new TokenManagerService();
                    //retorna no header da resposta o novo token
                    var token = _tokenManagerService.GenerateToken(user);
                    var authentication = new AuthenticationResultDto()
                    {
                        Success = true,
                        Expiration = _tokenManagerService.GetExpireDate(token).ToString("yyyy-MM-dd HH:mm:ss"),
                        Token = token,
                        Roles = user.Role,
                        ExpiredPassword = false
                    };
                    context.HttpContext.Response.Headers.Add("Authorization", authentication.Serialize());
                }
            }
            else
            {
                context.Result = new JsonResult(new { message = SharedResource.UnauthorizedAccess }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
