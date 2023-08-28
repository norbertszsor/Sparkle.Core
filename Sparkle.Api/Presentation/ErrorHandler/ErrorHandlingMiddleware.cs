using Sparkle.Api.Shared;
using System.Net;
using System.Text.Json;

namespace Sparkle.Api.Presentation.ErrorHandler
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(SparkleException ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = ex is SparkleException
                ? (int)HttpStatusCode.BadRequest
                : (int)HttpStatusCode.InternalServerError;

            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new { message = ex.Message });

            return response.WriteAsync(result);
        }
    }
}
