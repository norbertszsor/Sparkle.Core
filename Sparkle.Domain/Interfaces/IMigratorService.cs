using Sparkle.Domain.Data;

namespace Sparkle.Domain.Interfaces
{
    public interface IMigratorService
    {
        public Task MigrateAsync(SparkleContext context);
    }
}
