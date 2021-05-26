using GenericApp.Infra.CC.Exceptions.Extensions;
using GenericApp.Infra.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
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
                case ServiceException s:
                    response.StatusCode = (int)HttpStatusCode.OK;
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

                ////non handled exception
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(Result.Failed($"Ocorreu uma falha inesperada.<br>Entre em contato com a administração do sistema."));
                    break;
            }

            await response.WriteAsync(result);
        }
    }
}
