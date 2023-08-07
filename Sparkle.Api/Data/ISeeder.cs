namespace Sparkle.Api.Data
{
    public interface ISeeder
    {
        public Task SeedAsync(SparkleContext context);

        public Task MigrateAsync(SparkleContext context);
    }
}
