using FluentValidation;
using MyPiggyBank.Core.Protocol.Base;

namespace MyPiggyBank.Core.Protocol.CyclicOperation
{
    public class CyclicOperationsValidator : QueryStringParamsValidator<CyclicOperationsQuery>
    {
        public CyclicOperationsValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .Length(1, 20)
                .WithMessage("Length of Category name should be between 1 and 20.");

            RuleFor(q => q.User)
                .NotEmpty()
                .WithMessage("User of Cyclic operation has to be provided");

            RuleFor(q => q.Resource)
                .NotEmpty()
                .WithMessage("Resource of Cyclic operation has to be provided");

            RuleFor(q => q)
                .Must(res => res.MinEstimatedValue == null || res.MaxEstimatedValue == null || res.MinEstimatedValue <= res.MaxEstimatedValue)
                .WithMessage("Min estimated value can't be higher than max estimated value");

            RuleFor(q => q)
                .Must(res => res.MinPeriod == null || res.MaxPeriod == null || res.MinPeriod <= res.MaxPeriod)
                .WithMessage("Min period can't be higher than max period.");

            RuleFor(q => q)
                .Must(res => res.MinNextOccurence == null || res.MaxNextOccurence == null || res.MinNextOccurence <= res.MaxNextOccurence)
                .WithMessage("Earliest date of the next occurence can't be later than latest date of the next occurence.");
        }
    }
}
