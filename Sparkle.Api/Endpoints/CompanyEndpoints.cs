using MediatR;
using Sparkle.Api.Filters;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Endpoints
{
    public static class CompanyEndpoints
    {
        public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/company/get", async (IMediator mediator, [AsParameters] GetCompanyQuery query) =>
                {
                    var result = await mediator.Send(query);

                    return result is null ? Results.NotFound() : Results.Ok(result);
                })
                .AddEndpointFilter<ValidationFilter<GetCompanyQuery>>()
                .CacheOutput();
        }
    }
}
