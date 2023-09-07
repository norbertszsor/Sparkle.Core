namespace Sparkle.Transfer.Data
{
    public class PredictionDto
    {
        public string? MeterName { get; set; }
        public Dictionary<DateTime, double>? Prediction { get; set; }
    }
}
