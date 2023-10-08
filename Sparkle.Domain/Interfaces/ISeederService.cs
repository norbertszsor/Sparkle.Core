using Sparkle.Domain.Data;

namespace Sparkle.Domain.Interfaces
{
    public interface ISeederService
    {
        public Task SeedAsync(SparkleContext context);
    }
}
