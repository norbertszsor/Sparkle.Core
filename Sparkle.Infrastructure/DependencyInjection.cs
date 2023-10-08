using Microsoft.Extensions.DependencyInjection;
using Sparkle.Domain.Interfaces;
using Sparkle.Domain.Models;
using Sparkle.Infrastructure.Repositories;
using Sparkle.Infrastructure.Services;

namespace Sparkle.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            #region repositories
            services.AddScoped<IRepository<MeterEm, string?>, Repository<MeterEm, string?>>();

            services.AddScoped<IRepository<CompanyEm, string?>, Repository<CompanyEm, string?>>();

            services.AddScoped<IRepository<ReadingEm, string?>, Repository<ReadingEm, string?>>();
            #endregion
            
            services.AddSingleton<IMigratorService, MigratorService>();

            services.AddSingleton<ISeederService, SeederService>();

            return services;
        }
    }
}
