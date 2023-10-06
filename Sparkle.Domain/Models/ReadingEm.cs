using Sparkle.Domain.Interfaces;

namespace Sparkle.Domain.Models
{
    public class ReadingEm : IEntity<string?>
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
