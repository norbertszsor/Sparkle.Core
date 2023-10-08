using SparkleRegressor.Client.Models.Query;

namespace SparkleRegressor.Client.Abstraction
{
    public interface ISparkleRegressorClient
    {
        Task<Dictionary<DateTime, double>?> GetPredictionAsync(GetPredictionCm cmQuery);
    }
}
