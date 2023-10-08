using MediatR;
using Sparkle.Api.Filters;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Endpoints
{
    public static class RegressorEndpoints
    {
        public static void MapRegressorEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/regressor/get/prediction",
                    async (IMediator mediator, [AsParameters] GetPredictionQuery query) =>
                    {
                        var result = await mediator.Send(query);

                        return result is null ? Results.NotFound() : Results.Ok(result);

                    }).AddEndpointFilter<ValidationFilter<GetPredictionQuery>>();

            app.MapGet("/api/regressor/get/comparison",
                    async (IMediator mediator, [AsParameters] GetComparisonQuery query) =>
                    {
                        var result = await mediator.Send(query);

                        return result is null ? Results.NotFound() : Results.Ok(result);

                    }).AddEndpointFilter<ValidationFilter<GetComparisonQuery>>();

        }
    }
}
