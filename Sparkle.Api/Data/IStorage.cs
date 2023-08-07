using Sparkle.Api.Domain;
using Sparkle.Api.Domain.Models;

namespace Sparkle.Api.Data
{
    public interface IStorage
    {
        IQueryable<CompanyEm> Companies { get; }

        IQueryable<MeterEm> Meters { get; }

        IQueryable<ReadingsEm> Readings { get; }
    }
}
