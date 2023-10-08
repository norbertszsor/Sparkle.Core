using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sparkle.Domain.Data;
using Sparkle.Domain.Interfaces;
using Sparkle.Shared.Helpers;

namespace Sparkle.Shared.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void RunSeeder(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<ISeederService>();

            var context = scope.ServiceProvider.GetService<SparkleContext>();

            if (seeder == null || context == null)
            {
                throw ThrowHelper.Throw<SparkleContext>("Seeder or DataContext are not registered in DI container.");
            }

            seeder.SeedAsync(context)
                .GetAwaiter()
                .GetResult();
        }

        public static void RunMigrator(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var migrator = scope.ServiceProvider.GetRequiredService<IMigratorService>();

            var context = scope.ServiceProvider.GetService<SparkleContext>();

            if (migrator == null || context == null)
            {
                throw ThrowHelper.Throw<SparkleContext>("Migrator or DataContext are not registered in DI container.");
            }

            migrator.MigrateAsync(context)
                .GetAwaiter()
                .GetResult();
        }
    }
}
