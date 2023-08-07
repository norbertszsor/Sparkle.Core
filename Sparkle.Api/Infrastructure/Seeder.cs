using LinqToDB.Data;
using Sparkle.Api.Data;
using Sparkle.Api.Shared.Comparers;

namespace Sparkle.Api.Infrastructure
{
    public class Seeder : ISeeder
    {
        private readonly string _migrationFolder;

        public Seeder()
        {
            _migrationFolder = Path.Combine(AppContext.BaseDirectory,
                "Infrastructure", "Migrations");
        }

        public async Task SeedAsync(SparkleContext context)
        {
            await Task.Delay(1);

            throw new NotImplementedException();
        }

        public async Task MigrateAsync(SparkleContext context)
        {
            using var sparkleDb = new DataConnection(context.ConnectionString);

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
            return Directory.GetFiles(_migrationFolder, "*.sql")
                .Select(f => new FileInfo(f))
                .OrderBy(f => f, new FileInfoCreationDateComparer())
                .ToList();
        }
    }
}
