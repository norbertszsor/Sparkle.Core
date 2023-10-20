using Sparkle.Domain.Data;
using Sparkle.Domain.Interfaces;
using Sparkle.Shared.Helpers;
using System.Data.SQLite;
using LinqToDB;
using Sparkle.Domain.Models;

namespace Sparkle.Infrastructure.Services
{
    public class MigratorService : IMigratorService
    {
        public async Task MigrateAsync(SparkleContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionString))
            {
                throw ThrowHelper.Throw<SeederService>("Connection string is empty.");
            }

            SQLiteConnection.CreateFile(context.Connection.DataSource);

            await using (await context.BeginTransactionAsync())
            {
                await context.CreateTableAsync<CompanyEm>(tableOptions: TableOptions.CreateIfNotExists);

                await context.CreateTableAsync<MeterEm>(tableOptions: TableOptions.CreateIfNotExists);

                await context.CreateTableAsync<ReadingEm>(tableOptions: TableOptions.CreateIfNotExists);

                await context.CreateTableAsync<ApiTokenEm>(tableOptions: TableOptions.CreateIfNotExists);

                await context.CommitTransactionAsync();
            }
        }
    }
}
