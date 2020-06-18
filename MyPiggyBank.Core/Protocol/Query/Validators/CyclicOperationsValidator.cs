using FluentValidation;

namespace MyPiggyBank.Core.Protocol.Query.Validators
{
    public class CyclicOperationsValidator : AbstractValidator<CyclicOperationsQuery>
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
        }
    }
}
