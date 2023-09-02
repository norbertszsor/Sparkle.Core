using FluentValidation;
using Sparkle.Api.Shared.Helpers;

namespace Sparkle.Api.Presentation
{
    public class ValidationFilter<TModel> : IEndpointFilter where TModel : class
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService(typeof(IValidator<TModel>)) ??
                throw ThrowHelper.Throw<ValidationFilter<TModel>>("Validator is null");

            var model = context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(TModel)) ??
                throw ThrowHelper.Throw<ValidationFilter<TModel>>("Model is null");

            var result = await ((IValidator<TModel>)validator).ValidateAsync((TModel)model);

            if(!result.IsValid)
            {
                return Results.BadRequest(result.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            }

            return await next(context);
        }
    }
}
