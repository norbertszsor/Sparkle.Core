using LinqToDB;
using MediatR;
using Sparkle.Domain.Data;
using Sparkle.Domain.Models;
using Sparkle.Shared.Extensions;
using Sparkle.Shared.Helpers;
using Sparkle.Transfer.Data;
using Sparkle.Transfer.Query;
using SparkleRegressor.Client.Abstraction;
using SparkleRegressor.Client.Models;
using SparkleRegressor.Client.Models.Query;

namespace Sparkle.Handling.Handlers
{
    public class RegressorHandler : IRequestHandler<GetPredictionQuery, PredictionDto>,
        IRequestHandler<GetComparisonQuery, ComparisonDto>
    {
        private readonly SparkleContext _storage;
        private readonly ISparkleRegressorClient _sparkleRegressorClient;

        public RegressorHandler(SparkleContext storage, ISparkleRegressorClient sparkleRegressorClient)
        {
            _storage = storage;
            _sparkleRegressorClient = sparkleRegressorClient;
        }

        public async Task<PredictionDto> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            var meter = GetMeterWithReadings(request.MeterId);

            var response = await _sparkleRegressorClient.GetPredictionAsync(new GetPredictionCm
                           {
                               TimeSeriesDictId = meter?.Name.GetNumber(),
                               TimeSeriesDict = meter?.Readings?.Where(x => x.Time <= DateTime.UtcNow)
                                       .ToDictionary(x => x.Time, x => x.Value),
                               PredictionTicks = (int)request.Hours,
                               CountryCode = new CountryCodeCm
                               {
                                   Code = "PT"
                               },
                           }) ?? 
                           throw ThrowHelper.Throw<RegressorHandler>("Sprakle regressor response predictions are empty");

            return new PredictionDto
            {
                MeterName = meter?.Name,
                Prediction = response
            };
        }

        public async Task<ComparisonDto> Handle(GetComparisonQuery request, CancellationToken cancellationToken)
        {
            var meter = GetMeterWithReadings(request.MeterId);

            var response = await _sparkleRegressorClient.GetPredictionAsync(new GetPredictionCm
                           {
                               TimeSeriesDictId = meter?.Name.GetNumber(),
                               TimeSeriesDict =
                                   meter?.Readings?.Where(x => x.Time <= DateTime.UtcNow - TimeSpan.FromHours((int)request.Hours))
                                       .ToDictionary(x => x.Time, x => x.Value),
                               PredictionTicks = (int)request.Hours,
                               CountryCode = new CountryCodeCm
                               {
                                   Code = "PT"
                               },
                           }) ??
                           throw ThrowHelper.Throw<RegressorHandler>("Sprakle regressor response predictions are empty");

            return new ComparisonDto
            {
                MeterName = meter?.Name,
                Prediction = response,
                Previous = meter?.Readings?.Where(x => response.ContainsKey(x.Time - TimeSpan.FromHours(5)))
                    .Select(x => new { Time = x.Time + TimeSpan.FromHours(5), x.Value })
                    .ToDictionary(x => x.Time, x => x.Value)
            };
        }

        private MeterEm GetMeterWithReadings(string meterId)
        {
            return _storage.Meters.LoadWith(x => x.Readings)
                       .FirstOrDefault(x => x.Id == meterId) ??
                   throw ThrowHelper.Throw<RegressorHandler>($"Meter with id {meterId} not found");
        }
    }
}
