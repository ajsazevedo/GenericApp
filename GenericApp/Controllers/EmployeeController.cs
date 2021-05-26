using AutoMapper;
using GenericApp.Controllers.Base;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Domain.Models;
using GenericApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GenericApp.Controllers
{
    public class EmployeeController : BaseEntityController<Employee, EmployeeVM>
    {
        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger,
            IMapper mapper) : base(service, logger, mapper)
        {
        }
    }
}
