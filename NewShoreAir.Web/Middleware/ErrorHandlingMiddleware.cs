using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NewShoreAir.Domain.Excepciones;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NewShoreAir.Web.Excepciones
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private static  ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unhandled Exception");

            HttpStatusCode status;
            string message = String.Empty;

            var exceptionType = exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(UnauthorizedException))
            {
                message = exception.Message;
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(ForbiddenException))
            {
                message = exception.Message;
                status = HttpStatusCode.Forbidden;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(ConflictException))
            {
                message = exception.Message;
                status = HttpStatusCode.Conflict;
            }
            else if (exceptionType == typeof(NoContentException))
            {
                status = HttpStatusCode.NoContent;
                message = String.Empty;
            }
            else if (exceptionType == typeof(NullReferenceException))
            {
                message = "Ha ocurrido un error en el sistema.";
                status = HttpStatusCode.BadRequest;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
            }

            var result = JsonConvert.SerializeObject(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(result);
        }
    }
}
