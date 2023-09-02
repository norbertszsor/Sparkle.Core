using Sparkle.Transfer.Data;

namespace Sparkle.Transfer.Query
{
    public class GetPredictionQuery : IQuery<PredictionDto>
    {
        public required string MeterName { get; set; }

        public required int Hours { get; set; }
    }
}
