using GenericApp.Infra.CC.Exceptions.Extensions;
using GenericApp.Infra.CC.Localization.Resources;
using GenericApp.Infra.Common.Exceptions;
using GenericApp.Infra.Data.Extensions;
using GenericApp.Infra.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Wis.Common.Objects;

namespace GenericApp.Infra.CC.Exceptions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IUnitOfWork unitOfWork)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                ex.LogException();
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            string result;           

            switch (error)
            {
                case ValidationException v:
                    response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    result = JsonSerializer.Serialize(Result.Failed(v.Message));
                    break;

                case FluentValidation.ValidationException f:
                    response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    result = JsonSerializer.Serialize(Result.Failed(string.Join("\n", f.Errors)));
                    break;

                case ServiceException s:
                    response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    result = JsonSerializer.Serialize(Result.Failed(s.FriendlyMessage));
                    break;

                case BadRequestException b:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(Result.Failed(b.FriendlyMessage));
                    break;

                case GlobalException g:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(Result.Failed(g.Message));
                    break;

                case MailException m:
                    response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    result = JsonSerializer.Serialize(Result.Failed(m.FriendlyMessage));
                    break;

                case SecurityTokenValidationException f:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    result = JsonSerializer.Serialize(Result.Failed(f.Message));
                    break;

                case SqlException d:
                    response.StatusCode = d.GetSqlErrorCode();
                    result = JsonSerializer.Serialize(Result.Failed(d.GetSqlErrorMessage()));
                    break;

                case ArgumentOutOfRangeException _:
                case InvalidOperationException _:
                case DbUpdateException _:
                case ArgumentException _:
                    response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                    result = JsonSerializer.Serialize(SharedResource.RequestCouldNotBeProcessed);
                    break;

                ////non handled exception
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(Result.Failed(SharedResource.UnexpectedFailureOccurred));
                    break;
            }

            await response.WriteAsync(result);
        }
    }
}
