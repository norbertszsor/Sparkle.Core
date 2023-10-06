using LinqToDB;
using MediatR;
using Sparkle.Domain.Data;
using Sparkle.Domain.Interfaces;
using Sparkle.Domain.Models;
using Sparkle.Infrastructure.Mappers;
using Sparkle.Shared.Extensions;
using Sparkle.Shared.Helpers;
using Sparkle.Transfer;
using Sparkle.Transfer.Data;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Handlers
{
    public class MeterHandler : IRequestHandler<GetMeterListQuery, PagedList<MeterDto>>
    {
        private readonly SparkleContext _storage;
        private readonly IReposiotry<CompanyEm, string?> _companyRepository;

        public MeterHandler(SparkleContext context, IReposiotry<CompanyEm, string?> companyRepository)
        {
            _storage = context;
            _companyRepository = companyRepository;
        }

        public async Task<PagedList<MeterDto>> Handle(GetMeterListQuery request, CancellationToken cancellationToken)
        {
            _ = await _companyRepository.GetByIdAsync(request.CompanyId) ??
                throw ThrowHelper.Throw<MeterHandler>($"Company with id {request.CompanyId} not found");

            var companyMeters = await _storage.Meters.Where(x => x.CompanyId == request.CompanyId)
                .Select(x => x.MapToDto())
                .ToListAsync(token: cancellationToken);

            return companyMeters.ToPagedList(request);
        }
    }
}
