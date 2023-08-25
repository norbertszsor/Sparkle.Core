namespace Sparkle.Api.Domain.Models
{
    public class ReadingsEm : IEntity<string?>
    {
        public string? Id { get; set; }

        public DateTime Time { get; set; }

        public double Value { get; set; }

        public string? MeterId { get; set; }
        
        public MeterEm? Meter { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
