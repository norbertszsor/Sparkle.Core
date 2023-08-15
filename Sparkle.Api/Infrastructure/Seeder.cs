using LinqToDB;
using LinqToDB.Data;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Shared.Comparers;
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
            var sparkleDb = new DataConnection(context.Options);

            using (var t = sparkleDb.BeginTransactionAsync())
            {
                var testQuery = context.Companies
               .Where(x => x.Name == "godcompany")
               .Select(x => x.Id)
               .AsQueryable();

                var companyId = context.Companies.Where(x => x.Name == "godcompany").Select(x => x.Id).FirstOrDefault();

                if (companyId == Guid.Empty)
                {
                    companyId = (Guid)await context.InsertWithIdentityAsync(new CompanyEm
                    {
                        Name = "Company",
                        CreatedAt = DateTime.UtcNow,
                        Description = "God is sparkle",
                    });
                }
                await sparkleDb.CommitTransactionAsync();
            }

            using (var reader = new StreamReader(_seedFolderPath))
            {
                var columns = GetColumnsFromStream(reader);
            }

         
        }

        public async Task MigrateAsync(SparkleContext context)
        {
            if(string.IsNullOrEmpty(context.ConnectionString))
            {
                throw new Exception("Connection string is empty.");
            }   

            using var sparkleDb = new DataConnection(context.DataProvider, context.ConnectionString);

            SQLiteConnection.CreateFile(sparkleDb.Connection.DataSource);

            await sparkleDb.BeginTransactionAsync();

            foreach (var migration in GetMigrations())
            {
                var sql = await File.ReadAllTextAsync(migration.FullName);

                await sparkleDb.ExecuteAsync(sql);
            }

            await sparkleDb.CommitTransactionAsync();
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

            return Enumerable.Range(0, csvArray.GetLength(0))
                .Select(i => csvArray.Select(x => x[i]).ToArray());
        }
    }
}
