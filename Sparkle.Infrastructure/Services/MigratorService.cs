using LinqToDB.Data;
using Sparkle.Domain.Data;
using Sparkle.Domain.Interfaces;
using Sparkle.Shared.Comparators;
using Sparkle.Shared.Helpers;
using System.Data.SQLite;

namespace Sparkle.Infrastructure.Services
{
    public class MigratorService : IMigratorService
    {
        private readonly string _migrationFolderPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations");

        public async Task MigrateAsync(SparkleContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionString))
            {
                throw ThrowHelper.Throw<SeederService>("Connection string is empty.");
            }

            SQLiteConnection.CreateFile(context.Connection.DataSource);

            await using (await context.BeginTransactionAsync())
            {
                foreach (var migration in GetMigrations())
                {
                    var sql = await File.ReadAllTextAsync(migration.FullName);

                    await context.ExecuteAsync(sql);
                }

                await context.CommitTransactionAsync();
            }
        }

        private IEnumerable<FileInfo> GetMigrations()
        {
            return Directory.GetFiles(_migrationFolderPath, "*.sql")
                .Select(f => new FileInfo(f))
                .OrderBy(f => f, new FileInfoCreationDateComparer())
                .ToList();
        }
    }
}
