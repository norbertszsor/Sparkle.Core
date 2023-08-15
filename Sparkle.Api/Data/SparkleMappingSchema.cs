using LinqToDB.Mapping;
using Sparkle.Api.Domain.Models;

namespace Sparkle.Api.Data
{
    public class SparkleMappingSchema : MappingSchema
    {
        public SparkleMappingSchema()
        {
            var builder = new FluentMappingBuilder(this);

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
                .Property(x => x.Time).HasColumnName("Time").IsNotNull()
                .Property(x => x.Value).HasColumnName("Value").IsNotNull()
                .Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsNullable()
                .Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable();

            builder.Build();
        }
    }
}
