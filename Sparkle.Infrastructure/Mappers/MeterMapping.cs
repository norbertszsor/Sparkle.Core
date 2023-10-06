using Sparkle.Domain.Models;
using Sparkle.Transfer.Data;
using System.Linq.Expressions;

namespace Sparkle.Infrastructure.Mappers
{
    public static class MeterMapping
    {
        public static MeterDto MapToDto(this MeterEm meterEm)
        {
            return MapToDtoExpression().Compile().Invoke(meterEm);
        }

        public static MeterEm MapToEm(this MeterDto meterDto)
        {
            return MapToEmExpression().Compile().Invoke(meterDto);
        }

        private static Expression<Func<MeterEm, MeterDto>> MapToDtoExpression()
        {
            return meterEm => new MeterDto
            {
                Id = meterEm.Id,
                Name = meterEm.Name,
                CompanyId = meterEm.CompanyId,
            };
        }

        private static Expression<Func<MeterDto, MeterEm>> MapToEmExpression()
        {
            return meterDto => new MeterEm
            {
                Id = meterDto.Id,
                Name = meterDto.Name,
                CompanyId = meterDto.CompanyId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

    }
}
