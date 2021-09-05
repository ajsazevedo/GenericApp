using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Dto.Request;
using GenericApp.Domain.Interfaces.Services.Base;
using Wis.Common.Objects;

namespace GenericApp.Domain.Interfaces.Services.Entity
{
    public interface IUserService : IBaseDbService<UserDto>
    {
        UserDto FindByLogin(CredencialsDto userLogin);
        Result SetDefaultPassword(string username);
        Result ChangePassword(ChangeCredencialsDto credencials);
        UserDto FindById(long id);
    }
}
