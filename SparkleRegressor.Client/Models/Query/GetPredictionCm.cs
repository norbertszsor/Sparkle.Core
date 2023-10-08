namespace SparkleRegressor.Client.Models.Query
{
    public class GetPredictionCm
    {
        public int? TimeSeriesDictId { get; set; }

        public Dictionary<DateTime, double>? TimeSeriesDict { get; set; }

        public int PredictionTicks { get; set; }

        public CountryCodeCm? CountryCode { get; set; }
    }
}
