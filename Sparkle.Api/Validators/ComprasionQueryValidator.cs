using FluentValidation;
using Sparkle.Transfer.Enums;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Validators
{
    public class ComprasionQueryValidator : AbstractValidator<GetComparisonQuery>
    {
        public ComprasionQueryValidator()
        {
            RuleFor(x => x.MeterId).NotEmpty()
                .WithMessage("MeterName is required");

            RuleFor(x => x.Hours).NotEmpty().IsInEnum()
                .WithMessage($"Hours should be one of {string.Join(",", Enum.GetNames(typeof(ReggressorTimeSpanEnum)))}");
        }
    }
}
