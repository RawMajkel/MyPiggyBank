using FluentValidation;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Validators
{
    public class CyclicOperationRequestValidator : AbstractValidator<CyclicOperationSaveRequest>
    {
        public CyclicOperationRequestValidator()
        {
            RuleFor(r => r.ResourceId)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_Resource_Empty_Error);
            RuleFor(r => r.OperationCategoryId)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_OperationCategory_Empty_Error);
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_Name_Empty_Error)
                .MinimumLength(4)
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_Name_Length_Error);
        }
    }
}
