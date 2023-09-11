using FluentValidation;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Validation
{
    public class MeterQueryValidator : AbstractValidator<GetMeterListQuery>
    {
        public MeterQueryValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty()
                .WithMessage("CompanyId is required");
        }
    }
}
