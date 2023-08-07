namespace Sparkle.Api.Domain.Models
{
    public class MeterEm : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public Guid? CompanyId { get; set; }

        public CompanyEm? Company { get; set; }

        public IEnumerable<ReadingsEm>? Readings { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
