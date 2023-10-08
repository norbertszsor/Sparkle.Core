﻿using Sparkle.Transfer.Data;
using Sparkle.Transfer.Enums;
using Sparkle.Transfer.Interfaces;

namespace Sparkle.Transfer.Query
{
    public class GetPredictionQuery : IQuery<PredictionDto>
    {
        public required string MeterId { get; set; }

        public required RegressorTimeSpanEnum Hours { get; set; }
    }
}
