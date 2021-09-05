using GenericApp.Controllers.Base;
using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Interfaces.Services.Entity;
using GenericApp.Infra.Data.Interfaces;
using Serilog;

namespace GenericApp.Controllers
{
    public class CompanyController : BaseEntityController<CompanyDto, ICompanyService>
    {
        public CompanyController(ICompanyService service, ILogger logger, IUnitOfWork unitOfWork) 
            : base(service, logger, unitOfWork)
        {
        }
    }
}
