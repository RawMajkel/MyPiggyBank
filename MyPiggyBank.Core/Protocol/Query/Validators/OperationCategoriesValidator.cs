using FluentValidation;

namespace MyPiggyBank.Core.Protocol.Query.Validators
{
    public class OperationCategoriesValidator : AbstractValidator<OperationCategoriesQuery>
    {
        public OperationCategoriesValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .Length(1,20)
                .WithMessage("Length of Category name should be between 1 and 20.");
        }
    }
}
