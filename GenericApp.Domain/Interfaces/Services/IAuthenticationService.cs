using FluentValidation;
using GenericApp.Domain.Dto.Request;
using GenericApp.Domain.Dto.Return;
using GenericApp.Domain.Interfaces.Services.Base;
using Wis.Common.Objects;

namespace GenericApp.Domain.Interfaces.Services
{
    public interface IAuthenticationService : IBaseService
    {
        AuthenticationResultDto Authenticate<Validator>(CredencialsDto userLogin) where Validator : AbstractValidator<CredencialsDto>;
        Result ChangePassword(ChangeCredencialsDto credencials);
        Result ResetPassword(string username);
    }
}
