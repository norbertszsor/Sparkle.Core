using LinqToDB;
using LinqToDB.Data;
using Sparkle.Domain.Models;

namespace Sparkle.Domain.Data
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

        public ITable<ReadingEm> Readings => this.GetTable<ReadingEm>();
    }
}
