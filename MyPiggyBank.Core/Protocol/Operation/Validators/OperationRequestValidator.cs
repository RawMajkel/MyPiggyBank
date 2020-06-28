using FluentValidation;
using MyPiggyBank.Core.Protocol.Operation.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.Operation.Validators
{
    public class OperationRequestValidator : AbstractValidator<OperationSaveRequest>
    {
        public OperationRequestValidator()
        {
            RuleFor(r => r.ResourceId)
                .NotEmpty()
                .WithMessage(OperationResources.OperationRequestValidator_Resource_Empty_Error);
            RuleFor(r => r.OperationCategoryId)
                .NotEmpty()
                .WithMessage(OperationResources.OperationRequestValidator_OperationCategory_Empty_Error);
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(OperationResources.OperationRequestValidator_Name_Empty_Error)
                .MinimumLength(4)
                .WithMessage(OperationResources.OperationRequestValidator_Name_Length_Error);
            RuleFor(r => r.IsIncome)
                .NotEmpty()
                .WithMessage(OperationResources.OperationRequestValidator_IsIncome_Empty_Error);
            RuleFor(r => r.OccuredAt)
                .NotEmpty()
                .WithMessage(OperationResources.OperationRequestValidator_OccuredAt_Empty_Error);
        }
    }
}
