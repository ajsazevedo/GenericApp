using FluentValidation;
using GenericApp.Domain.Dto.Request;
using GenericApp.Domain.Dto.Return;
using GenericApp.Domain.Interfaces.Services.Base;
using Wis.Common.Objects;

namespace GenericApp.Domain.Interfaces.Services
{
    public interface IAuthenticationService : IBaseService
    {
        AuthenticationResultDto Authenticate<Validator>(CredentialsDto credentials) where Validator : AbstractValidator<CredentialsDto>;
        Result ChangePassword(ChangeCredentialsDto credentials);
        Result ResetPassword(string username);
    }
}
