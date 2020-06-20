using FluentValidation;
using MyPiggyBank.Core.Protocol.Base;

namespace MyPiggyBank.Core.Protocol.OperationCategories
{
    public class OperationCategoriesValidator : QueryStringParamsValidator<OperationCategoriesQuery>
    {
        public OperationCategoriesValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .Length(1, 20)
                .WithMessage("Length of Category name should be between 1 and 20.");
        }
    }
}
