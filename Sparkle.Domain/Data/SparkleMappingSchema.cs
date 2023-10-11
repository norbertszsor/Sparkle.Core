using LinqToDB.Mapping;
using Sparkle.Domain.Models;
using Sparkle.Domain.Extensions;

namespace Sparkle.Domain.Data
{
    public class SparkleMappingSchema : MappingSchema
    {
        public SparkleMappingSchema()
        {
            var builder = new FluentMappingBuilder(this);

            builder.Entity<CompanyEm>().HasTableName("Companies")
                .Property(x => x.Id).HasColumnName("Id").IsPrimaryKey()
                .Property(x => x.Name).HasColumnName("Name").IsNotNull()
                .Property(x => x.Description).HasColumnName("Description").IsNullable()
                .Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsNotNull()
                .Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable()
                .Property(x => x.Meters).HasAssociation(nameof(CompanyEm.Id), nameof(MeterEm.CompanyId));


            builder.Entity<MeterEm>().HasTableName("Meters")
                .Property(x => x.Id).HasColumnName("Id").IsPrimaryKey()
                .Property(x => x.Name).HasColumnName("Name").IsNotNull()
                .Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsNotNull()
                .Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable()
                .Property(x => x.CompanyId).HasColumnName("CompanyId").IsNullable()
                .Property(x => x.Readings).HasAssociation(nameof(MeterEm.Id), nameof(ReadingEm.MeterId));

            builder.Entity<ReadingEm>().HasTableName("Readings")
                .Property(x => x.Id).HasColumnName("Id").IsPrimaryKey()
                .Property(x => x.Time).HasColumnName("Time").IsNotNull()
                .Property(x => x.Value).HasColumnName("Value").IsNotNull()
                .Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsNotNull()
                .Property(x => x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable()
                .Property(x => x.MeterId).HasColumnName("MeterId").IsNullable();

            builder.Entity<ApiTokenEm>().HasTableName("ApiToken")
                .Property(x=>x.Id).HasColumnName("Id").IsPrimaryKey()
                .Property(x=>x.TokenHash).HasColumnName("TokenHash").IsNotNull()
                .Property(x=>x.CreatedAt).HasColumnName("CreatedAt").IsNotNull()
                .Property(x=>x.UpdatedAt).HasColumnName("UpdatedAt").IsNullable();


            builder.Build();
        }
    }
}
