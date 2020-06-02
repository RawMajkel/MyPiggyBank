using FluentValidation;

namespace MyPiggyBank.Core.Protocol.Query.Validators
{
    public class OperationsQueryValidator : AbstractValidator<OperationsQuery>
    {
        public OperationsQueryValidator()
        {
            // TODO what about rules for base class (QueryStringParams)?
            // TODO validation rules
        }
    }
}
