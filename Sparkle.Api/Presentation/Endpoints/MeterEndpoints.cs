using MediatR;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Endpoints
{
    public static class MeterEndpoints
    {
        public static void MapMeterEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/meter/get/list", async (IMediator mediator, [AsParameters] GetMeterListQuery query) =>
            {
                var result = await mediator.Send(query);

                return result?.Items is null
                    ? Results.NotFound()
                    : Results.Ok(result);

            }).AddEndpointFilter<ValidationFilter<GetMeterListQuery>>();
        }
    }
}
