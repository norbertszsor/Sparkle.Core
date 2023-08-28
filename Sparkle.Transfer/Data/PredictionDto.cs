namespace Sparkle.Transfer.Data
{
    public class PredictionDto
    {
        public string? MeterName { get; set; }
        public Dictionary<DateTime, double>? Predictions { get; set; }
    }
}
