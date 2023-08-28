using SparkleRegressor.Client.Abstraction;
using SparkleRegressor.Client.Models;
using System.Net.Http.Json;

namespace SparkleRegressor.Client.Logic
{
    public class SparkleRegressorClient : ISparkleRegressorClient
    {
        private readonly HttpClient _httpClient;

        public SparkleRegressorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PredictionCm?> GetPredictionAsync(GetPredictionCm cmQuery)
        {
            var predictionUrl = "reggressor/predict";

            var response = await _httpClient.PostAsJsonAsync(predictionUrl, cmQuery);

            if (response.IsSuccessStatusCode)
            {
                var prediction = await response.Content.ReadFromJsonAsync<PredictionCm>();
                return prediction;
            }

            return null;
        }
    }
}
