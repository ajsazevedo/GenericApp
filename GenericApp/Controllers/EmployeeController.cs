using GenericApp.Controllers.Base;
using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Interfaces.Services.Entity;
using GenericApp.Infra.Data.Interfaces;
using Serilog;

namespace GenericApp.Controllers
{
    public class EmployeeController : BaseEntityController<EmployeeDto, IEmployeeService>
    {
        public EmployeeController(IEmployeeService service, ILogger logger, IUnitOfWork unitOfWork) : base(service, logger, unitOfWork)
        {
        }
    }
}
