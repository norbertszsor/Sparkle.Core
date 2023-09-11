using Sparkle.Transfer.Data;
using Sparkle.Transfer.Enums;

namespace Sparkle.Transfer.Query
{
    public class GetComparisonQuery : IQuery<ComparisonDto>
    {
        public required string MeterId { get; set; }

        public required ReggressorTimeSpanEnum Hours { get; set; }
    }
}
