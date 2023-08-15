using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;

namespace Sparkle.Api.Shared.Extensions
{
    public static class ApplicationBuilderExstension
    {
        public static void RunSeeder(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

            var context = scope.ServiceProvider.GetService<SparkleContext>();

            if (seeder != null && context != null)
            {
                seeder.SeedAsync(context)
                    .GetAwaiter()
                    .GetResult();
            }
            else
            {
                throw new Exception("Seeder or DataContext are not registered in DI container.");
            }
        }

        public static void RunMigrator(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

            var context = scope.ServiceProvider.GetService<SparkleContext>();

            if (seeder != null && context != null)
            {
                seeder.MigrateAsync(context)
                    .GetAwaiter()
                    .GetResult();
            }
            else
            {
                throw new Exception("Seeder or DataContext are not registered in DI container.");
            }
        }
    }
}
