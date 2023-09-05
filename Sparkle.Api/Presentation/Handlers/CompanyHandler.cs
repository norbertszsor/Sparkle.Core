using MediatR;
using Sparkle.Api.Data.Interfaces;
using Sparkle.Api.Domain.Models;
using Sparkle.Api.Shared.Mappers;
using Sparkle.Transfer.Data;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Handlers
{
    public class CompanyHandler : IRequestHandler<GetCompanyQuery, CompanyDto?>
    {
        private readonly IReposiotry<CompanyEm, string?> _companyRepository;

        public CompanyHandler(IReposiotry<CompanyEm, string?> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto?> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var companies = await _companyRepository.GetAllAsync();

            var company = companies.FirstOrDefault()?.MapToDto();

            return company;
        }
    }
}
