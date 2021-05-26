using AutoMapper;
using GenericApp.Controllers.Base;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Domain.Models;
using GenericApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GenericApp.Controllers
{
    public class CompanyController : BaseEntityController<Company, CompanyVM>
    {
        public CompanyController(ICompanyService service, ILogger<CompanyController> logger, IMapper mapper) 
            : base(service, logger, mapper)
        {
        }
    }
}
