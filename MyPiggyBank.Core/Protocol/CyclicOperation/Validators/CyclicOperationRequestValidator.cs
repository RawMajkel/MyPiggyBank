using FluentValidation;
using MyPiggyBank.Core.Protocol.CyclicOperation.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.CyclicOperation.Validators
{
    public class CyclicOperationRequestValidator : AbstractValidator<CyclicOperationRequest>
    {
        public CyclicOperationRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_Name_Empty_Error)
                .MinimumLength(4)
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_Name_Length_Error);
            RuleFor(r => r.IsIncome)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_IsIncome_Empty_Error);
            RuleFor(r => r.Period)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_Period_Empty_Error);
            RuleFor(r => r.NextOccurence)
                .NotEmpty()
                .WithMessage(CyclicOperationResources.CyclicOperationRequestValidator_NextOccurence_Empty_Error);
        }
    }
}
