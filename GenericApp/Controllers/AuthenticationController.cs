using GenericApp.Application.Validators;
using GenericApp.Controllers.Base;
using GenericApp.Domain.Dto.Request;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using Wis.Common.Objects;

namespace GenericApp.Controllers
{
    public class AuthenticationController : GenericController
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service,
            ILogger logger)
            : base(logger)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Authenticate([FromBody] CredentialsDto credentials)
        {
            return Ok(_service.Authenticate<LoginValidator>(credentials));
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult ChangePassword([FromBody] ChangeCredentialsDto credencials)
        {
            return Ok(_service.ChangePassword(credencials));
        }

        [AuthorizationFilter]
        [HttpGet("[action]")]
        public IActionResult CheckToken()
        {
            return Ok(Result.Successfull());
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult ResetPassword([FromQuery] string username)
        {
            return Ok(_service.ResetPassword(username));
        }
    }
}
