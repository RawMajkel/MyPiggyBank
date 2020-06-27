using FluentValidation;
using MyPiggyBank.Core.Protocol.CyclicOperation.Responses;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Validators
{
    public class CyclicOperationResponseValidator : AbstractValidator<CyclicOperationResponse>
    {
        public CyclicOperationResponseValidator()
        {
            RuleFor(r => r.ResourceId)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_Resource_Empty_Error);
            RuleFor(r => r.OperationCategoryId)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_OperationCategory_Empty_Error);
        }
    }
}
