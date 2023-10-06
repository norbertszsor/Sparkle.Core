using Sparkle.Domain.Models;
using Sparkle.Transfer.Data;
using System.Linq.Expressions;

namespace Sparkle.Infrastructure.Mappers
{
    public static class CompanyMapping
    {
        public static CompanyDto MapToDto(this CompanyEm companyEm)
        {
            return MapToDtoExpression().Compile().Invoke(companyEm);
        }

        public static CompanyEm MapToEm(this CompanyDto companyDto)
        {
            return MapToEmExpression().Compile().Invoke(companyDto);
        }

        private static Expression<Func<CompanyEm, CompanyDto>> MapToDtoExpression()
        {
            return companyEm => new CompanyDto
            {
                Id = companyEm.Id,
                Name = companyEm.Name,
                Description = companyEm.Description
            };
        }

        private static Expression<Func<CompanyDto, CompanyEm>> MapToEmExpression()
        {
            return companyDto => new CompanyEm
            {
                Id = companyDto.Id,
                Name = companyDto.Name,
                Description = companyDto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
