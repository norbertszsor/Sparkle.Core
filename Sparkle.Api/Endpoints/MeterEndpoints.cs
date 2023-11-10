using MediatR;
using Sparkle.Api.Filters;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Endpoints
{
    public static class MeterEndpoints
    {
        public static void MapMeterEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/meter/get/list", async (IMediator mediator, [AsParameters] GetMeterListQuery query) =>
                {
                    var result = await mediator.Send(query);

                    return result?.Items == null ? Results.NotFound() : Results.Ok(result);
                })
                .AddEndpointFilter<ValidationFilter<GetMeterListQuery>>()
                .CacheOutput();
        }
    }
}
