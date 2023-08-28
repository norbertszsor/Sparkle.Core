using SparkleRegressor.Client.Models;

namespace SparkleRegressor.Client.Abstraction
{
    public interface ISparkleRegressorClient
    {
        Task<Dictionary<DateTime,double>?> GetPredictionAsync(GetPredictionCm cmQuery);
    }
}
