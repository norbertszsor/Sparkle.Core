using LinqToDB;
using LinqToDB.Data;
using Sparkle.Domain.Data;
using Sparkle.Domain.Models;
using Sparkle.Domain.Interfaces;
using Sparkle.Shared.Helpers;
using System.Globalization;
using Sparkle.Shared.Extensions;

namespace Sparkle.Infrastructure.Services
{
    public class SeederService : ISeederService
    {
        private readonly string _seedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seed", "data.csv");

        public async Task SeedAsync(SparkleContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionString))
            {
                throw ThrowHelper.Throw<SeederService>("Connection string is empty.");
            }

            await using (await context.BeginTransactionAsync())
            {
                await SeedApiTokenAsync(context);

                var companyId = await SeedCompaniesAsync(context);

                await SeedMetersAsync(context, companyId);

                await context.CommitTransactionAsync();
            }
        }

        private static async Task SeedApiTokenAsync(SparkleContext context)
        {
            var envApiToken = Environment.GetEnvironmentVariable("API_TOKEN");

            if (string.IsNullOrEmpty(envApiToken))
            {
                throw ThrowHelper.Throw<SeederService>("env variable API_TOKEN is empty or isn't exist.");
            }

            var apiToken = await context.ApiTokens
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(apiToken))
            {
                await context.InsertWithGuidIdentityAsync(new ApiTokenEm
                {
                    Id = Guid.NewGuid().ToString(),
                    TokenHash = TokenHelper.HashToken(envApiToken),
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        private static async Task<string> SeedCompaniesAsync(SparkleContext context)
        {
            var companyId = await context.Companies
                .Where(x => x.Name == "god_company")
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return !string.IsNullOrEmpty(companyId)
                ? companyId
                : await context.InsertWithGuidIdentityAsync(new CompanyEm
                {
                    Name = "god_company",
                    Description = "seeded company description",
                    CreatedAt = DateTime.UtcNow
                });
        }

        private async Task SeedMetersAsync(SparkleContext context, string companyId)
        {
            using var reader = new StreamReader(_seedFilePath);
            var columns = GetColumnsFromStream(reader).ToList();

            var dateTimeIndexes = columns[0];

            columns.RemoveAt(0);

            var meters = columns.Select(column =>
            {
                var meterId = Guid.NewGuid()
                    .ToString();

                return new MeterEm
                {
                    Id = meterId,
                    Name = column[0],
                    CompanyId = companyId,
                    CreatedAt = DateTime.UtcNow,
                    Readings = column.Skip(1)
                        .Select((value, i) => new ReadingEm
                        {
                            Id = Guid.NewGuid().ToString(),
                            MeterId = meterId,
                            Time = DateTime.Parse(dateTimeIndexes[i + 1]),
                            Value = Convert.ToDouble(value,
                                new NumberFormatInfo()
                                {
                                    NumberDecimalSeparator = "."
                                }),
                            CreatedAt = DateTime.UtcNow,
                        }).ToHashSet()
                };

            }).ToHashSet();

            var readings = meters.SelectMany(x => x.Readings ?? Array.Empty<ReadingEm>());

            await context.BulkCopyAsync(meters);

            await context.BulkCopyAsync(readings);
        }

        private static IEnumerable<string[]> GetColumnsFromStream(TextReader reader)
        {
            var csvArray = reader.ReadToEnd()
                               .Split('\n')
                               .Select(x => x.Split(','))
                               .ToArray() ??
                           throw ThrowHelper.Throw<SeederService>("SeederService file are empty.");

            return Enumerable.Range(0, csvArray[0].Length).Select(i => csvArray.Select(x => x[i])
                .ToArray());
        }
    }
}
