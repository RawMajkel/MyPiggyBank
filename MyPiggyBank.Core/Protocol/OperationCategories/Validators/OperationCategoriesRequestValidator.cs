using FluentValidation;
using MyPiggyBank.Core.Protocol.OperationCategories.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPiggyBank.Core.Protocol.OperationCategories.Validators
{
    public class OperationCategoriesRequestValidator : AbstractValidator<OperationCategoriesSaveRequest>
    {
        public OperationCategoriesRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(OperationCategoriesResources.OperationCategoriesRequestValidator_Name_Empty_Error)
                .MinimumLength(4)
                .WithMessage(OperationCategoriesResources.OperationCategoriesRequestValidator_Name_Length_Error);
        }
    }
}
