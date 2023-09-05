using LinqToDB;
using MediatR;
using Sparkle.Api.Data;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Shared;
using Sparkle.Api.Shared.Helpers;
using Sparkle.Api.Shared.Mappers;
using Sparkle.Transfer.Data;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Handlers
{
    public class MeterHandler : IRequestHandler<GetMeterListQuery, IEnumerable<MeterDto>>
    {
        private readonly SparkleContext _storage;
        private readonly IReposiotry<CompanyEm, string?> _companyRepository;

        public MeterHandler(SparkleContext context, IReposiotry<CompanyEm, string?> companyRepository)
        {
            _storage = context;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<MeterDto>> Handle(GetMeterListQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId) ?? 
                throw ThrowHelper.Throw<SparkleException>($"Company with id {request.CompanyId} not found");

            var companyMeters = await _storage.Meters.Where(x => x.CompanyId == request.CompanyId)
                .Select(x=>x.MapToDto())
                .ToListAsync(token: cancellationToken);

            return companyMeters;
        }
    }
}
