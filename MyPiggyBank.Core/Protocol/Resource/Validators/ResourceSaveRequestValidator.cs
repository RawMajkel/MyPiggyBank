using FluentValidation;
using MyPiggyBank.Core.Protocol.Resource.Requests;

namespace MyPiggyBank.Core.Protocol.Resource.Validators
{
    public class ResourceSaveRequestValidator : AbstractValidator<ResourceSaveRequest>
    {
        public ResourceSaveRequestValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty()
                .WithMessage("error message");

            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("error message");

            RuleFor(r => r.Currency)
                .NotEmpty()
                .WithMessage("error message");
        }
    }
}