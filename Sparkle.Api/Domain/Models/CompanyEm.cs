namespace Sparkle.Api.Domain.Models
{
    public class CompanyEm : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public IEnumerable<MeterEm>? Meters { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
