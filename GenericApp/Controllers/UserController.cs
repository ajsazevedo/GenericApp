using GenericApp.Controllers.Base;
using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Interfaces.Services.Entity;
using GenericApp.Infra.Data.Interfaces;
using Serilog;

namespace GenericApp.Controllers
{
    public class UserController : BaseEntityController<UserDto, IUserService>
    {
        public UserController(IUserService service, ILogger logger, IUnitOfWork unitOfWork) : base(service, logger, unitOfWork)
        {
        }
    }
}
