using FluentValidation;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Validation
{
    public class ReggressorValidation : AbstractValidator<GetPredictionQuery>
    {
        public ReggressorValidation() 
        {
            RuleFor(x => x.MeterName).NotEmpty()
                .WithMessage("MeterName is required");

            RuleFor(x => x.Hours).GreaterThan(24)
                .WithMessage("Hours must be greater than 24");
        }
    }
}
