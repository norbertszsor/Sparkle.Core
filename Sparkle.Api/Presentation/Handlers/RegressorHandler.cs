using LinqToDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Shared.Extensions;
using Sparkle.Api.Shared.Helpers;
using Sparkle.Transfer.Data;
using Sparkle.Transfer.Query;
using SparkleRegressor.Client.Abstraction;
using SparkleRegressor.Client.Models;

namespace Sparkle.Api.Presentation.Handlers
{
    public class RegressorHandler : IRequestHandler<GetPredictionQuery, PredictionDto>
    {
        private readonly SparkleContext _storage;
        private readonly IReposiotry<MeterEm, string?> _meterRepository;
        private readonly ISparkleRegressorClient _sparkleRegressorClient;

        public RegressorHandler(
            SparkleContext storage,
            IReposiotry<MeterEm, string?> meterRepository,
            ISparkleRegressorClient sparkleRegressorClient)
        {
            _storage = storage;
            _meterRepository = meterRepository;
            _sparkleRegressorClient = sparkleRegressorClient;
        }

        public async Task<PredictionDto> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            var meter = _storage.Meters.LoadWith(x => x.Readings).FirstOrDefault(x => x.Name == request.MeterName);

            var response = await _sparkleRegressorClient.GetPredictionAsync(new GetPredictionCm
            {
                TimeSeriesDictId = meter?.Name.GetNumber(),
                TimeSeriesDict = meter?.Readings?.ToDictionary(x => x.Time, x => x.Value),
                PredictionTicks = request.Hours,
                CountryCodeDto = new CountryCodeCm
                {
                    Code = "PT"
                },
            });

            var result = new PredictionDto
            {
                MeterName = meter.Name,
                Predictions = response?.Predictions,
            };

            if (result.Predictions == null)
            {
                throw ThrowHelper.Throw<RegressorHandler>("Predictions are empty");
            }

            return result;
        }
    }
}
