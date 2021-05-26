using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GenericApp.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : ControllerBase
    {
        protected readonly ILogger<GenericController> _logger;
        protected readonly IMapper _mapper;

        public GenericController(ILogger<GenericController> logger, [FromServices] IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        protected T Map<T>(object obj) => _mapper.Map<T>(obj);
    }
}
