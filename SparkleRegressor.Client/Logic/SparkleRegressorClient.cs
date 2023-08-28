using SparkleRegressor.Client.Abstraction;
using SparkleRegressor.Client.Models;
using System.Collections;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SparkleRegressor.Client.Logic
{
    public class SparkleRegressorClient : ISparkleRegressorClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public SparkleRegressorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<Dictionary<DateTime,double>?> GetPredictionAsync(GetPredictionCm cmQuery)
        {
            var predictionUrl = "api/reggressor/predict";

            var jsonRequest = JsonSerializer.Serialize(cmQuery, _jsonSerializerOptions);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(predictionUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting prediction: {response.StatusCode}");
            }

            var result = await response.Content.ReadFromJsonAsync<ResponseCm>();

            var jsonElementData = result?.Data as JsonElement? ?? 
                throw new Exception("Error getting prediction");

            var jsonElementPredictions = jsonElementData.GetProperty("predictions");

            var predictions = jsonElementPredictions.Deserialize<Dictionary<string,double>>();

            var resultPredictions = predictions?.ToDictionary(x => DateTime.Parse(x.Key).ToUniversalTime(), x => x.Value);

            return resultPredictions;
        }
    }
}
