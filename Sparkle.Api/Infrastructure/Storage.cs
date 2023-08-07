using LinqToDB;
using Sparkle.Api.Data;
using Sparkle.Api.Domain.Models;

namespace Sparkle.Api.Infrastructure
{
    public class Storage : IStorage
    {
        private readonly SparkleContext _sparkleContext;

        public Storage(SparkleContext sparkleContext)
        {
            _sparkleContext = sparkleContext;
        }

        public IQueryable<CompanyEm> Companies => _sparkleContext.GetTable<CompanyEm>();

        public IQueryable<MeterEm> Meters => _sparkleContext.GetTable<MeterEm>();

        public IQueryable<ReadingsEm> Readings => _sparkleContext.GetTable<ReadingsEm>();
    }
}
