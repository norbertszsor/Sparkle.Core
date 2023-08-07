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
            var mappings = new MappingSchema();

            var builder = new FluentMappingBuilder(mappings);

            builder.Entity<CompanyEm>().HasTableName("Companies")
                .Property(x => x.Id).HasColumnName("Id").IsIdentity().IsPrimaryKey()
                .Property(x => x.Name).HasColumnName("Name").IsNotNull()
                .Property(x => x.Description).HasColumnName("Description").IsNullable()
                .Property(x => x.Meters).HasColumnName("Meters").IsNullable()
                .Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsNullable()
                .Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable();

            builder.Entity<MeterEm>().HasTableName("Meters")
                .Property(x => x.Id).HasColumnName("Id").IsIdentity().IsPrimaryKey()
                .Property(x => x.Name).HasColumnName("Name").IsNotNull()
                .Property(x => x.Readings).HasColumnName("Readings").IsNullable()
                .Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsNullable()
                .Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable();

            builder.Entity<ReadingsEm>().HasTableName("Readings")
                .Property(x => x.Id).HasColumnName("Id").IsIdentity().IsPrimaryKey()
                .Property(x => x.Value).HasColumnName("Value").IsNotNull()
                .Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsNullable()
                .Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable();


            AddMappingSchema(mappings);
        }

        public ITable<CompanyEm> Companies { get { return this.GetTable<CompanyEm>(); } }

        public ITable<MeterEm> Meters { get { return this.GetTable<MeterEm>(); } }

        public ITable<ReadingsEm> Readings { get { return this.GetTable<ReadingsEm>(); } }

    }
}
