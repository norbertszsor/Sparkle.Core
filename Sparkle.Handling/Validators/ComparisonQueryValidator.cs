using FluentValidation;
using Sparkle.Transfer.Enums;
using Sparkle.Transfer.Query;

namespace Sparkle.Handling.Validators
{
    public class ComparisonQueryValidator : AbstractValidator<GetComparisonQuery>
    {
        public ComparisonQueryValidator()
        {
            RuleFor(x => x.MeterId).NotEmpty()
                .WithMessage("MeterName is required");

            RuleFor(x => x.Hours).NotEmpty()
                .IsInEnum()
                .WithMessage(
                    $"Hours should be one of {string.Join(",", Enum.GetNames(typeof(RegressorTimeSpanEnum)))}");
        }
    }
}
