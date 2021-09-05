using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GenericApp.Controllers.Base
{
    [Authorize, ApiController, Route("api/labi/[controller]")]
    public class GenericController : ControllerBase
    {
        protected readonly ILogger _logger;

        public GenericController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
