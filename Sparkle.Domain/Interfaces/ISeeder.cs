using Sparkle.Domain.Data;

namespace Sparkle.Domain.Interfaces
{
    public interface ISeeder
    {
        public Task SeedAsync(SparkleContext context);

        public Task MigrateAsync(SparkleContext context);
    }
}
