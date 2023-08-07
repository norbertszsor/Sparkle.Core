﻿namespace Sparkle.Api.Domain
{
    public interface IEntity<Tkey>
    {
        Tkey Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
