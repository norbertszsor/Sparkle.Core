using SparkleRegressor.Client.Models;

namespace SparkleRegressor.Client.Abstraction
{
    public interface ISparkleRegressorClient
    {
        Task<PredictionCm?> GetPredictionAsync(GetPredictionCm cmQuery);
    }
}
