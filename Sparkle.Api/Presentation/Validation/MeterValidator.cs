using FluentValidation;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Validation
{
    public class MeterValidator : AbstractValidator<GetMeterListQuery>
    {
        public MeterValidator()
        {
            RuleFor(x => x.CompanyId).NotEmpty()
                .WithMessage("CompanyId is required");
        }
    }
}
