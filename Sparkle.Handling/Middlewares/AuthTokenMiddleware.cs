using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sparkle.Domain.Data;
using Sparkle.Shared.Helpers;

namespace Sparkle.Handling.Middlewares
{
    public class AuthTokenMiddleware : IMiddleware
    {
        private readonly ILogger<AuthTokenMiddleware> _logger;
        private readonly SparkleContext _storage;

        public AuthTokenMiddleware(ILogger<AuthTokenMiddleware> logger, SparkleContext storage)
        {
            _logger = logger;
            _storage = storage;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.TryGetValue("ApiToken", out var headerApiToken))
            {
                _logger.LogWarning("Missing API token");

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync("Missing API token");

                return;
            }

            var hashedToken = await _storage.ApiTokens
                .Select(x => x.TokenHash)
                .FirstOrDefaultAsync();

            if (TokenHelper.VerifyToken(headerApiToken, hashedToken))
            {
                await next(context);
            }
            else
            {
                _logger.LogWarning("Invalid API token");

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync("Invalid API token");

                return;
            }
        }
    }
}
