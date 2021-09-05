using GenericApp.Controllers.Base;
using GenericApp.Filters;
using GenericApp.Infra.CC.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Wis.Common.Objects;

namespace GenericApp.Controllers
{
    public class ApplicationController : GenericController
    {
        private readonly IApplicationManager _applicationManager;

        public ApplicationController(IApplicationManager applicationManager, ILogger logger) : base(logger)
        {
            _applicationManager = applicationManager;
        }

        [AuthorizationFilter]
        [HttpGet("[action]")]
        public IActionResult GetCurrentUser()
        {
            return Ok(Result.Successfull(_applicationManager.GetUserName()));
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetCurrentVersion()
        {
            return Ok(Result.Successfull(_applicationManager.GetApplicationVersion()));
        }
    }
}
