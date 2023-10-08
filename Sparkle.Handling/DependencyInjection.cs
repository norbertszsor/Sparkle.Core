using Microsoft.Extensions.DependencyInjection;
using Sparkle.Handling.Validators;
using FluentValidation;
using System.Reflection;
using Sparkle.Handling.Middlewares;

namespace Sparkle.Handling
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHandling(this IServiceCollection services)
        {
            #region validators
            services.AddValidatorsFromAssemblyContaining(typeof(PredictionQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(ComparisonQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(CompanyQueryValidator));

            services.AddValidatorsFromAssemblyContaining(typeof(MeterQueryValidator));
            #endregion

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                options.Lifetime = ServiceLifetime.Scoped;
            });

            services.AddTransient<ErrorHandlingMiddleware>();

            return services;
        }
    }
}
