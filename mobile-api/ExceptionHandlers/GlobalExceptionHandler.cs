using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mobile_api.Responses;
using System.ComponentModel.DataAnnotations;


//using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace mobile_api.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError("Error Message: {message}, Time of occurrence {time}", exception.Message, DateTime.UtcNow);
            (string Detail, string Tittle, int StatusCode) =
            (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            );
            var problemDetails = new ProblemDetails
            {
                Title = Tittle,
                Detail = Detail,
                Status = StatusCode,
                Instance = context.TraceIdentifier,
            };
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("errors", validationException.Value);
            }
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
