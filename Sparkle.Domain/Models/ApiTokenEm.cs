using Sparkle.Domain.Interfaces;

namespace Sparkle.Domain.Models
{
    public class ApiTokenEm : IEntity<string?>
    {
        public string? Id { get; set; }

        public required string? TokenHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
