using LinqToDB;
using LinqToDB.Data;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Shared.Comparers;
using Sparkle.Api.Shared.Extensions;
using System.Data.SQLite;

namespace Sparkle.Api.Infrastructure
{
    public class Seeder : ISeeder
    {
        private readonly string _migrationFolderPath;
        private readonly string _seedFolderPath;
        public Seeder()
        {
            _migrationFolderPath = Path.Combine(Directory.GetCurrentDirectory(), 
                               "Infrastructure", "Migrations");

            _seedFolderPath = Path.Combine(Directory.GetCurrentDirectory(), 
                               "Infrastructure", "Seed", "data.csv");
        }

        public async Task SeedAsync(SparkleContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionString))
            {
                throw new Exception("Connection string is empty.");
            }

            using(await context.BeginTransactionAsync())
            {
                var companyId = await context.Companies.Where(x => x.Name == "godcompany").Select(x => x.Id).FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(companyId))
                {
                    throw new Exception("Database is already seeded.");
                }

                companyId = await context.InsertWithGuidIdentityAsync(new CompanyEm
                {
                    Name = "godcompany",
                    Description = "god is sparkle",
                    CreatedAt = DateTime.UtcNow
                });

                using (var reader = new StreamReader(_seedFolderPath))
                {
                    var columns = GetColumnsFromStream(reader).ToList();

                    var dateTimeIndexes = columns[0];

                    columns.RemoveAt(0);

                    var meters = columns.Select(x => new MeterEm
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = x[0],
                        CompanyId = companyId,
                        CreatedAt = DateTime.UtcNow,
                        Readings = x.Skip(1).Select((r, i) => new ReadingsEm
                        {
                            Id = Guid.NewGuid().ToString(),
                            Time = DateTime.Parse(dateTimeIndexes[i + 1]),
                            Value = Convert.ToDouble(r),
                            CreatedAt = DateTime.UtcNow,
                        }).ToArray()
                    });

                    var readings = meters.SelectMany(x => x.Readings);

                    await context.BulkCopyAsync(meters);
                    await context.BulkCopyAsync(readings);
                }

                await context.CommitTransactionAsync();
            }
        }

        public async Task MigrateAsync(SparkleContext context)
        {
            if(string.IsNullOrEmpty(context.ConnectionString))
            {
                throw new Exception("Connection string is empty.");
            }   

            SQLiteConnection.CreateFile(context.Connection.DataSource);

            using (await context.BeginTransactionAsync())
            {
                foreach (var migration in GetMigrations())
                {
                    var sql = await File.ReadAllTextAsync(migration.FullName);

                    await context.ExecuteAsync(sql);
                }

                await context.CommitTransactionAsync();
            }
        }

        private ICollection<FileInfo> GetMigrations()
        {
            return Directory.GetFiles(_migrationFolderPath, "*.sql")
                .Select(f => new FileInfo(f))
                .OrderBy(f => f, new FileInfoCreationDateComparer())
                .ToList();
        }

        private static IEnumerable<string[]> GetColumnsFromStream(StreamReader reader)
        {
            var csvArray = reader.ReadToEnd().Split('\n').Select(x => x.Split(',')).ToArray() ??
                throw new Exception("stream file is empty.");

            return Enumerable.Range(0, csvArray[0].Length)
                .Select(i => csvArray.Select(x => x[i]).ToArray());
        }
    }
}
