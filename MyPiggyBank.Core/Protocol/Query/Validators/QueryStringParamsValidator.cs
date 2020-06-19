using FluentValidation;

namespace MyPiggyBank.Core.Protocol.Query.Validators
{
    public class QueryStringParamsValidator<T> : AbstractValidator<T> where T : QueryStringParams
    {
        public QueryStringParamsValidator()
        {
            RuleFor(q => q.Limit)
                .NotEmpty().InclusiveBetween(1, 100)
                .WithMessage("Collection size limit should be set between 1 and 100.");
        }
    }
}
