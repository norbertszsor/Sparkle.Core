using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sparkle.Transfer.Data;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Endpoints
{
    public static class ReggressorEndpoints
    {
        public static void MapReggressorEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/prediction", async (IMediator mediator, [FromBody] GetPredictionQuery query) =>
            {
                var result = await mediator.Send(query);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });
        }
    }
}
