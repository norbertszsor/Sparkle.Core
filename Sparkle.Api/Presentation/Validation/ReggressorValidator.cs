using FluentValidation;
using Sparkle.Transfer.Query;

namespace Sparkle.Api.Presentation.Validation
{
    public class ReggressorValidator : AbstractValidator<GetPredictionQuery>
    {
        public ReggressorValidator() 
        {
            RuleFor(x => x.MeterId).NotEmpty()
                .WithMessage("MeterName is required");

            RuleFor(x => x.Hours).GreaterThan(23)
                .WithMessage("Hours must be greater than 23");
        }
    }
}
