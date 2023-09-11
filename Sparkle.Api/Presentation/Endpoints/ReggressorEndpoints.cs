using MediatR;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Endpoints
{
    public static class ReggressorEndpoints
    {
        public static void MapReggressorEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/reggressor/get/prediction", async (IMediator mediator, [AsParameters] GetPredictionQuery query) =>
            {
                var result = await mediator.Send(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);

            }).AddEndpointFilter<ValidationFilter<GetPredictionQuery>>();

            app.MapGet("/api/reggressor/get/comparison", async (IMediator mediator, [AsParameters] GetComparisonQuery query) =>
            {
                var result = await mediator.Send(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);

            }).AddEndpointFilter<ValidationFilter<GetComparisonQuery>>();

        }
    }
}
