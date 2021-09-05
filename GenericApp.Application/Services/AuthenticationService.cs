using FluentValidation;
using GenericApp.Application.Services.Base;
using GenericApp.Domain.Dto.Request;
using GenericApp.Domain.Dto.Return;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Domain.Interfaces.Services.Entity;
using GenericApp.Infra.CC.Interfaces;
using GenericApp.Infra.CC.Localization.Resources;
using Wis.Common.Objects;

namespace GenericApp.Application.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ITokenManagerService _tokenManagerService;
        private readonly IUserService _userService;

        public AuthenticationService(
            IUserService userService,
            ITokenManagerService tokenManagerService,
            IApplicationManager applicationManager) : base(applicationManager)
        {
            _tokenManagerService = tokenManagerService;
            _userService = userService;
        }

        public AuthenticationResultDto Authenticate<Validator>(CredencialsDto userLogin) where Validator : AbstractValidator<CredencialsDto>
        {
            var user = _userService.FindByLogin(userLogin);
            if (user.IsPasswordValid())
            {
                var token = _tokenManagerService.GenerateToken(_tokenManagerService.CreateIdentity(user));

                return new AuthenticationResultDto()
                {
                    Success = true,
                    Expiration = _tokenManagerService.GetExpireDate(token).ToString("yyyy-MM-dd HH:mm:ss"),
                    Token = token,
                    Message = SharedResource.SuccessfullyGeneratedToken,
                    Roles = user.Role,
                    ExpiredPassword = false
                };
            }
            return new AuthenticationResultDto()
            {
                Success = false,
                Message = SharedResource.ExpiredPassword,
                Roles = user.Role,
                ExpiredPassword = true
            };
        }

        public Result ChangePassword(ChangeCredencialsDto credencials)
        {
            return _userService.ChangePassword(credencials);
        }

        public Result ResetPassword(string username)
        {
            return _userService.SetDefaultPassword(username);
        }
    }
}
