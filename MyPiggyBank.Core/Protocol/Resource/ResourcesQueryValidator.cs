using FluentValidation;
using MyPiggyBank.Core.Protocol.Base;

namespace MyPiggyBank.Core.Protocol.Resource
{
    public class ResourcesQueryValidator : QueryStringParamsValidator<ResourcesQuery>
    {
        public ResourcesQueryValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .Length(1, 50)
                .WithMessage("Length of Resource name should be between 1 and 50.");

            RuleFor(q => q)
                .Must(res => res.MinValue == null || res.MaxValue == null || res.MinValue <= res.MaxValue)
                .WithMessage("Min value can't be higher than max value");
        }
    }
}
