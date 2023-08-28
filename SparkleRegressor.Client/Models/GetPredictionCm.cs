namespace SparkleRegressor.Client.Models
{
    public class GetPredictionCm
    {
        public int? TimeSeriesDictId { get; set; }

        public Dictionary<DateTime, double>? TimeSeriesDict { get; set; }

        public int PredictionTicks { get; set; }

        public CountryCodeCm? CountryCodeDto { get; set; }
    }
}
