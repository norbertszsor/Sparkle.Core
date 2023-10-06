using MediatR;
using Sparkle.Domain.Interfaces;
using Sparkle.Domain.Models;
using Sparkle.Infrastructure.Mappers;
using Sparkle.Shared.Helpers;
using Sparkle.Transfer.Data;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Handlers
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

            var company = companies.FirstOrDefault()?.MapToDto() ??
                throw ThrowHelper.Throw<CompanyHandler>($"Company not found");

            return company;
        }
    }
}
