using FluentValidation;

namespace MyPiggyBank.Core.Protocol.Query.Validators
{
    public class OperationsQueryValidator : QueryStringParamsValidator<OperationsQuery>
    {
        public OperationsQueryValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .Length(1, 200)
                .WithMessage("Length of the operation name should be between 1 and 200.");

            RuleFor(q => q)
                .Must(res => ((res.MinValue == null || res.MaxValue == null) || res.MinValue <= res.MaxValue))
                .WithMessage("Min value can't be higher than max value");

            RuleFor(q => q)
                .Must(res => ((res.MinOccuredAt == null || res.MaxOccuredAt == null) || res.MinOccuredAt <= res.MaxOccuredAt))
                .WithMessage("Earliest date occured at can't be later than latest occured date at.");
        }
    }
}
