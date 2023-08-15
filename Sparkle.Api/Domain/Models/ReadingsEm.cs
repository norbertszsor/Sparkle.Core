namespace Sparkle.Api.Domain.Models
{
    public class ReadingsEm : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public double Value { get; set; }

        public Guid? MeterId { get; set; }
        
        public MeterEm? Meter { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
