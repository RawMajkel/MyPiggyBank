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
                .WithMessage(ResourceResources.ResourceRequestValidator_UserId_Empty_Error);

            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(ResourceResources.ResourceRequestValidator_Name_Empty_Error)
                .MinimumLength(4)
                .WithMessage(ResourceResources.ResourceRequestValidator_Name_Length_Error);

            RuleFor(r => r.Currency)
                .NotEmpty()
                .WithMessage(ResourceResources.ResourceRequestValidator_Currency_Empty_Error);
        }
    }
}