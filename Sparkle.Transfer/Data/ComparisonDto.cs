namespace Sparkle.Transfer.Data
{
    public class ComparisonDto : PredictionDto
    {
        public Dictionary<DateTime, double>? Previous { get; set; }
    }
}
