using Sparkle.Transfer.Data;
using Sparkle.Transfer.Enums;

namespace Sparkle.Transfer.Query
{
    public class GetPredictionQuery : IQuery<PredictionDto>
    {
        public required string MeterId { get; set; }

        public required ReggressorTimeSpanEnum Hours { get; set; }
    }
}
