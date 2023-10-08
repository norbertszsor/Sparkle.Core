using Sparkle.Api.Endpoints;

namespace Sparkle.Api.Extensions
{
    public static class EndpointExtension
    {
        public static void AddEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapRegressorEndpoints();

            app.MapCompanyEndpoints();

            app.MapMeterEndpoints();
        }
    }
}
