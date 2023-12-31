﻿using Sparkle.Domain.Interfaces;

namespace Sparkle.Domain.Models
{
    public class MeterEm : IEntity<string?>
    {
        public string? Id { get; set; }

        public required string? Name { get; set; }

        public string? CompanyId { get; set; }

        public CompanyEm? Company { get; set; }

        public IEnumerable<ReadingEm>? Readings { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
