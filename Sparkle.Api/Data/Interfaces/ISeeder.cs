namespace Sparkle.Api.Data.Interfaces
{
    public interface ISeeder
    {
        public Task SeedAsync(SparkleContext context);

        public Task MigrateAsync(SparkleContext context);
    }
}
