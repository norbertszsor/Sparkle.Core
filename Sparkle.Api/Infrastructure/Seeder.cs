using LinqToDB;
using LinqToDB.Data;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Shared.Comparers;
using Sparkle.Api.Shared.Extensions;
using Sparkle.Api.Shared.Helpers;
using System.Data.SQLite;
using System.Globalization;

namespace Sparkle.Api.Infrastructure
{
    public class Seeder : ISeeder
    {
        private readonly string _migrationFolderPath;
        private readonly string _seedFilePath;

        public Seeder()
        {
            _migrationFolderPath = Path.Combine(Directory.GetCurrentDirectory(),
                               "Infrastructure", "Migrations");

            _seedFilePath = Path.Combine(Directory.GetCurrentDirectory(),
                               "Infrastructure", "Seed", "data.csv");
        }

        public async Task SeedAsync(SparkleContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionString))
            {
                throw ThrowHelper.Throw<Seeder>("Connection string is empty.");
            }

            using (await context.BeginTransactionAsync())
            {
                var companyId = await context.Companies.Where(x => x.Name == "godcompany").Select(x => x.Id).FirstOrDefaultAsync();

                if (!string.IsNullOrEmpty(companyId))
                {
                    return;
                }

                companyId = await context.InsertWithGuidIdentityAsync(new CompanyEm
                {
                    Name = "godcompany",
                    Description = "god is sparkle",
                    CreatedAt = DateTime.UtcNow
                });

                using (var reader = new StreamReader(_seedFilePath))
                {
                    var columns = GetColumnsFromStream(reader).ToList();

                    var dateTimeIndexes = columns[0];

                    columns.RemoveAt(0);

                    var meters = columns.Select(column =>
                    {
                        var meterId = Guid.NewGuid().ToString();

                        var meter = new MeterEm
                        {
                            Id = meterId,
                            Name = column[0],
                            CompanyId = companyId,
                            CreatedAt = DateTime.UtcNow,
                            Readings = column
                            .Skip(1)
                            .Select((value, i) => new ReadingEm
                            {
                                Id = Guid.NewGuid().ToString(),
                                MeterId = meterId,
                                Time = DateTime.Parse(dateTimeIndexes[i + 1]),
                                Value = Convert.ToDouble(value,new NumberFormatInfo() 
                                { 
                                    NumberDecimalSeparator = "."}
                                ),
                                CreatedAt = DateTime.UtcNow,
                            }).ToHashSet()
                        };

                        return meter;
                    }).ToHashSet();

                    var readings = meters.SelectMany(x => x.Readings);

                    await context.BulkCopyAsync(meters);
                    await context.BulkCopyAsync(readings);
                }

                await context.CommitTransactionAsync();
            }
        }

        public async Task MigrateAsync(SparkleContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionString))
            {
                throw ThrowHelper.Throw<Seeder>("Connection string is empty.");
            }

            throw ThrowHelper.Throw<Seeder>(AppContext.BaseDirectory+"-"+Directory.GetCurrentDirectory());

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
               throw ThrowHelper.Throw<Seeder>("Seeder file are empty.");

            return Enumerable.Range(0, csvArray[0].Length)
                .Select(i => csvArray.Select(x => x[i]).ToArray());
        }
    }
}
