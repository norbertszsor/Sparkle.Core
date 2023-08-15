using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Sparkle.Api.Domain.Models;

namespace Sparkle.Api.Data
{
    public class SparkleContext : DataConnection
    {
        public SparkleContext(DataOptions<SparkleContext> options)
            : base(options.Options)
        {
            AddMappingSchema(new SparkleMappingSchema());
        }

        public ITable<CompanyEm> Companies => this.GetTable<CompanyEm>();

        public ITable<MeterEm> Meters => this.GetTable<MeterEm>();

        public ITable<ReadingsEm> Readings => this.GetTable<ReadingsEm>();
    }
}
